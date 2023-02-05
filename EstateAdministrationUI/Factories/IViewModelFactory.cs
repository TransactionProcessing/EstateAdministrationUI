namespace EstateAdministrationUI.Factories{
    using System;
    using System.Collections.Generic;
    using Areas.Estate.Models;
    using BusinessLogic.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IViewModelFactory{
        #region Methods

        AssignOperatorToMerchantModel ConvertFrom(AssignOperatorToMerchantViewModel assignOperatorToMerchantViewModel);

        MerchantBalanceViewModel ConvertFrom(MerchantBalanceModel merchantBalanceModel);

        AddMerchantDeviceModel ConvertFrom(AddMerchantDeviceViewModel addMerchantDeviceViewModel);

        EstateViewModel ConvertFrom(EstateModel estateModel);

        ContractProductListViewModel ConvertFrom(ContractModel contractModel);

        ContractProductTransactionFeesListViewModel ConvertFrom(ContractProductModel contractProduct);

        CreateOperatorModel ConvertFrom(CreateOperatorViewModel createOperatorViewModel);

        CreateContractModel ConvertFrom(CreateContractViewModel createContractViewModel);

        List<OperatorListViewModel> ConvertFrom(Guid estateId,
                                                List<EstateOperatorModel> estateOperatorModels);

        OperatorListViewModel ConvertFrom(Guid estateId,
                                          EstateOperatorModel estateOperatorModel);

        CreateMerchantModel ConvertFrom(CreateMerchantViewModel createMerchantViewModel);

        List<MerchantListViewModel> ConvertFrom(List<MerchantModel> merchantModels);

        List<ContractListViewModel> ConvertFrom(List<ContractModel> contractModels);

        MerchantViewModel ConvertFrom(MerchantModel merchantModel);

        MerchantBalanceHistoryListViewModel ConvertFrom(List<MerchantBalanceHistory> merchantBalanceModel);

        MakeMerchantDepositModel ConvertFrom(MakeMerchantDepositViewModel makeMerchantDepositViewModel);

        AddProductToContractModel ConvertFrom(CreateContractProductViewModel createContractProductViewModel);

        AddTransactionFeeToContractProductModel ConvertFrom(CreateContractProductTransactionFeeViewModel createContractProductTransactionFeeViewModel);

        List<FileImportLogViewModel> ConvertFrom(List<FileImportLogModel> models);

        FileImportLogViewModel ConvertFrom(FileImportLogModel model);

        FileDetailsViewModel ConvertFrom(FileDetailsModel model);

        List<ContractProductTypeViewModel> ConvertFrom(List<ContractProductTypeModel> contractProductTypeModels);

        #endregion
    }
}