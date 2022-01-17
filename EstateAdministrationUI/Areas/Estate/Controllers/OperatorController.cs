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
    using Services;
    using Shared.Logger;

    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class OperatorController : Controller
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
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="viewModelFactory">The view model factory.</param>
        public OperatorController(IApiClient apiClient,
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
        public async Task<IActionResult> CreateOperator(CancellationToken cancellationToken)
        {
            CreateOperatorViewModel viewModel = new CreateOperatorViewModel();

            return this.View("CreateOperator", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperator(CreateOperatorViewModel viewModel,
                                                        CancellationToken cancellationToken)
        {
            // Validate the model
            if (this.ValidateModel(viewModel))
            {
                try
                {
                    String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                    CreateOperatorModel createOperatorModel = this.ViewModelFactory.ConvertFrom(viewModel);

                    // All good with model, call the client to create the operator
                    CreateOperatorResponseModel createOperatorResponse =
                        await this.ApiClient.CreateOperator(accessToken, this.User.Identity as ClaimsIdentity, createOperatorModel, cancellationToken);

                    // Operator Created, redirect to the Operator List screen
                    return this.RedirectToAction("GetOperatorList",
                                                 "Operator",
                                                 new
                                                 {
                                                     Area = "Estate"
                                                 }).WithSuccess("Operator Created", $"Operator {createOperatorModel.OperatorName} created successfully");
                }
                catch(Exception e)
                {
                    return this.RedirectToAction("CreateOperator", "Operator")
                               .WithWarning("Operator Not Created", Helpers.BuildUserErrorMessage("Operator not created successfully"));
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View("CreateOperator", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperatorList(CancellationToken cancellationToken)
        {
            return this.View("OperatorList");
        }

        /// <summary>
        /// Gets the merchant list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetOperatorListAsJson(CancellationToken cancellationToken)
        {
            try
            {
                // Search Value from (Search box)  
                String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();

                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                EstateModel estate = await this.ApiClient.GetEstate(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

                List<OperatorListViewModel> operatorViewModels = this.ViewModelFactory.ConvertFrom(estate.EstateId, estate.Operators);

                Expression<Func<OperatorListViewModel, Boolean>> whereClause = m => m.OperatorName.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, operatorViewModels, whereClause));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return this.Json(Helpers.GetErrorDataForDataTable<String>("Error getting operator list"));
            }
        }

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(CreateOperatorViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        #endregion
    }
}