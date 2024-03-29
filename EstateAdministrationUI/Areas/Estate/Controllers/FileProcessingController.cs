﻿namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Common;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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

        #endregion

        #region Constructors
        public FileProcessingController(IApiClient apiClient)
        {
            this.ApiClient = apiClient;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the file details.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileDetails([FromQuery] Guid fileId,
                                                        CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                FileDetailsModel fileDetailsModel = await this.ApiClient.GetFileDetails(accessToken, Guid.Empty,
                                                                                        estateId, fileId, cancellationToken);

                return this.View("FileDetails", ViewModelFactory.ConvertFrom(fileDetailsModel));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.View("FileDetails").WithWarning("File Details", Helpers.BuildUserErrorMessage("Failed to get File Details Record"));
            }
        }

        /// <summary>
        /// Gets the file import log.
        /// </summary>
        /// <param name="fileImportLogId">The file import log identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileImportLog([FromQuery] Guid fileImportLogId,
                                                          CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                FileImportLogModel fileImportLogModel =
                    await this.ApiClient.GetFileImportLog(accessToken, Guid.Empty,
                                                          estateId, fileImportLogId, cancellationToken);

                return this.View("FileImportLog", ViewModelFactory.ConvertFrom(fileImportLogModel));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.View("FileImportLog").WithWarning("File Import Log", Helpers.BuildUserErrorMessage("Failed to get File Import Log Record"));
            }
        }

        /// <summary>
        /// Gets the file import log file list as json.
        /// </summary>
        /// <param name="fileImportLogId">The file import log identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetFileImportLogFileListAsJson([FromQuery] Guid fileImportLogId,
                                                                        CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                FileImportLogModel fileImportLogModel =
                    await this.ApiClient.GetFileImportLog(accessToken, Guid.Empty,
                                                          estateId, fileImportLogId, cancellationToken);

                FileImportLogViewModel viewModel = ViewModelFactory.ConvertFrom(fileImportLogModel);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.Files));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting file import log"));
            }
        }

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

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                List<FileImportLogModel> importLogFileModelList =
                    await this.ApiClient.GetFileImportLogs(accessToken, Guid.Empty,
                                                           estateId, merchantId, startDateTime, endDateTime, cancellationToken);
                List<FileImportLogViewModel> importLogFileViewModelList = ViewModelFactory.ConvertFrom(importLogFileModelList);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, importLogFileViewModelList));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting file import log list"));
            }
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

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                FileDetailsModel fileDetailsModel = await this.ApiClient.GetFileDetails(accessToken, Guid.Empty,
                                                                                        estateId, fileId, cancellationToken);

                FileDetailsViewModel viewModel = ViewModelFactory.ConvertFrom(fileDetailsModel);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.FileLines));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Failed to get File Details Record"));
            }
        }

        /// <summary>
        /// Gets the file profile list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileProfileListAsJson(CancellationToken cancellationToken)
        {
            List<FileProfileViewModel> fileProfileViewModels = new List<FileProfileViewModel>();
            fileProfileViewModels.Add(new FileProfileViewModel
                                      {
                                          FileProfileId = Guid.Parse("B2A59ABF-293D-4A6B-B81B-7007503C3476"),
                                          FileProfileName = "Safaricom Topup"
                                      });
            fileProfileViewModels.Add(new FileProfileViewModel
                                      {
                                          FileProfileId = Guid.Parse("8806EDBC-3ED6-406B-9E5F-A9078356BE99"),
                                          FileProfileName = "Voucher Issue"
                                      });

            return this.Json(fileProfileViewModels);
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

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                var merchantModelList = await this.ApiClient.GetMerchants(accessToken, Guid.Empty,
                                                                          estateId, cancellationToken);

                var merchantViewModelList = ViewModelFactory.ConvertFrom(merchantModelList);

                return this.Json(merchantViewModelList);
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting merchant list"));
            }
        }

        /// <summary>
        /// Posts the upload file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUploadFile(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            if (this.Request.Form.ContainsKey("merchantId") == false)
            {
                return this.BadRequest(new
                                       {
                                           message = "Missing merchantId form parameter"
                                       });
            }

            if (this.Request.Form.ContainsKey("fileProfileId") == false)
            {
                return this.BadRequest(new
                                       {
                                           message = "Missing fileProfileId form parameter"
                                       });
            }

            IFormFileCollection files = this.HttpContext.Request.Form.Files;
            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);
            Guid userId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, "sub");
            Guid merchantId = Guid.Parse(this.Request.Form["merchantId"]);
            Guid fileProfileId = Guid.Parse(this.Request.Form["fileProfileId"]);
            IFormFile file = files.First();
            if (file.Length > 0)
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms, cancellationToken);
                    Byte[] fileBytes = ms.ToArray();
                    Guid fileId = await this.ApiClient.UploadFile(accessToken,
                                                                  Guid.Empty, 
                                                                  estateId,
                                                                  merchantId,
                                                                  userId,
                                                                  fileProfileId,
                                                                  fileBytes,
                                                                  file.FileName,
                                                                  cancellationToken);

                    return this.Ok(new
                                   {
                                       message = "File Processed Successfully"
                                   });
                }
            }

            return this.BadRequest(new
                                   {
                                       message = "File is empty"
                                   });
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UploadFile(CancellationToken cancellationToken)
        {
            return this.View("UploadFile");
        }

        #endregion
    }
}