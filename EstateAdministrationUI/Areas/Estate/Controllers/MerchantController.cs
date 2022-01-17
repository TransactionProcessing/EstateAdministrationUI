﻿namespace EstateAdministrationUI.Areas.Estate.Controllers
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

        [HttpGet]
        public ActionResult AssignOperatorToMerchant(Guid merchantId,
                                                     CancellationToken cancellationToken)
        {
            AssignOperatorToMerchantViewModel viewModel = new AssignOperatorToMerchantViewModel();
            viewModel.MerchantId = merchantId;

            return this.PartialView("_AssignOperatorToMerchant", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperatorListAsJson(Guid merchantId, CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                EstateModel estate = await this.ApiClient.GetEstate(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);
                
                List<OperatorListViewModel> operatorViewModels = this.ViewModelFactory.ConvertFrom(estate.EstateId, estate.Operators);

                MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

                List<OperatorListViewModel> availableOperators = new List<OperatorListViewModel>();
                foreach (OperatorListViewModel operatorListViewModel in operatorViewModels)
                {
                    if (merchantModel.Operators.SingleOrDefault(o => o.OperatorId == operatorListViewModel.OperatorId) ==null)
                    {
                        availableOperators.Add(operatorListViewModel);
                    }
                }

                return this.Json(availableOperators);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return this.Json(new List<OperatorListViewModel>());
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AssignOperatorToMerchant(AssignOperatorToMerchantViewModel viewModel,
                                                                  CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return PartialView("_AssignOperatorToMerchant", viewModel);

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            AssignOperatorToMerchantModel model = this.ViewModelFactory.ConvertFrom(viewModel);

            try
            {

                await this.ApiClient.AssignOperatorToMerchant(accessToken, this.User.Identity as ClaimsIdentity, viewModel.MerchantId, model, cancellationToken);

                return this.RedirectToAction("GetMerchant",
                                             new
                                             {
                                                 merchantId = viewModel.MerchantId
                                             }).WithSuccess("Operator Assigned Successfully", $"Operator Assigned successfully for Merchant");
            }
            catch(Exception e)
            {
                return this.RedirectToAction("GetMerchant",
                                             new
                                             {
                                                 merchantId = viewModel.MerchantId
                                             }).WithWarning("Operator Assign Failed", Helpers.BuildUserErrorMessage("Failed to assign Operator to Merchant"));
            }
        }

        [HttpGet]
        public ActionResult AddMerchantDevice(Guid merchantId, CancellationToken cancellationToken)
        {
            AddMerchantDeviceViewModel viewModel = new AddMerchantDeviceViewModel();
            viewModel.MerchantId = merchantId;

            return this.PartialView("_AddMerchantDevice", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddMerchantDevice(AddMerchantDeviceViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return PartialView("_AddMerchantDevice", viewModel);

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            AddMerchantDeviceModel model = this.ViewModelFactory.ConvertFrom(viewModel);

            try
            {
                await this.ApiClient.AddDeviceToMerchant(accessToken, this.User.Identity as ClaimsIdentity, viewModel.MerchantId, model, cancellationToken);

                return this.RedirectToAction("GetMerchant",
                                             new
                                             {
                                                 merchantId = viewModel.MerchantId
                                             }).WithSuccess("Device Added Successfully", $"Device added successfully for Merchant");
            }
            catch(Exception e)
            {
                return this.RedirectToAction("GetMerchant",
                                             new
                                             {
                                                 merchantId = viewModel.MerchantId
                                             }).WithWarning("Device Add Failed", Helpers.BuildUserErrorMessage("Failed to add device to Merchant"));
            }
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
                try
                {
                    String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                    CreateMerchantModel createMerchantModel = this.ViewModelFactory.ConvertFrom(viewModel);

                    // All good with model, call the client to create the merchant
                    CreateMerchantResponseModel createMerchantResponse =
                        await this.ApiClient.CreateMerchant(accessToken, this.User.Identity as ClaimsIdentity, createMerchantModel, cancellationToken);

                    // TODO: Investigate some kind of spinner...
                    await Task.Delay(TimeSpan.FromSeconds(30));

                    // Merchant Created, redirect to the Merchant List screen
                    return this.RedirectToAction("GetMerchant",
                                                 "Merchant",
                                                 new
                                                 {
                                                     merchantId = createMerchantResponse.MerchantId
                                                 });
                }
                catch(Exception e)
                {
                    // Something went wrong creating the contract
                    return this.View("CreateMerchant").WithWarning("New Merchant", Helpers.BuildUserErrorMessage("Error creating the merchant"));
                }
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
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

                return this.View("MerchantDetails", this.ViewModelFactory.ConvertFrom(merchantModel));
            }
            catch(Exception e)
            {
                return this.RedirectToAction("GetMerchantList", "Merchant").WithWarning("Merchant Details:", Helpers.BuildUserErrorMessage("Failed to get Merchant Record"));
            }
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
            try
            {

                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

                MerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantModel);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.Devices));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting merchant devices"));
            }
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
            try
            {
                // Search Value from (Search box)  
                String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                List<MerchantModel> merchantList = await this.ApiClient.GetMerchants(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

                List<MerchantListViewModel> merchantViewModels = this.ViewModelFactory.ConvertFrom(merchantList);

                Expression<Func<MerchantListViewModel, Boolean>> whereClause = m => m.MerchantName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                    m.Town.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, merchantViewModels, whereClause));
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting merchant list"));
            }
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
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                MerchantModel merchantModel = await this.ApiClient.GetMerchant(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

                MerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantModel);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.Operators));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting merchant operators"));
            }
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
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                // Start and End Date 
                // Set the defaults
                DateTime startDateTime = DateTime.Now.AddDays(-1).Date;
                DateTime endDateTime = DateTime.Now.Date;

                if (this.HttpContext.Request.Form.ContainsKey("startDate"))
                {
                    startDateTime = DateTime.ParseExact(this.HttpContext.Request.Form["startDate"], "yyyy-MM-dd", null);
                }

                if (this.HttpContext.Request.Form.ContainsKey("endDate"))
                {
                    endDateTime = DateTime.ParseExact(this.HttpContext.Request.Form["endDate"], "yyyy-MM-dd", null);
                    endDateTime = endDateTime.AddDays(1);
                }

                List<MerchantBalanceHistory> merchantBalanceHistory =
                    await this.ApiClient.GetMerchantBalanceHistory(accessToken, this.User.Identity as ClaimsIdentity, merchantId, startDateTime, endDateTime, cancellationToken);

                MerchantBalanceHistoryListViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantBalanceHistory);

                // Search Value from (Search box)  
                String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
                Expression<Func<MerchantBalanceHistoryViewModel, Boolean>> whereClause = m => m.Reference.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, viewModel.MerchantBalanceHistoryViewModels, whereClause));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting merchant balance history"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMerchantBalanceAsJson([FromQuery] Guid merchantId,
                                        CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                MerchantBalanceModel merchantBalance = await this.ApiClient.GetMerchantBalance(accessToken, this.User.Identity as ClaimsIdentity, merchantId, cancellationToken);

                MerchantBalanceViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchantBalance);

                return this.Json(viewModel);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting merchant balance"));
            }
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
                try
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
                catch(Exception e)
                {
                    return this.RedirectToAction("MakeMerchantDeposit", "Merchant")
                               .WithWarning("Deposit Unsuccessful", Helpers.BuildUserErrorMessage("Deposit made successfully for Merchant - {viewModel.MerchantName}"));
                }
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