namespace EstateAdministrationUI.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Common;
    using BusinessLogic.Factories;
    using BusinessLogic.Models;
    using EstateManagement.Client;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using EstateReporting.Client;
    using EstateReporting.DataTransferObjects;
    using FileProcessor.Client;
    using FileProcessor.DataTransferObjects;
    using FileProcessor.DataTransferObjects.Responses;
    using Shared.Logger;
    using SortDirection = BusinessLogic.Models.SortDirection;
    using SortField = BusinessLogic.Models.SortField;

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
        /// The estate reporting client
        /// </summary>
        private readonly IEstateReportingClient EstateReportingClient;

        /// <summary>
        /// The file processor client
        /// </summary>
        private readonly IFileProcessorClient FileProcessorClient;

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
        /// <param name="estateReportingClient">The estate reporting client.</param>
        /// <param name="fileProcessorClient">The file processor client.</param>
        /// <param name="modelFactory">The model factory.</param>
        public ApiClient(IEstateClient estateClient,
                         IEstateReportingClient estateReportingClient,
                         IFileProcessorClient fileProcessorClient,
                         IModelFactory modelFactory)
        {
            this.EstateClient = estateClient;
            this.EstateReportingClient = estateReportingClient;
            this.FileProcessorClient = fileProcessorClient;
            this.ModelFactory = modelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The estate identifier claim type
        /// </summary>
        private const String EstateIdClaimType = "estateId";

        public async Task<Guid> UploadFile(String accessToken,
                                     ClaimsIdentity claimsIdentity,
                                     Guid merchantId,
                                     Guid fileProfileId,
                                     Byte[] fileData,
                                     String fileName,
                                     CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);
            Guid userId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "sub");

            UploadFileRequest request = new UploadFileRequest
                                        {
                                            EstateId = estateId,
                                            FileProfileId = fileProfileId,
                                            MerchantId = merchantId,
                                            UserId = userId
                                        };

            var response = await this.FileProcessorClient.UploadFile(accessToken, fileName, fileData, request, cancellationToken);

            return response;
        }

        public async Task<AddMerchantDeviceResponseModel> AddDeviceToMerchant(String accessToken,
                                                                              ClaimsIdentity claimsIdentity,
                                                                              Guid merchantId,
                                                                              AddMerchantDeviceModel merchantDeviceModel,
                                                                              CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            AddMerchantDeviceRequest apiRequest = this.ModelFactory.ConvertFrom(merchantDeviceModel);

            AddMerchantDeviceResponse apiResponse = await this.EstateClient.AddDeviceToMerchant(accessToken, estateId, merchantId, apiRequest, cancellationToken);

            AddMerchantDeviceResponseModel addMerchantDeviceResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return addMerchantDeviceResponseModel;
        }

        /// <summary>
        /// Adds the product to contract.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="addProductToContractModel">The add product to contract model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AddProductToContractResponseModel> AddProductToContract(String accessToken,
                                                                                  ClaimsIdentity claimsIdentity,
                                                                                  Guid contractId,
                                                                                  AddProductToContractModel addProductToContractModel,
                                                                                  CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);
            
            AddProductToContractRequest apiRequest = this.ModelFactory.ConvertFrom(addProductToContractModel);

            AddProductToContractResponse apiResponse = await this.EstateClient.AddProductToContract(accessToken, estateId, contractId, apiRequest, cancellationToken);

            AddProductToContractResponseModel addProductToContractResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return addProductToContractResponseModel;
        }

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            EstateResponse estate = await this.EstateClient.GetEstate(accessToken, estateId, cancellationToken);

            return this.ModelFactory.ConvertFrom(estate);
        }

        /// <summary>
        /// Gets the file import log.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="fileImportLogId">The file import log identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<FileImportLogModel> GetFileImportLog(String accessToken,
                                                               ClaimsIdentity claimsIdentity,
                                                               Guid fileImportLogId,
                                                               CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            FileImportLog fileImportLog = await this.FileProcessorClient.GetFileImportLog(accessToken, fileImportLogId, estateId, null, cancellationToken);

            return this.ModelFactory.ConvertFrom(fileImportLog);
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
            try
            {
                Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

                MerchantResponse merchant = await this.EstateClient.GetMerchant(accessToken, estateId, merchantId, cancellationToken);

                return this.ModelFactory.ConvertFrom(merchant);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public async Task<MerchantBalanceModel> GetMerchantBalance(String accessToken,
                                                                   ClaimsIdentity claimsIdentity,
                                                                   Guid merchantId,
                                                                   CancellationToken cancellationToken)
        {
            try
            {
                Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

                MerchantBalanceResponse merchantBalance = await this.EstateClient.GetMerchantBalance(accessToken, estateId, merchantId, cancellationToken);

                return this.ModelFactory.ConvertFrom(merchantBalance);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the merchant balance history.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<MerchantBalanceHistory>> GetMerchantBalanceHistory(String accessToken,
                                      ClaimsIdentity claimsIdentity,
                                      Guid merchantId,
                                      DateTime startDate,
                                      DateTime endDate,
                                      CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            try
            {
                List<MerchantBalanceHistoryResponse> merchantBalanceHistory =
                    await this.EstateClient.GetMerchantBalanceHistory(accessToken, estateId, merchantId, cancellationToken);

                // Filter the data on dates
                merchantBalanceHistory = merchantBalanceHistory.Where(m => m.EntryDateTime.Date >= startDate && m.EntryDateTime.Date <= endDate).ToList();

                return this.ModelFactory.ConvertFrom(merchantBalanceHistory);
            }
            catch(Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(KeyNotFoundException))
                {
                    return new List<MerchantBalanceHistory>();
                }

                throw;
            }
            
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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

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
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            MakeMerchantDepositRequest apiRequest = this.ModelFactory.ConvertFrom(makeMerchantDepositModel);

            MakeMerchantDepositResponse apiResponse = await this.EstateClient.MakeMerchantDeposit(accessToken, estateId, merchantId, apiRequest, cancellationToken);

            MakeMerchantDepositResponseModel makeMerchantDepositResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return makeMerchantDepositResponseModel;
        }

        /// <summary>
        /// Gets the file details.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<FileDetailsModel> GetFileDetails(String accessToken,
                                                          ClaimsIdentity claimsIdentity,
                                                          Guid fileId,
                                                          CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            FileDetails fileDetails = await this.FileProcessorClient.GetFile(accessToken, estateId, fileId, cancellationToken);

            FileDetailsModel model = this.ModelFactory.ConvertFrom(fileDetails);

            return model;
        }

        /// <summary>
        /// Gets the file import logs.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<FileImportLogModel>> GetFileImportLogs(String accessToken,
                                                                      ClaimsIdentity claimsIdentity,
                                                                      Guid? merchantId,
                                                                      DateTime startDate,
                                                                      DateTime endDate,
                                                                      CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            FileImportLogList fileImportLogs = await this.FileProcessorClient.GetFileImportLogs(accessToken, estateId, startDate, endDate, merchantId, cancellationToken);

            List<FileImportLogModel> fileImportLogModels = this.ModelFactory.ConvertFrom(fileImportLogs);

            return fileImportLogModels;
        }

        /// <summary>
        /// Adds the transaction fee to contract product.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="addTransactionFeeToContractProductModel">The add transaction fee to contract product model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AddTransactionFeeToContractProductResponseModel> AddTransactionFeeToContractProduct(String accessToken,
                                                                                                              ClaimsIdentity claimsIdentity,
                                                                                                              Guid contractId,
                                                                                                              Guid contractProductId,
                                                                                                              AddTransactionFeeToContractProductModel addTransactionFeeToContractProductModel,
                                                                                                              CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            AddTransactionFeeForProductToContractRequest apiRequest = this.ModelFactory.ConvertFrom(addTransactionFeeToContractProductModel);

            AddTransactionFeeForProductToContractResponse apiResponse = await this.EstateClient.AddTransactionFeeForProductToContract(accessToken, estateId, contractId, contractProductId, apiRequest, cancellationToken);

            AddTransactionFeeToContractProductResponseModel addTransactionFeeToContractProductResponseModel = this.ModelFactory.ConvertFrom(apiResponse);

            return addTransactionFeeToContractProductResponseModel;
        }


        /// <summary>
        /// Gets the transactions for date period.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="datePeriod">The date period.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionForPeriodModel> GetTransactionsForDatePeriod(String accessToken,
                                                                               ClaimsIdentity claimsIdentity, 
                                                                               DatePeriod datePeriod,
                                                                               CancellationToken cancellationToken)
        {
            
            // Work out the date range required
            (DateTime startDate, DateTime endDate) dateRange = this.CalculateDateRange(datePeriod);
            

            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            TransactionsByDayResponse response = await this.EstateReportingClient.GetTransactionsForEstateByDate(accessToken,
                                                                                                                     estateId,
                                                                                                                     dateRange.startDate.ToString("yyyyMMdd"),
                                                                                                                     dateRange.endDate.ToString("yyyyMMdd"),
                                                                                                                     cancellationToken);
            TransactionForPeriodModel model = this.ModelFactory.ConvertToPeriodModel(response);

            return model;
        }

        /// <summary>
        /// Gets the transactions by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDateModel> GetTransactionsByDate(String accessToken,
                                                                         ClaimsIdentity claimsIdentity,
                                                                         DateTime startDate,
                                                                         DateTime endDate,
                                                                         CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            TransactionsByDayResponse response = await this.EstateReportingClient.GetTransactionsForEstateByDate(accessToken,
                                                                                                                     estateId,
                                                                                                                     startDate.ToString("yyyyMMdd"),
                                                                                                                     endDate.ToString("yyyyMMdd"),
                                                                                                                     cancellationToken);
            TransactionsByDateModel model = this.ModelFactory.ConvertFrom(response);

            return model;
        }

        /// <summary>
        /// Gets the transactions by week.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByWeekModel> GetTransactionsByWeek(String accessToken,
                                                                         ClaimsIdentity claimsIdentity,
                                                                         DateTime startDate,
                                                                         DateTime endDate,
                                                                         CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            TransactionsByWeekResponse response = await this.EstateReportingClient.GetTransactionsForEstateByWeek(accessToken,
                                                                                                                     estateId,
                                                                                                                     startDate.ToString("yyyyMMdd"),
                                                                                                                     endDate.ToString("yyyyMMdd"),
                                                                                                                     cancellationToken);
            TransactionsByWeekModel model = this.ModelFactory.ConvertFrom(response);

            return model;
        }

        /// <summary>
        /// Gets the transactions by month.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMonthModel> GetTransactionsByMonth(String accessToken,
                                                                           ClaimsIdentity claimsIdentity,
                                                                           DateTime startDate,
                                                                           DateTime endDate,
                                                                           CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            TransactionsByMonthResponse response = await this.EstateReportingClient.GetTransactionsForEstateByMonth(accessToken,
                estateId,
                startDate.ToString("yyyyMMdd"),
                endDate.ToString("yyyyMMdd"),
                cancellationToken);

            TransactionsByMonthModel model = this.ModelFactory.ConvertFrom(response);

            return model;
        }

        /// <summary>
        /// Gets the transactions by merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMerchantModel> GetTransactionsByMerchant(String accessToken,
                                                                                 ClaimsIdentity claimsIdentity,
                                                                                 DateTime startDate,
                                                                                 DateTime endDate,
                                                                                 Int32 recordCount,
                                                                                 SortDirection sortDirection,
                                                                                 SortField sortField,
                                                                                 CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);
            
            TransactionsByMerchantResponse response =
                await this.EstateReportingClient.GetTransactionsForEstateByMerchant(accessToken,
                                                                                    estateId,
                                                                                    startDate.ToString("yyyyMMdd"),
                                                                                    endDate.ToString("yyyyMMdd"),
                                                                                    recordCount,
                                                                                    this.ModelFactory.ConvertFrom(sortDirection),
                                                                                    this.ModelFactory.ConvertFrom(sortField),
                                                                                    cancellationToken);

            TransactionsByMerchantModel model = this.ModelFactory.ConvertFrom(response);

            return model;

        }

        /// <summary>
        /// Gets the transactions by operator.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByOperatorModel> GetTransactionsByOperator(String accessToken,
                                                                                 ClaimsIdentity claimsIdentity,
                                                                                 DateTime startDate,
                                                                                 DateTime endDate,
                                                                                 Int32 recordCount,
                                                                                 SortDirection sortDirection,
                                                                                 SortField sortField,
                                                                                 CancellationToken cancellationToken)
        {
            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, EstateIdClaimType);

            TransactionsByOperatorResponse response = await this.EstateReportingClient.GetTransactionsForEstateByOperator(accessToken,
                                                                                              estateId,
                                                                                              startDate.ToString("yyyyMMdd"),
                                                                                              endDate.ToString("yyyyMMdd"),
                                                                                              recordCount,
                                                                                              this.ModelFactory.ConvertFrom(sortDirection),
                                                                                              this.ModelFactory.ConvertFrom(sortField),
                                                                                              cancellationToken);

            TransactionsByOperatorModel model = this.ModelFactory.ConvertFrom(response);

            return model;
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
            if (!claimsIdentity.HasClaim(x => x.Type.ToLower() == claimType.ToLower()))
            {
                throw new InvalidOperationException($"User {claimsIdentity.Name} does not have Claim [{claimType}]");
            }

            Claim claim = claimsIdentity.Claims.Single(x => x.Type.ToLower() == claimType.ToLower());
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
        }

        #endregion

        /// <summary>
        /// Calculates the date range.
        /// </summary>
        /// <param name="datePeriod">The date period.</param>
        /// <param name="today">The today.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        private (DateTime startDate, DateTime endDate) CalculateDateRange(DatePeriod datePeriod,
                                                                          DateTime? today = null,
                                                                          DayOfWeek firstDayOfWeek = DayOfWeek.Sunday)
        {
            if (today == null)
            {
                today=DateTime.Today;
            }

            if (datePeriod == DatePeriod.Today)
            {
                return (today.Value, today.Value);
            }
            else if (datePeriod == DatePeriod.ThisWeek)
            {
                return (today.Value.StartOfWeek(firstDayOfWeek), today.Value);
            }
            else
            {
                return (new DateTime(today.Value.Year, today.Value.Month, 1), today.Value);
            }
        }
    }
}