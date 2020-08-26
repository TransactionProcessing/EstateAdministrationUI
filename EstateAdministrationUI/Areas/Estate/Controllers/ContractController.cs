using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading;
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
    public class ContractController : Controller
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
        /// Initializes a new instance of the <see cref="ContractController" /> class.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="viewModelFactory">The view model factory.</param>
        public ContractController(IApiClient apiClient,
                                  IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        [HttpGet]
        public async Task<IActionResult> CreateContract(CancellationToken cancellationToken)
        {
            CreateContractViewModel viewModel = new CreateContractViewModel();

            return this.View("CreateContract", viewModel);
        }

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(CreateContractViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractViewModel viewModel, CancellationToken cancellationToken)
        {
            // Validate the model
            if (this.ValidateModel(viewModel))
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                CreateContractModel createContractModel = this.ViewModelFactory.ConvertFrom(viewModel);

                // All good with model, call the client to create the golf club
                CreateContractResponseModel createContractResponseModel =
                    await this.ApiClient.CreateContract(accessToken, this.User.Identity as ClaimsIdentity, createContractModel, cancellationToken);

                // Merchant Created, redirect to the Merchant List screen
                return this.RedirectToAction("GetContractList",
                                             "Contract").WithSuccess("Contract Created Successful", $"Contract {viewModel.ContractDescription} successfully created");
            }

            // If we got this far, something failed, redisplay form
            return this.View("CreateContract", viewModel);
        }

        /// <summary>
        /// Gets the contract list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetContractList(CancellationToken cancellationToken)
        {
            return this.View("ContractList");
        }

        /// <summary>
        /// Gets the contract list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetContractListAsJson(CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetContractListAsJson");

            // Search Value from (Search box)  
            String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            Logger.LogDebug($"searchvalue is {searchValue}");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");
            Logger.LogDebug("got access token");

            List<ContractModel> contractList = await this.ApiClient.GetContracts(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

            List<ContractListViewModel> contractViewModels = this.ViewModelFactory.ConvertFrom(contractList);

            Logger.LogDebug($"contract list count is {contractViewModels.Count}");

            Expression<Func<ContractListViewModel, Boolean>> whereClause = c => c.OperatorName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

            DataTablesResult<ContractListViewModel> dataTableResult = Helpers.GetDataForDataTable(this.Request.Form, contractViewModels, whereClause);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, contractViewModels, whereClause));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperatorListAsJson(CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetOperatorListAsJson");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");
            Logger.LogDebug("got access token");

            EstateModel estate = await this.ApiClient.GetEstate(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

            List<OperatorListViewModel> operatorViewModels = this.ViewModelFactory.ConvertFrom(estate.EstateId, estate.Operators);

            return this.Json(operatorViewModels);
        }

        #endregion
    }
}
