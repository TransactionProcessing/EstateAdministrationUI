namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
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
    using Newtonsoft.Json;
    using Services;
    using Shared.Logger;

    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class MerchantController : Controller
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
        /// Initializes a new instance of the <see cref="MerchantController" /> class.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="viewModelFactory">The view model factory.</param>
        public MerchantController(IApiClient apiClient,
                                  IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the merchant.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateMerchant(CancellationToken cancellationToken)
        {
            CreateMerchantViewModel viewModel = new CreateMerchantViewModel();

            return this.View("CreateMerchant", viewModel);
        }

        /// <summary>
        /// Creates the merchant.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMerchant(CreateMerchantViewModel viewModel,
                                                        CancellationToken cancellationToken)
        {
            // Validate the model
            if (this.ValidateModel(viewModel))
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                CreateMerchantModel createMerchantModel = this.ViewModelFactory.ConvertFrom(viewModel);

                // All good with model, call the client to create the golf club
                CreateMerchantResponseModel creteMerchantResponse =
                    await this.ApiClient.CreateMerchant(accessToken, this.User.Identity as ClaimsIdentity, createMerchantModel, cancellationToken);

                // Merchant Created, redirect to the Merchant List screen
                return this.RedirectToAction("GetMerchant",
                                             "Merchant",
                                             new
                                             {
                                                 merchantId = creteMerchantResponse.MerchantId
                                             });
            }

            // If we got this far, something failed, redisplay form
            return this.View("CreateMerchant", viewModel);
        }

        /// <summary>
        /// Gets the merchant.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMerchant([FromQuery] Guid merchantId,
                                                     CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

            return this.View("MerchantDetails", this.ViewModelFactory.ConvertFrom(merchantModel));
        }

        /// <summary>
        /// Gets the merchant device list as json.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMerchantDeviceListAsJson([FromQuery] Guid merchantId,
                                                                     CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetMerchantDeviceListAsJson");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

            MerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantModel);

            DataTablesResult<KeyValuePair<String, String>> dataTableResult = Helpers.GetDataForDataTable(this.Request.Form, viewModel.Devices);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);
            
            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.Devices));
        }

        /// <summary>
        /// Gets the merchant list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMerchantList(CancellationToken cancellationToken)
        {
            return this.View("MerchantList");
        }

        /// <summary>
        /// Gets the merchant list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMerchantListAsJson(CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetMerchantListAsJson");

            // Search Value from (Search box)  
            String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            Logger.LogDebug($"searchvalue is {searchValue}");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");
            Logger.LogDebug("got access token");

            List<MerchantModel> merchantList = await this.ApiClient.GetMerchants(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

            List<MerchantListViewModel> merchantViewModels = this.ViewModelFactory.ConvertFrom(merchantList);

            Logger.LogDebug($"merchant list count is {merchantViewModels.Count}");

            Expression<Func<MerchantListViewModel, Boolean>> whereClause = m => m.MerchantName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                m.Town.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

            DataTablesResult<MerchantListViewModel> dataTableResult = Helpers.GetDataForDataTable(this.Request.Form, merchantViewModels, whereClause);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, merchantViewModels, whereClause));
        }

        /// <summary>
        /// Gets the merchant operator list as json.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMerchantOperatorListAsJson([FromQuery] Guid merchantId,
                                                                       CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetMerchantOperatorListAsJson");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

            MerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantModel);

            DataTablesResult<MerchantOperatorViewModel> dataTableResult = Helpers.GetDataForDataTable(this.Request.Form, viewModel.Operators);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.Operators));
        }

        /// <summary>
        /// Gets the merchant balance history as json.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMerchantBalanceHistoryAsJson([FromQuery] Guid merchantId,
                                                                         CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetMerchantBalanceHistoryAsJson");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            List<MerchantBalanceHistory> merchantBalanceHistory = await this.ApiClient.GetMerchantBalanceHistory(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

            MerchantBalanceHistoryListViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantBalanceHistory);

            // Search Value from (Search box)  
            String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            Logger.LogDebug($"searchvalue is {searchValue}");

            Expression<Func<MerchantBalanceHistoryViewModel, Boolean>> whereClause = m => m.Reference.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

            DataTablesResult<MerchantBalanceHistoryViewModel> dataTableResult = Helpers.GetDataForDataTable(this.Request.Form, viewModel.MerchantBalanceHistoryViewModels, whereClause);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.MerchantBalanceHistoryViewModels, whereClause));
        }
        [HttpGet]
        public async Task<IActionResult> MakeMerchantDeposit([FromQuery] Guid merchantId,
                                                             [FromQuery] String merchantName,
                                                             CancellationToken cancellationToken)
        {
            MakeMerchantDepositViewModel viewModel = new MakeMerchantDepositViewModel();
            viewModel.MerchantId = merchantId.ToString();
            viewModel.MerchantName = merchantName;

            return this.View("MakeMerchantDeposit", viewModel);
        }

        /// <summary>
        /// Creates the merchant.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MakeMerchantDeposit(MakeMerchantDepositViewModel viewModel,
                                                             CancellationToken cancellationToken)
        {
            // Validate the model
            if (this.ValidateModel(viewModel))
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                MakeMerchantDepositModel makeMerchantDepositModel = this.ViewModelFactory.ConvertFrom(viewModel);

                // All good with model, call the client to create the golf club
                MakeMerchantDepositResponseModel makeMerchantDepositResponseModel =
                    await this.ApiClient.MakeMerchantDeposit(accessToken,
                                                             this.User.Identity as ClaimsIdentity,
                                                             Guid.Parse(viewModel.MerchantId),
                                                             makeMerchantDepositModel,
                                                             cancellationToken);

                // Merchant Created, redirect to the Merchant List screen
                return this.RedirectToAction("GetMerchantList", "Merchant")
                           .WithSuccess("Deposit Successful", $"Deposit made successfully for Merchant - {viewModel.MerchantName}");
            }

            // If we got this far, something failed, redisplay form
            return this.View("MakeMerchantDeposit", viewModel);
        }

        /// <summary>
        /// Updates the merchant.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMerchant(MerchantViewModel viewModel,
                                                        CancellationToken cancellationToken)
        {
            return this.View("MerchantDetails", viewModel);
        }

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(CreateMerchantViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(MakeMerchantDepositViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        #endregion
    }
}