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

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
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

        #endregion

        #region Methods

        /// <summary>
        /// Creates the contract.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateContract(CancellationToken cancellationToken)
        {
            CreateContractViewModel viewModel = new CreateContractViewModel();

            return this.View("CreateContract", viewModel);
        }

        /// <summary>
        /// Creates the contract.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractViewModel viewModel,
                                                        CancellationToken cancellationToken)
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
                return this.RedirectToAction("GetContractList", "Contract")
                           .WithSuccess("Contract Created Successful", $"Contract {viewModel.ContractDescription} successfully created");
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

        /// <summary>
        /// Gets the contract products list.
        /// </summary>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetContractProductsList([FromQuery] Guid contractId,
                                                                 CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            ContractModel contract = await this.ApiClient.GetContract(accessToken, this.User.Identity as ClaimsIdentity, contractId, cancellationToken);

            ContractProductListViewModel viewModel = this.ViewModelFactory.ConvertFrom(contract);

            return this.View("ContractProductsList", viewModel);
        }

        /// <summary>
        /// Gets the contract list as json.
        /// </summary>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetContractProductsListAsJson([FromQuery] Guid contractId,
                                                                       CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetContractProductsListAsJson");

            // Search Value from (Search box)  
            String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            Logger.LogDebug($"searchvalue is {searchValue}");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");
            Logger.LogDebug("got access token");

            ContractModel contract = await this.ApiClient.GetContract(accessToken, this.User.Identity as ClaimsIdentity, contractId, cancellationToken);

            ContractProductListViewModel contractProductListViewModel = this.ViewModelFactory.ConvertFrom(contract);

            Logger.LogDebug($"contract product list count is {contractProductListViewModel.ContractProducts.Count}");

            Expression<Func<ContractProductViewModel, Boolean>> whereClause = c => c.ProductName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.DisplayText.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

            DataTablesResult<ContractProductViewModel> dataTableResult =
                Helpers.GetDataForDataTable(this.Request.Form, contractProductListViewModel.ContractProducts, whereClause);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, contractProductListViewModel.ContractProducts, whereClause));
        }

        /// <summary>
        /// Gets the contract product transaction fees list.
        /// </summary>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetContractProductTransactionFeesList([FromQuery] Guid contractId,
                                                                               [FromQuery] Guid contractProductId,
                                                                               CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            ContractProductModel contractProduct =
                await this.ApiClient.GetContractProduct(accessToken, this.User.Identity as ClaimsIdentity, contractId, contractProductId, cancellationToken);

            ContractProductTransactionFeesListViewModel viewModel = this.ViewModelFactory.ConvertFrom(contractProduct);

            return this.View("ContractProductTransactionFeesList", viewModel);
        }

        /// <summary>
        /// Gets the contract product transaction fees list as json.
        /// </summary>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetContractProductTransactionFeesListAsJson([FromQuery] Guid contractId,
                                                                                     [FromQuery] Guid contractProductId,
                                                                                     CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetContractProductTransactionFeeListAsJson");

            // Search Value from (Search box)  
            String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            Logger.LogDebug($"searchvalue is {searchValue}");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");
            Logger.LogDebug("got access token");

            ContractProductModel contractProduct =
                await this.ApiClient.GetContractProduct(accessToken, this.User.Identity as ClaimsIdentity, contractId, contractProductId, cancellationToken);

            ContractProductTransactionFeesListViewModel contractProductTransactionFeesViewModel = this.ViewModelFactory.ConvertFrom(contractProduct);

            Logger.LogDebug($"contract product transaction fee list count is {contractProductTransactionFeesViewModel.TransactionFees.Count}");

            Expression<Func<ContractProductTransactionFeesViewModel, Boolean>> whereClause =
                c => c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                     c.CalculationType.Contains(searchValue, StringComparison.OrdinalIgnoreCase) || c.FeeType.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                     c.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

            DataTablesResult<ContractProductTransactionFeesViewModel> dataTableResult =
                Helpers.GetDataForDataTable(this.Request.Form, contractProductTransactionFeesViewModel.TransactionFees, whereClause);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(Helpers.GetDataForDataTable(this.Request.Form, contractProductTransactionFeesViewModel.TransactionFees, whereClause));
        }

        /// <summary>
        /// Gets the operator list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(CreateContractViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        #endregion
    }
}