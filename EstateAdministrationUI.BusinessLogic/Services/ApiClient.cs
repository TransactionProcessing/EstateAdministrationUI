namespace EstateAdministrationUI.Services{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Factories;
    using BusinessLogic.Models;
    using EstateManagement.Client;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using FileProcessor.Client;
    using FileProcessor.DataTransferObjects;
    using FileProcessor.DataTransferObjects.Responses;
    using Shared.Logger;
    using TransactionProcessor.Client;
    using TransactionProcessor.DataTransferObjects;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateAdministrationUI.Services.IApiClient" />
    [ExcludeFromCodeCoverage]
    public class ApiClient : IApiClient{
        #region Fields

        private readonly IEstateClient EstateClient;

        private readonly IFileProcessorClient FileProcessorClient;

        private readonly ITransactionProcessorClient TransactionProcessorClient;

        #endregion

        #region Constructors

        public ApiClient(IEstateClient estateClient,
                         IFileProcessorClient fileProcessorClient,
                         ITransactionProcessorClient transactionProcessorClient){
            this.EstateClient = estateClient;
            this.FileProcessorClient = fileProcessorClient;
            this.TransactionProcessorClient = transactionProcessorClient;
        }

        #endregion

        #region Methods

        public async Task<AddMerchantDeviceResponseModel> AddDeviceToMerchant(String accessToken,
                                                                              Guid actionId,
                                                                              Guid estateId,
                                                                              Guid merchantId,
                                                                              AddMerchantDeviceModel merchantDeviceModel,
                                                                              CancellationToken cancellationToken){
            try{
                AddMerchantDeviceRequest apiRequest = ModelFactory.ConvertFrom(merchantDeviceModel);

                AddMerchantDeviceResponse apiResponse = await this.EstateClient.AddDeviceToMerchant(accessToken, estateId, merchantId, apiRequest, cancellationToken);

                AddMerchantDeviceResponseModel addMerchantDeviceResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return addMerchantDeviceResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<AddProductToContractResponseModel> AddProductToContract(String accessToken,
                                                                                  Guid actionId,
                                                                                  Guid estateId,
                                                                                  Guid contractId,
                                                                                  AddProductToContractModel addProductToContractModel,
                                                                                  CancellationToken cancellationToken){
            try{
                AddProductToContractRequest apiRequest = ModelFactory.ConvertFrom(addProductToContractModel);

                AddProductToContractResponse apiResponse = await this.EstateClient.AddProductToContract(accessToken, estateId, contractId, apiRequest, cancellationToken);

                AddProductToContractResponseModel addProductToContractResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return addProductToContractResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<AddTransactionFeeToContractProductResponseModel> AddTransactionFeeToContractProduct(String accessToken,
                                                                                                              Guid actionId,
                                                                                                              Guid estateId,
                                                                                                              Guid contractId,
                                                                                                              Guid contractProductId,
                                                                                                              AddTransactionFeeToContractProductModel
                                                                                                                  addTransactionFeeToContractProductModel,
                                                                                                              CancellationToken cancellationToken){
            try{
                AddTransactionFeeForProductToContractRequest apiRequest = ModelFactory.ConvertFrom(addTransactionFeeToContractProductModel);

                AddTransactionFeeForProductToContractResponse apiResponse =
                    await this.EstateClient.AddTransactionFeeForProductToContract(accessToken, estateId, contractId, contractProductId, apiRequest, cancellationToken);

                AddTransactionFeeToContractProductResponseModel addTransactionFeeToContractProductResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return addTransactionFeeToContractProductResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<AssignOperatorToMerchantResponseModel> AssignOperatorToMerchant(String accessToken,
                                                                                          Guid actionId,
                                                                                          Guid estateId,
                                                                                          Guid merchantId,
                                                                                          AssignOperatorToMerchantModel assignOperatorToMerchantModel,
                                                                                          CancellationToken cancellationToken){
            try{
                AssignOperatorRequest apiRequest = ModelFactory.ConvertFrom(assignOperatorToMerchantModel);

                AssignOperatorResponse apiResponse = await this.EstateClient.AssignOperatorToMerchant(accessToken, estateId, merchantId, apiRequest, cancellationToken);

                AssignOperatorToMerchantResponseModel assignOperatorToMerchantResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return assignOperatorToMerchantResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<CreateContractResponseModel> CreateContract(String accessToken,
                                                                      Guid actionId,
                                                                      Guid estateId,
                                                                      CreateContractModel createContractModel,
                                                                      CancellationToken cancellationToken){
            try{
                CreateContractRequest apiRequest = ModelFactory.ConvertFrom(createContractModel);

                CreateContractResponse apiResponse = await this.EstateClient.CreateContract(accessToken, estateId, apiRequest, cancellationToken);

                CreateContractResponseModel createContractResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return createContractResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<CreateMerchantResponseModel> CreateMerchant(String accessToken,
                                                                      Guid actionId,
                                                                      Guid estateId,
                                                                      CreateMerchantModel createMerchantModel,
                                                                      CancellationToken cancellationToken){
            try{
                CreateMerchantRequest apiRequest = ModelFactory.ConvertFrom(createMerchantModel);

                CreateMerchantResponse apiResponse = await this.EstateClient.CreateMerchant(accessToken, estateId, apiRequest, cancellationToken);

                CreateMerchantResponseModel createMerchantResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return createMerchantResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<CreateOperatorResponseModel> CreateOperator(String accessToken,
                                                                      Guid actionId,
                                                                      Guid estateId,
                                                                      CreateOperatorModel createOperatorModel,
                                                                      CancellationToken cancellationToken){
            try{
                CreateOperatorRequest apiRequest = ModelFactory.ConvertFrom(createOperatorModel);

                CreateOperatorResponse apiResponse = await this.EstateClient.CreateOperator(accessToken, estateId, apiRequest, cancellationToken);

                CreateOperatorResponseModel createOperatorResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return createOperatorResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<ContractModel> GetContract(String accessToken,
                                                     Guid actionId,
                                                     Guid estateId,
                                                     Guid contractId,
                                                     CancellationToken cancellationToken){
            try{
                ContractResponse contract = await this.EstateClient.GetContract(accessToken, estateId, contractId, true, true, cancellationToken);

                return ModelFactory.ConvertFrom(contract);
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<ContractProductModel> GetContractProduct(String accessToken,
                                                                   Guid actionId,
                                                                   Guid estateId,
                                                                   Guid contractId,
                                                                   Guid contractProductId,
                                                                   CancellationToken cancellationToken){
            try{
                ContractResponse contract = await this.EstateClient.GetContract(accessToken, estateId, contractId, true, true, cancellationToken);

                ContractModel contractModel = ModelFactory.ConvertFrom(contract);

                return contractModel.ContractProducts.SingleOrDefault(cp => cp.ContractProductId == contractProductId);
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<List<ContractProductTypeModel>> GetContractProductTypeList(String accessToken, CancellationToken cancellationToken){
            return new List<ContractProductTypeModel>{
                                                         new(){
                                                                  Description = "Mobile Topup",
                                                                  ProductType = 1
                                                              },
                                                         new(){
                                                                  Description = "Voucher",
                                                                  ProductType = 2
                                                              },
                                                         new(){
                                                                  Description = "Bill Payment",
                                                                  ProductType = 3
                                                              }
                                                     };
        }

        public async Task<List<ContractModel>> GetContracts(String accessToken,
                                                            Guid actionId,
                                                            Guid estateId,
                                                            CancellationToken cancellationToken){
            try{
                List<ContractResponse> contracts = await this.EstateClient.GetContracts(accessToken, estateId, cancellationToken);

                return ModelFactory.ConvertFrom(contracts);
            }
            catch(Exception ex){
                Logger.LogError(ex);
                throw;
            }
        }

        public async Task<EstateModel> GetEstate(String accessToken,
                                                 Guid actionId,
                                                 Guid estateId,
                                                 CancellationToken cancellationToken){
            try{
                EstateResponse estate = await this.EstateClient.GetEstate(accessToken, estateId, cancellationToken);

                return ModelFactory.ConvertFrom(estate);
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<FileDetailsModel> GetFileDetails(String accessToken,
                                                           Guid actionId,
                                                           Guid estateId,
                                                           Guid fileId,
                                                           CancellationToken cancellationToken){
            try{
                FileDetails fileDetails = await this.FileProcessorClient.GetFile(accessToken, estateId, fileId, cancellationToken);

                FileDetailsModel model = ModelFactory.ConvertFrom(fileDetails);

                return model;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<FileImportLogModel> GetFileImportLog(String accessToken,
                                                               Guid actionId,
                                                               Guid estateId,
                                                               Guid fileImportLogId,
                                                               CancellationToken cancellationToken){
            try{
                FileImportLog fileImportLog = await this.FileProcessorClient.GetFileImportLog(accessToken, fileImportLogId, estateId, null, cancellationToken);

                return ModelFactory.ConvertFrom(fileImportLog);
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<List<FileImportLogModel>> GetFileImportLogs(String accessToken,
                                                                      Guid actionId,
                                                                      Guid estateId,
                                                                      Guid? merchantId,
                                                                      DateTime startDate,
                                                                      DateTime endDate,
                                                                      CancellationToken cancellationToken){
            try{
                FileImportLogList fileImportLogs =
                    await this.FileProcessorClient.GetFileImportLogs(accessToken, estateId, startDate, endDate, merchantId, cancellationToken);

                List<FileImportLogModel> fileImportLogModels = ModelFactory.ConvertFrom(fileImportLogs);

                return fileImportLogModels;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<MerchantModel> GetMerchant(String accessToken,
                                                     Guid actionId,
                                                     Guid estateId,
                                                     Guid merchantId,
                                                     CancellationToken cancellationToken){
            try{
                MerchantResponse merchant = await this.EstateClient.GetMerchant(accessToken, estateId, merchantId, cancellationToken);
                MerchantBalanceResponse merchantBalance = await this.TransactionProcessorClient.GetMerchantBalance(accessToken, estateId, merchantId, cancellationToken);

                return ModelFactory.ConvertFrom(merchant, merchantBalance);
            }
            catch(Exception ex){
                Logger.LogError(ex);
                throw;
            }
        }

        public async Task<MerchantBalanceModel> GetMerchantBalance(String accessToken,
                                                                   Guid actionId,
                                                                   Guid estateId,
                                                                   Guid merchantId,
                                                                   CancellationToken cancellationToken){
            try{
                MerchantBalanceResponse merchantBalance = await this.TransactionProcessorClient.GetMerchantBalance(accessToken, estateId, merchantId, cancellationToken);

                return ModelFactory.ConvertFrom(merchantBalance);
            }
            catch(Exception ex){
                Logger.LogError(ex);
                throw;
            }
        }

        public async Task<List<MerchantBalanceHistory>> GetMerchantBalanceHistory(String accessToken,
                                                                                  Guid actionId,
                                                                                  Guid estateId,
                                                                                  Guid merchantId,
                                                                                  DateTime startDate,
                                                                                  DateTime endDate,
                                                                                  CancellationToken cancellationToken){
            try{
                List<MerchantBalanceChangedEntryResponse> merchantBalanceHistory =
                    await this.TransactionProcessorClient.GetMerchantBalanceHistory(accessToken, estateId, merchantId, startDate, endDate, cancellationToken);

                return ModelFactory.ConvertFrom(merchantBalanceHistory);
            }
            catch(Exception ex){
                Logger.LogError(ex);
                throw;
            }
        }

        public async Task<List<MerchantModel>> GetMerchants(String accessToken,
                                                            Guid actionId,
                                                            Guid estateId,
                                                            CancellationToken cancellationToken){
            try{
                List<MerchantResponse> merchants = await this.EstateClient.GetMerchants(accessToken, estateId, cancellationToken);

                return ModelFactory.ConvertFrom(merchants);
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<MakeMerchantDepositResponseModel> MakeMerchantDeposit(String accessToken,
                                                                                Guid actionId,
                                                                                Guid estateId,
                                                                                Guid merchantId,
                                                                                MakeMerchantDepositModel makeMerchantDepositModel,
                                                                                CancellationToken cancellationToken){
            try{
                MakeMerchantDepositRequest apiRequest = ModelFactory.ConvertFrom(makeMerchantDepositModel);

                MakeMerchantDepositResponse apiResponse = await this.EstateClient.MakeMerchantDeposit(accessToken, estateId, merchantId, apiRequest, cancellationToken);

                MakeMerchantDepositResponseModel makeMerchantDepositResponseModel = ModelFactory.ConvertFrom(apiResponse);

                return makeMerchantDepositResponseModel;
            }
            catch(Exception e){
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<Guid> UploadFile(String accessToken,
                                           Guid actionId,
                                           Guid estateId,
                                           Guid merchantId,
                                           Guid userId,
                                           Guid fileProfileId,
                                           Byte[] fileData,
                                           String fileName,
                                           CancellationToken cancellationToken){
            UploadFileRequest apiRequest = new UploadFileRequest{
                                                                    EstateId = estateId,
                                                                    FileProfileId = fileProfileId,
                                                                    MerchantId = merchantId,
                                                                    UserId = userId
                                                                };

            Guid apiResponse = await this.FileProcessorClient.UploadFile(accessToken, fileName, fileData, apiRequest, cancellationToken);

            return apiResponse;
        }

        #endregion
    }
}