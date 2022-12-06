namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System.Collections.Generic;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using FileProcessor.DataTransferObjects.Responses;
    using Models;
    using TransactionProcessor.DataTransferObjects;
    using SortDirectionModel = Models.SortDirection;
    using SortFieldModel = Models.SortField;

    /// <summary>
    /// 
    /// </summary>
    public interface IModelFactory
    {
        #region Methods

        EstateModel ConvertFrom(EstateResponse source);

        MerchantBalanceModel ConvertFrom(MerchantBalanceResponse source);

        CreateOperatorRequest ConvertFrom(CreateOperatorModel source);

        CreateContractRequest ConvertFrom(CreateContractModel source);

        CreateOperatorResponseModel ConvertFrom(CreateOperatorResponse source);

        List<MerchantModel> ConvertFrom(List<MerchantResponse> source);
        
        List<ContractModel> ConvertFrom(List<ContractResponse> source);
        
        ContractModel ConvertFrom(ContractResponse source);
        
        MerchantModel ConvertFrom(MerchantResponse merchantResponse, MerchantBalanceResponse merchantBalanceResponse);

        CreateMerchantResponseModel ConvertFrom(CreateMerchantResponse source);

        CreateMerchantRequest ConvertFrom(CreateMerchantModel source);

        MakeMerchantDepositRequest ConvertFrom(MakeMerchantDepositModel source);

        List<FileImportLogModel> ConvertFrom(FileImportLogList source);

        FileImportLogModel ConvertFrom(FileImportLog source);

        MakeMerchantDepositResponseModel ConvertFrom(MakeMerchantDepositResponse source);

        CreateContractResponseModel ConvertFrom(CreateContractResponse source);

        AddProductToContractRequest ConvertFrom(AddProductToContractModel source);

        AddMerchantDeviceRequest ConvertFrom(AddMerchantDeviceModel source);

        AddMerchantDeviceResponseModel ConvertFrom(AddMerchantDeviceResponse source);

        AddProductToContractResponseModel ConvertFrom(AddProductToContractResponse source);

        AddTransactionFeeForProductToContractRequest ConvertFrom(AddTransactionFeeToContractProductModel source);

        AddTransactionFeeToContractProductResponseModel ConvertFrom(AddTransactionFeeForProductToContractResponse source);

        List<MerchantBalanceHistory> ConvertFrom(List<MerchantBalanceChangedEntryResponse> merchantBalanceHistory);

        FileDetailsModel ConvertFrom(FileDetails source);

        AssignOperatorRequest ConvertFrom(AssignOperatorToMerchantModel source);

        AssignOperatorToMerchantResponseModel ConvertFrom(AssignOperatorResponse source);

        #endregion
    }
}