namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System.Collections.Generic;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using EstateReporting.DataTransferObjects;
    using FileProcessor.DataTransferObjects.Responses;
    using Models;
    using SortDirectionModel = Models.SortDirection;
    using SortDirectionDTO = EstateReporting.DataTransferObjects.SortDirection;
    using SortFieldModel = Models.SortField;
    using SortFieldDTO = EstateReporting.DataTransferObjects.SortField;


    /// <summary>
    /// 
    /// </summary>
    public interface IModelFactory
    {
        #region Methods

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        EstateModel ConvertFrom(EstateResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        MerchantBalanceModel ConvertFrom(MerchantBalanceResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        CreateOperatorRequest ConvertFrom(CreateOperatorModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        CreateContractRequest ConvertFrom(CreateContractModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        CreateOperatorResponseModel ConvertFrom(CreateOperatorResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        List<MerchantModel> ConvertFrom(List<MerchantResponse> source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        List<ContractModel> ConvertFrom(List<ContractResponse> source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        ContractModel ConvertFrom(ContractResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        MerchantModel ConvertFrom(MerchantResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        CreateMerchantResponseModel ConvertFrom(CreateMerchantResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        CreateMerchantRequest ConvertFrom(CreateMerchantModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        MakeMerchantDepositRequest ConvertFrom(MakeMerchantDepositModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        List<FileImportLogModel> ConvertFrom(FileImportLogList source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        FileImportLogModel ConvertFrom(FileImportLog source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        MakeMerchantDepositResponseModel ConvertFrom(MakeMerchantDepositResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        CreateContractResponseModel ConvertFrom(CreateContractResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AddProductToContractRequest ConvertFrom(AddProductToContractModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AddMerchantDeviceRequest ConvertFrom(AddMerchantDeviceModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AddMerchantDeviceResponseModel ConvertFrom(AddMerchantDeviceResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AddProductToContractResponseModel ConvertFrom(AddProductToContractResponse source);

        #endregion

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AddTransactionFeeForProductToContractRequest ConvertFrom(AddTransactionFeeToContractProductModel source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AddTransactionFeeToContractProductResponseModel ConvertFrom(AddTransactionFeeForProductToContractResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TransactionForPeriodModel ConvertToPeriodModel(TransactionsByDayResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TransactionsByDateModel ConvertFrom(TransactionsByDayResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TransactionsByWeekModel ConvertFrom(TransactionsByWeekResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TransactionsByMonthModel ConvertFrom(TransactionsByMonthResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        SortDirectionDTO ConvertFrom(SortDirectionModel sortDirection);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        /// <returns></returns>
        SortFieldDTO ConvertFrom(SortFieldModel sortField);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TransactionsByMerchantModel ConvertFrom(TransactionsByMerchantResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TransactionsByOperatorModel ConvertFrom(TransactionsByOperatorResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantBalanceHistory">The merchant balance history.</param>
        /// <returns></returns>
        List<MerchantBalanceHistory> ConvertFrom(List<MerchantBalanceHistoryResponse> merchantBalanceHistory);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        FileDetailsModel ConvertFrom(FileDetails source);
    }
}