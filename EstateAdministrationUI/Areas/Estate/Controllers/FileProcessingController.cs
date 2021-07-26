namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Common;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Shared.Logger;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class FileProcessingController : Controller
    {
        #region Fields

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;

        /// <summary>
        /// The view model factory
        /// </summary>
        private readonly IViewModelFactory ViewModelFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileProcessingController"/> class.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="viewModelFactory">The view model factory.</param>
        public FileProcessingController(IApiClient apiClient,
                                        IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the file import log list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileImportLogList(CancellationToken cancellationToken)
        {
            return this.View("FileImportLogList");
        }

        [HttpGet]
        public async Task<IActionResult> GetFileImportLog([FromQuery] Guid fileImportLogId, CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            FileImportLogModel fileImportLogModel = await this.ApiClient.GetFileImportLog(accessToken, this.User.Identity as ClaimsIdentity, fileImportLogId, cancellationToken);

            if (fileImportLogModel == null)
            {
                return this.RedirectToAction("GetFileImportLogList", "FileProcessing").WithWarning("Warning:", $"Failed to get File Import Log Record, please try again.");
            }

            return this.View("FileImportLog", this.ViewModelFactory.ConvertFrom(fileImportLogModel));
        }

        [HttpPost]
        public async Task<IActionResult> GetFileImportLogFileListAsJson([FromQuery] Guid fileImportLogId,
                                                                        CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                FileImportLogModel fileImportLogModel =
                    await this.ApiClient.GetFileImportLog(accessToken, this.User.Identity as ClaimsIdentity, fileImportLogId, cancellationToken);

                FileImportLogViewModel viewModel = this.ViewModelFactory.ConvertFrom(fileImportLogModel);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.Files));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, new List<FileImportLogFileViewModel>()));
            }
        }

        /// <summary>
        /// Gets the merchant list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetFileImportLogListAsJson(CancellationToken cancellationToken)
        {
            try
            {
                // Start and End Date 
                // Set the defaults
                DateTime startDateTime = DateTime.Now.AddDays(-1).Date;
                DateTime endDateTime = DateTime.Now.Date;
                Guid? merchantId = null;

                if (this.HttpContext.Request.Form.ContainsKey("startDate"))
                {
                    startDateTime = DateTime.ParseExact(this.HttpContext.Request.Form["startDate"], "yyyy-MM-dd", null);
                }

                if (this.HttpContext.Request.Form.ContainsKey("endDate"))
                {
                    endDateTime = DateTime.ParseExact(this.HttpContext.Request.Form["endDate"], "yyyy-MM-dd", null);
                }

                if (this.HttpContext.Request.Form.ContainsKey("merchantId"))
                {
                    var merchantIdFormParameter = this.HttpContext.Request.Form["merchantId"];
                    if (merchantIdFormParameter != "-1")
                    {
                        merchantId = Guid.Parse(merchantIdFormParameter);
                    }
                }

                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                List<FileImportLogModel> importLogFileModelList =
                    await this.ApiClient.GetFileImportLogs(accessToken, this.User.Identity as ClaimsIdentity, merchantId, startDateTime, endDateTime, cancellationToken);
                var importLogFileViewModelList = this.ViewModelFactory.ConvertFrom(importLogFileModelList);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, importLogFileViewModelList));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, new List<FileImportLogFileViewModel>()));
            }
        }

        /// <summary>
        /// Gets the merchant list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetMerchantListAsJson(CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                var merchantModelList = await this.ApiClient.GetMerchants(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

                var merchantViewModelList = this.ViewModelFactory.ConvertFrom(merchantModelList);

                return this.Json(merchantViewModelList);
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, new List<MerchantViewModel>()));
            }
        }

        /// <summary>
        /// Gets the file details.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileDetails([FromQuery] Guid fileId, CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            FileDetailsModel fileDetailsModel = await this.ApiClient.GetFileDetails(accessToken, this.User.Identity as ClaimsIdentity, fileId, cancellationToken);

            if (fileDetailsModel == null)
            {
                return this.RedirectToAction("GetFileImportLog", "FileProcessing").WithWarning("Warning:", $"Failed to get File Details Record, please try again.");
            }

            return this.View("FileDetails", this.ViewModelFactory.ConvertFrom(fileDetailsModel));
        }

        /// <summary>
        /// Gets the file line list as json.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetFileLineListAsJson([FromQuery] Guid fileId,
                                                               CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                FileDetailsModel fileDetailsModel =
                    await this.ApiClient.GetFileDetails(accessToken, this.User.Identity as ClaimsIdentity, fileId, cancellationToken);

                FileDetailsViewModel viewModel = this.ViewModelFactory.ConvertFrom(fileDetailsModel);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.FileLines));
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, new List<FileImportLogFileViewModel>()));
            }
        }

        #endregion
    }
}