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

        
        #endregion

        #region Constructors
        
        public ContractController(IApiClient apiClient)
        {
            this.ApiClient = apiClient;
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
                try
                {
                    String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                    CreateContractModel createContractModel = ViewModelFactory.ConvertFrom(viewModel);

                    Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                    // All good with model, call the client to create the golf club
                    CreateContractResponseModel createContractResponseModel =
                        await this.ApiClient.CreateContract(accessToken, Guid.Empty,
                                                            estateId, createContractModel, cancellationToken);

                    // Merchant Created, redirect to the Merchant List screen
                    return this.RedirectToAction("GetContractList", "Contract")
                               .WithSuccess("Contract Created Successful", $"Contract {viewModel.ContractDescription} successfully created");
                }
                catch(Exception ex)
                {
                    // Something went wrong creating the contract
                    return this.View("CreateContract").WithWarning("New Contract", Helpers.BuildUserErrorMessage("Error creating the contract"));
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View("CreateContract", viewModel);
        }

        /// <summary>
        /// Creates the contract.
        /// </summary>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateContractProduct([FromQuery] Guid contractId,
                                                               CancellationToken cancellationToken)
        {
            CreateContractProductViewModel viewModel = new CreateContractProductViewModel();
            viewModel.ContractId = contractId;

            return this.View("CreateContractProduct", viewModel);
        }

        /// <summary>
        /// Creates the contract.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContractProduct(CreateContractProductViewModel viewModel,
                                                               CancellationToken cancellationToken)
        {
            // Validate the model
            if (this.ValidateModel(viewModel))
            {
                try
                {
                    String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                    Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                    AddProductToContractModel addProductToContractModel = ViewModelFactory.ConvertFrom(viewModel);

                    // All good with model, call the client
                    AddProductToContractResponseModel addProductToContractResponse =
                        await this.ApiClient.AddProductToContract(accessToken,
                                                                  Guid.Empty,
                                                                  estateId,
                                                                  viewModel.ContractId,
                                                                  addProductToContractModel,
                                                                  cancellationToken);

                    // Product Created, redirect to the Product List screen
                    return this.RedirectToAction("GetContractProductsList",
                                                 "Contract",
                                                 new
                                                 {
                                                     contractId = addProductToContractResponse.ContractId
                                                 }).WithSuccess("Product Created Successful", $"Contract Product {viewModel.ProductName} successfully created");
                }
                catch(Exception ex)
                {
                    // Something went wrong creating the Product
                    return this.View("CreateContractProduct").WithWarning("New Contract Product", Helpers.BuildUserErrorMessage("Error creating the contract product"));
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View("CreateContractProduct", viewModel);
        }

        /// <summary>
        /// Creates the contract product transaction fee.
        /// </summary>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateContractProductTransactionFee([FromQuery] Guid contractId,
                                                                             [FromQuery] Guid contractProductId,
                                                                             CancellationToken cancellationToken)
        {
            CreateContractProductTransactionFeeViewModel viewModel = new CreateContractProductTransactionFeeViewModel();
            viewModel.ContractId = contractId;
            viewModel.ContractProductId = contractProductId;

            return this.View("CreateContractProductTransactionFee", viewModel);
        }

        /// <summary>
        /// Creates the contract product transaction fee.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContractProductTransactionFee(CreateContractProductTransactionFeeViewModel viewModel,
                                                                             CancellationToken cancellationToken)
        {
            // Validate the model
            if (this.ValidateModel(viewModel))
            {
                try
                {
                    String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                    Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                    AddTransactionFeeToContractProductModel addTransactionFeeToContractProductModel = ViewModelFactory.ConvertFrom(viewModel);

                    // All good with model, call the client
                    AddTransactionFeeToContractProductResponseModel addTransactionFeeToContractProductResponse =
                        await this.ApiClient.AddTransactionFeeToContractProduct(accessToken,
                                                                                Guid.Empty,
                                                                                estateId,
                                                                                viewModel.ContractId,
                                                                                viewModel.ContractProductId,
                                                                                addTransactionFeeToContractProductModel,
                                                                                cancellationToken);

                    // Transaction Fee Created, redirect to the Transaction Fee List screen
                    return this.RedirectToAction("GetContractProductTransactionFeesList",
                                                 "Contract",
                                                 new
                                                 {
                                                     contractId = addTransactionFeeToContractProductResponse.ContractId,
                                                     contractProductId = addTransactionFeeToContractProductResponse.ProductId
                                                 }).WithSuccess("Transaction Fee Created Successful", $"Transaction Fee {viewModel.FeeDescription} successfully created");
                }
                catch(Exception ex)
                {
                    // Something went wrong creating the Product
                    return this.View("CreateContractProductTransactionFee").WithWarning("New Contract Product Transaction Fee",
                                                                                        Helpers
                                                                                            .BuildUserErrorMessage("Error creating the contract product transaction fee"));
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View("CreateContractProductTransactionFee", viewModel);
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
            try
            {
                Logger.LogDebug("In method GetContractListAsJson");

                // Search Value from (Search box)  
                String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                List<ContractModel> contractList = await this.ApiClient.GetContracts(accessToken, Guid.Empty,
                                                                                     estateId, cancellationToken);

                List<ContractListViewModel> contractViewModels = ViewModelFactory.ConvertFrom(contractList);

                Logger.LogDebug($"contract list count is {contractViewModels.Count}");

                Expression<Func<ContractListViewModel, Boolean>> whereClause = c => c.OperatorName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                    c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

                DataTablesResult<ContractListViewModel> dataTableResult = Helpers.GetDataForDataTable(this.Request.Form, contractViewModels, whereClause);

                String jsonResult = JsonConvert.SerializeObject(dataTableResult);
                Logger.LogDebug(jsonResult);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, contractViewModels, whereClause));
            }
            catch(Exception ex)
            {
                return this.Json(Helpers.GetErrorDataForDataTable<String>(Helpers.BuildUserErrorMessage("Error getting contract list")));
            }
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
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                ContractModel contract = await this.ApiClient.GetContract(accessToken, Guid.Empty,
                                                                          estateId, contractId, cancellationToken);

                ContractProductListViewModel viewModel = ViewModelFactory.ConvertFrom(contract);

                return this.View("ContractProductsList", viewModel);
            }
            catch(Exception e)
            {
                return this.View("ContractProductsList", new ContractProductListViewModel())
                           .WithWarning("Contract Products", Helpers.BuildUserErrorMessage("Error getting a list of products for Contract"));
            }
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
            try
            {
                // Search Value from (Search box)  
                String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();

                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                ContractModel contract = await this.ApiClient.GetContract(accessToken, Guid.Empty,
                                                                          estateId, contractId, cancellationToken);

                ContractProductListViewModel contractProductListViewModel = ViewModelFactory.ConvertFrom(contract);

                Expression<Func<ContractProductViewModel, Boolean>> whereClause = c => c.ProductName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                       c.DisplayText.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, contractProductListViewModel.ContractProducts, whereClause));
            }
            catch(Exception e)
            {
                return this.Json(Helpers.GetErrorDataForDataTable<String>(Helpers.BuildUserErrorMessage("Error getting contract product list")));
            }
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
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                ContractProductModel contractProduct =
                    await this.ApiClient.GetContractProduct(accessToken, Guid.Empty,
                                                            estateId, contractId, contractProductId, cancellationToken);

                ContractProductTransactionFeesListViewModel viewModel = ViewModelFactory.ConvertFrom(contractProduct);

                return this.View("ContractProductTransactionFeesList", viewModel);
            }
            catch(Exception e)
            {
                return this.View("ContractProductsList", new ContractProductListViewModel())
                           .WithWarning("Contract Product Transaction Fees", Helpers.BuildUserErrorMessage("Error getting a list of transaction fees for product"));
            }
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
            try
            {
                // Search Value from (Search box)  
                String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();

                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                ContractProductModel contractProduct =
                    await this.ApiClient.GetContractProduct(accessToken, Guid.Empty,
                                                            estateId, contractId, contractProductId, cancellationToken);

                ContractProductTransactionFeesListViewModel contractProductTransactionFeesViewModel = ViewModelFactory.ConvertFrom(contractProduct);

                Expression<Func<ContractProductTransactionFeesViewModel, Boolean>> whereClause =
                    c => c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                         c.CalculationType.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                         c.FeeType.Contains(searchValue, StringComparison.OrdinalIgnoreCase) || c.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

                return this.Json(Helpers.GetDataForDataTable(this.Request.Form, contractProductTransactionFeesViewModel.TransactionFees, whereClause));
            }
            catch(Exception e)
            {
                return this.Json(Helpers.GetErrorDataForDataTable<String>(Helpers.BuildUserErrorMessage("Error getting contract product transaction fee list")));
            }
        }

        /// <summary>
        /// Gets the operator list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOperatorListAsJson(CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

                EstateModel estate = await this.ApiClient.GetEstate(accessToken, Guid.Empty,
                                                                    estateId, cancellationToken);

                List<OperatorListViewModel> operatorViewModels = ViewModelFactory.ConvertFrom(estate.EstateId, estate.Operators);

                return this.Json(operatorViewModels);
            }
            catch(Exception e)
            {
                return this.Json(Helpers.GetErrorDataForDataTable<String>(Helpers.BuildUserErrorMessage("Error getting operator list")));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetContractProductTypeListAsJson(CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                List<ContractProductTypeModel> contractProductTypeList= await this.ApiClient.GetContractProductTypeList(accessToken, cancellationToken);

                List<ContractProductTypeViewModel> contractProductTypeViewModels = ViewModelFactory.ConvertFrom(contractProductTypeList);

                return this.Json(contractProductTypeViewModels);
            }
            catch (Exception e)
            {
                return this.Json(Helpers.GetErrorDataForDataTable<String>(Helpers.BuildUserErrorMessage("Error getting contract product type list")));
            }
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

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(CreateContractProductViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        private Boolean ValidateModel(CreateContractProductTransactionFeeViewModel viewModel)
        {
            return this.ModelState.IsValid;
        }

        #endregion
    }
}