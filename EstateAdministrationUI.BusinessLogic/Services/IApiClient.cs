namespace EstateAdministrationUI.Services{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IApiClient{
        #region Methods

        Task<AddMerchantDeviceResponseModel> AddDeviceToMerchant(String accessToken,
                                                                 Guid actionId,
                                                                 Guid estateId,
                                                                 Guid merchantId,
                                                                 AddMerchantDeviceModel merchantDeviceModel,
                                                                 CancellationToken cancellationToken);

        Task<AddProductToContractResponseModel> AddProductToContract(String accessToken,
                                                                     Guid actionId,
                                                                     Guid estateId,
                                                                     Guid contractId,
                                                                     AddProductToContractModel addProductToContractModel,
                                                                     CancellationToken cancellationToken);

        Task<AddTransactionFeeToContractProductResponseModel> AddTransactionFeeToContractProduct(String accessToken,
                                                                                                 Guid actionId,
                                                                                                 Guid estateId,
                                                                                                 Guid contractId,
                                                                                                 Guid contractProductId,
                                                                                                 AddTransactionFeeToContractProductModel
                                                                                                     addTransactionFeeToContractProductModel,
                                                                                                 CancellationToken cancellationToken);

        Task<AssignOperatorToMerchantResponseModel> AssignOperatorToMerchant(String accessToken,
                                                                             Guid actionId,
                                                                             Guid estateId,
                                                                             Guid merchantId,
                                                                             AssignOperatorToMerchantModel assignOperatorToMerchantModel,
                                                                             CancellationToken cancellationToken);

        Task<CreateContractResponseModel> CreateContract(String accessToken,
                                                         Guid actionId,
                                                         Guid estateId,
                                                         CreateContractModel createContractModel,
                                                         CancellationToken cancellationToken);

        Task<CreateMerchantResponseModel> CreateMerchant(String accessToken,
                                                         Guid actionId,
                                                         Guid estateId,
                                                         CreateMerchantModel createMerchantModel,
                                                         CancellationToken cancellationToken);

        Task<CreateOperatorResponseModel> CreateOperator(String accessToken,
                                                         Guid actionId,
                                                         Guid estateId,
                                                         CreateOperatorModel createOperatorModel,
                                                         CancellationToken cancellationToken);

        Task<ContractModel> GetContract(String accessToken,
                                        Guid actionId,
                                        Guid estateId,
                                        Guid contractId,
                                        CancellationToken cancellationToken);

        Task<ContractProductModel> GetContractProduct(String accessToken,
                                                      Guid actionId,
                                                      Guid estateId,
                                                      Guid contractId,
                                                      Guid contractProductId,
                                                      CancellationToken cancellationToken);

        Task<List<ContractProductTypeModel>> GetContractProductTypeList(String accessToken, CancellationToken cancellationToken);

        Task<List<ContractModel>> GetContracts(String accessToken,
                                               Guid actionId,
                                               Guid estateId,
                                               CancellationToken cancellationToken);

        Task<EstateModel> GetEstate(String accessToken,
                                    Guid actionId,
                                    Guid estateId,
                                    CancellationToken cancellationToken);

        Task<FileDetailsModel> GetFileDetails(String accessToken,
                                              Guid actionId,
                                              Guid estateId,
                                              Guid fileId,
                                              CancellationToken cancellationToken);

        Task<FileImportLogModel> GetFileImportLog(String accessToken,
                                                  Guid actionId,
                                                  Guid estateId,
                                                  Guid fileImportLogId,
                                                  CancellationToken cancellationToken);

        Task<List<FileImportLogModel>> GetFileImportLogs(String accessToken,
                                                         Guid actionId,
                                                         Guid estateId,
                                                         Guid? merchantId,
                                                         DateTime startDate,
                                                         DateTime endDate,
                                                         CancellationToken cancellationToken);

        Task<MerchantModel> GetMerchant(String accessToken,
                                        Guid actionId,
                                        Guid estateId,
                                        Guid merchantId,
                                        CancellationToken cancellationToken);

        Task<MerchantBalanceModel> GetMerchantBalance(String accessToken,
                                                      Guid actionId,
                                                      Guid estateId,
                                                      Guid merchantId,
                                                      CancellationToken cancellationToken);

        Task<List<MerchantBalanceHistory>> GetMerchantBalanceHistory(String accessToken,
                                                                     Guid actionId,
                                                                     Guid estateId,
                                                                     Guid merchantId,
                                                                     DateTime startDate,
                                                                     DateTime endDate,
                                                                     CancellationToken cancellationToken);

        Task<List<MerchantModel>> GetMerchants(String accessToken,
                                               Guid actionId,
                                               Guid estateId,
                                               CancellationToken cancellationToken);

        Task<List<MerchantListModel>> GetMerchantsForReporting(String accessToken,
                                                               Guid estateId,
                                                               CancellationToken cancellationToken);

        Task<List<OperatorListModel>> GetOperatorsForReporting(String accessToken,
                                                               Guid estateId,
                                                               CancellationToken cancellationToken);

        Task<MakeMerchantDepositResponseModel> MakeMerchantDeposit(String accessToken,
                                                                   Guid actionId,
                                                                   Guid estateId,
                                                                   Guid merchantId,
                                                                   MakeMerchantDepositModel makeMerchantDepositModel,
                                                                   CancellationToken cancellationToken);

        Task<Guid> UploadFile(String accessToken,
                              Guid actionId,
                              Guid estateId,
                              Guid merchantId,
                              Guid userId,
                              Guid fileProfileId,
                              Byte[] fileData,
                              String fileName,
                              CancellationToken cancellationToken);


        Task<List<CalendarDateModel>> GetCalendarDates(
            string accessToken,
            Guid estateId,
            int year,
            CancellationToken cancellationToken);

        Task<List<CalendarYearModel>> GetCalendarYears(
            string accessToken,
            Guid estateId,
            CancellationToken cancellationToken);

        Task<List<ComparisonDateModel>> GetComparisonDates(
            string accessToken,
            Guid estateId,
            CancellationToken cancellationToken);

        Task<TodaysSalesModel> GetTodaysSales(
            string accessToken,
            Guid estateId,
            Guid? merchantId,
            Guid? operatorId,
            DateTime comparisonDate,
            CancellationToken cancellationToken);

        Task<List<TodaysSalesCountByHourModel>> GetTodaysSalesCountByHour(
            string accessToken,
            Guid estateId,
            Guid? merchantId,
            Guid? operatorId,
            DateTime comparisonDate,
            CancellationToken cancellationToken);

        Task<List<TodaysSalesValueByHourModel>> GetTodaysSalesValueByHour(
            string accessToken,
            Guid estateId,
            Guid? merchantId,
            Guid? operatorId,
            DateTime comparisonDate,
            CancellationToken cancellationToken);

        Task<TodaysSettlementModel> GetTodaysSettlement(
            string accessToken,
            Guid estateId,
            Guid? merchantId,
            Guid? operatorId,
            DateTime comparisonDate,
            CancellationToken cancellationToken);

        Task<MerchantKpiModel> GetMerchantKpi(
            string accessToken,
            Guid estateId,
            CancellationToken cancellationToken);

        Task<TodaysSalesModel> GetTodaysFailedSales(
            string accessToken,
            Guid estateId,
            string responseCode,
            DateTime comparisonDate,
            CancellationToken cancellationToken);

        Task<List<TopBottomOperatorDataModel>> GetTopBottomOperatorData(
            string accessToken,
            Guid estateId,
            TopBottom topBottom,
            int resultCount,
            CancellationToken cancellationToken);

        Task<List<TopBottomMerchantDataModel>> GetTopBottomMerchantData(
            string accessToken,
            Guid estateId,
            TopBottom topBottom,
            int resultCount,
            CancellationToken cancellationToken);

        Task<List<TopBottomProductDataModel>> GetTopBottomProductData(
            string accessToken,
            Guid estateId,
            TopBottom topBottom,
            int resultCount,
            CancellationToken cancellationToken);

        Task<LastSettlementModel> GetLastSettlement(
            string accessToken,
            Guid estateId,
            Guid? merchantId,
            Guid? operatorId,
            CancellationToken cancellationToken);



        #endregion
    }
}