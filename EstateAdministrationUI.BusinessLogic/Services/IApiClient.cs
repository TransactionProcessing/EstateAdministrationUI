namespace EstateAdministrationUI.Services{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IApiClient{
        #region Methods

        Task<AddMerchantDeviceResponseModel> AddDeviceToMerchant(String accessToken,
                                                                 ClaimsIdentity claimsIdentity,
                                                                 Guid merchantId,
                                                                 AddMerchantDeviceModel merchantDeviceModel,
                                                                 CancellationToken cancellationToken);

        Task<AddProductToContractResponseModel> AddProductToContract(String accessToken,
                                                                     ClaimsIdentity claimsIdentity,
                                                                     Guid contractId,
                                                                     AddProductToContractModel addProductToContractModel,
                                                                     CancellationToken cancellationToken);

        Task<AddTransactionFeeToContractProductResponseModel> AddTransactionFeeToContractProduct(String accessToken,
                                                                                                 ClaimsIdentity claimsIdentity,
                                                                                                 Guid contractId,
                                                                                                 Guid contractProductId,
                                                                                                 AddTransactionFeeToContractProductModel
                                                                                                     addTransactionFeeToContractProductModel,
                                                                                                 CancellationToken cancellationToken);

        Task<AssignOperatorToMerchantResponseModel> AssignOperatorToMerchant(String accessToken,
                                                                             ClaimsIdentity claimsIdentity,
                                                                             Guid merchantId,
                                                                             AssignOperatorToMerchantModel assignOperatorToMerchantModel,
                                                                             CancellationToken cancellationToken);

        Task<CreateContractResponseModel> CreateContract(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         CreateContractModel createContractModel,
                                                         CancellationToken cancellationToken);

        Task<CreateMerchantResponseModel> CreateMerchant(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         CreateMerchantModel createMerchantModel,
                                                         CancellationToken cancellationToken);

        Task<CreateOperatorResponseModel> CreateOperator(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         CreateOperatorModel createOperatorModel,
                                                         CancellationToken cancellationToken);

        Task<ContractModel> GetContract(String accessToken,
                                        ClaimsIdentity claimsIdentity,
                                        Guid contractId,
                                        CancellationToken cancellationToken);

        Task<ContractProductModel> GetContractProduct(String accessToken,
                                                      ClaimsIdentity claimsIdentity,
                                                      Guid contractId,
                                                      Guid contractProductId,
                                                      CancellationToken cancellationToken);

        Task<List<ContractProductTypeModel>> GetContractProductTypeList(String accessToken, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken);

        Task<List<ContractModel>> GetContracts(String accessToken,
                                               ClaimsIdentity claimsIdentity,
                                               CancellationToken cancellationToken);

        Task<EstateModel> GetEstate(String accessToken,
                                    ClaimsIdentity claimsIdentity,
                                    CancellationToken cancellationToken);

        Task<FileDetailsModel> GetFileDetails(String accessToken,
                                              ClaimsIdentity claimsIdentity,
                                              Guid fileId,
                                              CancellationToken cancellationToken);

        Task<FileImportLogModel> GetFileImportLog(String accessToken,
                                                  ClaimsIdentity claimsIdentity,
                                                  Guid fileImportLogId,
                                                  CancellationToken cancellationToken);

        Task<List<FileImportLogModel>> GetFileImportLogs(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         Guid? merchantId,
                                                         DateTime startDate,
                                                         DateTime endDate,
                                                         CancellationToken cancellationToken);

        Task<MerchantModel> GetMerchant(String accessToken,
                                        ClaimsIdentity claimsIdentity,
                                        Guid merchantId,
                                        CancellationToken cancellationToken);

        Task<MerchantBalanceModel> GetMerchantBalance(String accessToken,
                                                      ClaimsIdentity claimsIdentity,
                                                      Guid merchantId,
                                                      CancellationToken cancellationToken);

        Task<List<MerchantBalanceHistory>> GetMerchantBalanceHistory(String accessToken,
                                                                     ClaimsIdentity claimsIdentity,
                                                                     Guid merchantId,
                                                                     DateTime startDate,
                                                                     DateTime endDate,
                                                                     CancellationToken cancellationToken);

        Task<List<MerchantModel>> GetMerchants(String accessToken,
                                               ClaimsIdentity claimsIdentity,
                                               CancellationToken cancellationToken);

        Task<MakeMerchantDepositResponseModel> MakeMerchantDeposit(String accessToken,
                                                                   ClaimsIdentity claimsIdentity,
                                                                   Guid merchantId,
                                                                   MakeMerchantDepositModel makeMerchantDepositModel,
                                                                   CancellationToken cancellationToken);

        Task<Guid> UploadFile(String accessToken,
                              ClaimsIdentity claimsIdentity,
                              Guid merchantId,
                              Guid fileProfileId,
                              Byte[] fileData,
                              String fileName,
                              CancellationToken cancellationToken);

        #endregion
    }
}