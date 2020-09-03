namespace EstateAdministrationUI.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Factories;
    using BusinessLogic.Models;
    using EstateManagement.Client;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateAdministrationUI.Services.IApiClient" />
    [ExcludeFromCodeCoverage]
    public class ApiClient : IApiClient
    {
        #region Fields

        /// <summary>
        /// The estate client
        /// </summary>
        private readonly IEstateClient EstateClient;

        /// <summary>
        /// The model factory
        /// </summary>
        private readonly IModelFactory ModelFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="estateClient">The estate client.</param>
        /// <param name="modelFactory">The model factory.</param>
        public ApiClient(IEstateClient estateClient,
                         IModelFactory modelFactory)
        {
            this.EstateClient = estateClient;
            this.ModelFactory = modelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the contract.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="createContractModel">The create contract model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreateContractResponseModel> CreateContract(String accessToken,
                                                                      ClaimsIdentity claimsIdentity,
                                                                      CreateContractModel createContractModel,
                                                                      CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            CreateContractRequest apiRequest = this.ModelFactory.ConvertFrom(createContractModel);

            CreateContractResponse apiResponse = await this.EstateClient.CreateContract(accessToken, estateId, apiRequest, cancellationToken);

            CreateContractResponseModel createContractResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return createContractResponseModel;
        }

        /// <summary>
        /// Creates the merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="createMerchantModel">The create merchant model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreateMerchantResponseModel> CreateMerchant(String accessToken,
                                                                      ClaimsIdentity claimsIdentity,
                                                                      CreateMerchantModel createMerchantModel,
                                                                      CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            CreateMerchantRequest apiRequest = this.ModelFactory.ConvertFrom(createMerchantModel);

            CreateMerchantResponse apiResponse = await this.EstateClient.CreateMerchant(accessToken, estateId, apiRequest, cancellationToken);

            CreateMerchantResponseModel createMerchantResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return createMerchantResponseModel;
        }

        /// <summary>
        /// Creates the operator.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="createOperatorModel">The create operator model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<CreateOperatorResponseModel> CreateOperator(String accessToken,
                                                                      ClaimsIdentity claimsIdentity,
                                                                      CreateOperatorModel createOperatorModel,
                                                                      CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            CreateOperatorRequest apiRequest = this.ModelFactory.ConvertFrom(createOperatorModel);

            CreateOperatorResponse apiResponse = await this.EstateClient.CreateOperator(accessToken, estateId, apiRequest, cancellationToken);

            CreateOperatorResponseModel createOperatorResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return createOperatorResponseModel;
        }

        /// <summary>
        /// Gets the contract.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ContractModel> GetContract(String accessToken,
                                                     ClaimsIdentity claimsIdentity,
                                                     Guid contractId,
                                                     CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            ContractResponse contract = await this.EstateClient.GetContract(accessToken, estateId, contractId, true, true, cancellationToken);

            return this.ModelFactory.ConvertFrom(contract);
        }

        /// <summary>
        /// Gets the contract product.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ContractProductModel> GetContractProduct(String accessToken,
                                                                   ClaimsIdentity claimsIdentity,
                                                                   Guid contractId,
                                                                   Guid contractProductId,
                                                                   CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            ContractResponse contract = await this.EstateClient.GetContract(accessToken, estateId, contractId, true, true, cancellationToken);

            ContractModel contractModel = this.ModelFactory.ConvertFrom(contract);

            return contractModel.ContractProducts.SingleOrDefault(cp => cp.ContractProductId == contractProductId);
        }

        /// <summary>
        /// Gets the contracts.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<ContractModel>> GetContracts(String accessToken,
                                                            ClaimsIdentity claimsIdentity,
                                                            CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            List<ContractResponse> contracts = await this.EstateClient.GetContracts(accessToken, estateId, cancellationToken);

            return this.ModelFactory.ConvertFrom(contracts);
        }

        /// <summary>
        /// Gets the estate.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<EstateModel> GetEstate(String accessToken,
                                                 ClaimsIdentity claimsIdentity,
                                                 CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            EstateResponse estate = await this.EstateClient.GetEstate(accessToken, estateId, cancellationToken);

            return this.ModelFactory.ConvertFrom(estate);
        }

        /// <summary>
        /// Gets the merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<MerchantModel> GetMerchant(String accessToken,
                                                     ClaimsIdentity claimsIdentity,
                                                     Guid merchantId,
                                                     CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            MerchantResponse merchant = await this.EstateClient.GetMerchant(accessToken, estateId, merchantId, cancellationToken);

            return this.ModelFactory.ConvertFrom(merchant);
        }

        /// <summary>
        /// Gets the merchants.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<MerchantModel>> GetMerchants(String accessToken,
                                                            ClaimsIdentity claimsIdentity,
                                                            CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            List<MerchantResponse> merchants = await this.EstateClient.GetMerchants(accessToken, estateId, cancellationToken);

            return this.ModelFactory.ConvertFrom(merchants);
        }

        /// <summary>
        /// Makes the merchant deposit.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="makeMerchantDepositModel">The make merchant deposit model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<MakeMerchantDepositResponseModel> MakeMerchantDeposit(String accessToken,
                                                                                ClaimsIdentity claimsIdentity,
                                                                                Guid merchantId,
                                                                                MakeMerchantDepositModel makeMerchantDepositModel,
                                                                                CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            MakeMerchantDepositRequest apiRequest = this.ModelFactory.ConvertFrom(makeMerchantDepositModel);

            MakeMerchantDepositResponse apiResponse = await this.EstateClient.MakeMerchantDeposit(accessToken, estateId, merchantId, apiRequest, cancellationToken);

            MakeMerchantDepositResponseModel makeMerchantDepositResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return makeMerchantDepositResponseModel;
        }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">User {claimsIdentity.Name} does not have Claim [{claimType}]</exception>
        private static T GetClaimValue<T>(ClaimsIdentity claimsIdentity,
                                          String claimType)
        {
            if (!claimsIdentity.HasClaim(x => x.Type == claimType))
            {
                throw new InvalidOperationException($"User {claimsIdentity.Name} does not have Claim [{claimType}]");
            }

            Claim claim = claimsIdentity.Claims.Single(x => x.Type == claimType);
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
        }

        #endregion
    }
}