using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EstateAdministrationUI.BusinessLogic.Factories;
using EstateAdministrationUI.BusinessLogic.Models;
using EstateManagement.Client;
using EstateManagement.DataTransferObjects.Requests.Contract;
using EstateManagement.DataTransferObjects.Requests.Merchant;
using EstateManagement.DataTransferObjects.Responses.Contract;
using EstateManagement.DataTransferObjects.Responses.Estate;
using EstateManagement.DataTransferObjects.Responses.Merchant;
using EstateReportingAPI.Client;
using EstateReportingAPI.DataTransferObjects;
using EstateReportingAPI.DataTrasferObjects;
using FileProcessor.Client;
using FileProcessor.DataTransferObjects;
using FileProcessor.DataTransferObjects.Responses;
using Shared.Logger;
using SimpleResults;
using TransactionProcessor.Client;
using TransactionProcessor.DataTransferObjects;
using TopBottom = EstateAdministrationUI.BusinessLogic.Models.TopBottom;

namespace EstateAdministrationUI.Services;

/// <summary>
/// </summary>
/// <seealso cref="EstateAdministrationUI.Services.IApiClient" />
[ExcludeFromCodeCoverage]
public class ApiClient : IApiClient {
    #region Fields

    private readonly IEstateClient EstateClient;

    private readonly IEstateReportingApiClient EstateReportingApiClient;

    private readonly IFileProcessorClient FileProcessorClient;

    private readonly ITransactionProcessorClient TransactionProcessorClient;

    #endregion

    #region Constructors

    public ApiClient(IEstateClient estateClient, IFileProcessorClient fileProcessorClient, ITransactionProcessorClient transactionProcessorClient, IEstateReportingApiClient estateReportingApiClient) {
        this.EstateClient = estateClient;
        this.FileProcessorClient = fileProcessorClient;
        this.TransactionProcessorClient = transactionProcessorClient;
        this.EstateReportingApiClient = estateReportingApiClient;
    }

    #endregion

    #region Methods

    public async Task AddDeviceToMerchant(String accessToken, Guid actionId, Guid estateId, Guid merchantId, AddMerchantDeviceModel merchantDeviceModel, CancellationToken cancellationToken) {
        async Task ClientMethod() {
            AddMerchantDeviceRequest apiRequest = ModelFactory.ConvertFrom(merchantDeviceModel);

            await this.EstateClient.AddDeviceToMerchant(accessToken, estateId, merchantId, apiRequest, cancellationToken);
        }

        await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<AddProductToContractResponseModel> AddProductToContract(String accessToken, Guid actionId, Guid estateId, Guid contractId, AddProductToContractModel addProductToContractModel, CancellationToken cancellationToken) {
        async Task<AddProductToContractResponseModel> ClientMethod() {
            AddProductToContractRequest apiRequest = ModelFactory.ConvertFrom(addProductToContractModel);

            AddProductToContractResponse apiResponse = await this.EstateClient.AddProductToContract(accessToken, estateId, contractId, apiRequest, cancellationToken);

            AddProductToContractResponseModel addProductToContractResponseModel = ModelFactory.ConvertFrom(apiResponse);

            return addProductToContractResponseModel;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<AddTransactionFeeToContractProductResponseModel> AddTransactionFeeToContractProduct(String accessToken, Guid actionId, Guid estateId, Guid contractId, Guid contractProductId, AddTransactionFeeToContractProductModel addTransactionFeeToContractProductModel, CancellationToken cancellationToken) {
        async Task<AddTransactionFeeToContractProductResponseModel> ClientMethod() {
            AddTransactionFeeForProductToContractRequest apiRequest = ModelFactory.ConvertFrom(addTransactionFeeToContractProductModel);

            AddTransactionFeeForProductToContractResponse apiResponse = await this.EstateClient.AddTransactionFeeForProductToContract(accessToken, estateId, contractId, contractProductId, apiRequest, cancellationToken);

            AddTransactionFeeToContractProductResponseModel addTransactionFeeToContractProductResponseModel = ModelFactory.ConvertFrom(apiResponse);

            return addTransactionFeeToContractProductResponseModel;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task AssignOperatorToMerchant(String accessToken, Guid actionId, Guid estateId, Guid merchantId, AssignOperatorToMerchantModel assignOperatorToMerchantModel, CancellationToken cancellationToken) {
        async Task ClientMethod() {
            AssignOperatorRequest apiRequest = ModelFactory.ConvertFrom(assignOperatorToMerchantModel);

            await this.EstateClient.AssignOperatorToMerchant(accessToken, estateId, merchantId, apiRequest, cancellationToken);
        }

        await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<CreateContractResponseModel> CreateContract(String accessToken, Guid actionId, Guid estateId, CreateContractModel createContractModel, CancellationToken cancellationToken) {
        async Task<CreateContractResponseModel> ClientMethod() {
            CreateContractRequest apiRequest = ModelFactory.ConvertFrom(createContractModel);

            CreateContractResponse apiResponse = await this.EstateClient.CreateContract(accessToken, estateId, apiRequest, cancellationToken);

            CreateContractResponseModel createContractResponseModel = ModelFactory.ConvertFrom(apiResponse);

            return createContractResponseModel;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<CreateMerchantResponseModel> CreateMerchant(String accessToken, Guid actionId, Guid estateId, CreateMerchantModel createMerchantModel, CancellationToken cancellationToken) {
        async Task<CreateMerchantResponseModel> ClientMethod() {
            CreateMerchantRequest apiRequest = ModelFactory.ConvertFrom(createMerchantModel);

            CreateMerchantResponse apiResponse = await this.EstateClient.CreateMerchant(accessToken, estateId, apiRequest, cancellationToken);

            CreateMerchantResponseModel createMerchantResponseModel = ModelFactory.ConvertFrom(apiResponse);

            return createMerchantResponseModel;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<CreateOperatorResponseModel> CreateOperator(String accessToken, Guid actionId, Guid estateId, CreateOperatorModel createOperatorModel, CancellationToken cancellationToken) {
        //async Task<CreateOperatorResponseModel> ClientMethod()
        //{
        //    CreateOperatorRequest apiRequest = ModelFactory.ConvertFrom(createOperatorModel);

        //    CreateOperatorResponse apiResponse = await this.EstateClient.CreateOperator(accessToken, estateId, apiRequest, cancellationToken);

        //    CreateOperatorResponseModel createOperatorResponseModel = ModelFactory.ConvertFrom(apiResponse);

        //    return createOperatorResponseModel;
        //}
        //return await CallClientMethod<CreateOperatorResponseModel>(ClientMethod, cancellationToken);
        // TODO: upgarde this as part of UI changes for create/assign operators
        return null;
    }

    public async Task<ContractModel> GetContract(String accessToken, Guid actionId, Guid estateId, Guid contractId, CancellationToken cancellationToken) {
        async Task<ContractModel> ClientMethod() {
            ContractResponse contract = await this.EstateClient.GetContract(accessToken, estateId, contractId, cancellationToken);

            return ModelFactory.ConvertFrom(contract);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<ContractProductModel> GetContractProduct(String accessToken, Guid actionId, Guid estateId, Guid contractId, Guid contractProductId, CancellationToken cancellationToken) {
        async Task<ContractProductModel> ClientMethod() {
            ContractResponse contract = await this.EstateClient.GetContract(accessToken, estateId, contractId, cancellationToken);

            ContractModel contractModel = ModelFactory.ConvertFrom(contract);

            return contractModel.ContractProducts.SingleOrDefault(cp => cp.ContractProductId == contractProductId);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<ContractProductTypeModel>> GetContractProductTypeList(String accessToken, CancellationToken cancellationToken) {
        return new List<ContractProductTypeModel> {
            new() {
                Description = "Mobile Topup",
                ProductType = 1
            },
            new() {
                Description = "Voucher",
                ProductType = 2
            },
            new() {
                Description = "Bill Payment",
                ProductType = 3
            }
        };
    }

    public async Task<List<ContractModel>> GetContracts(String accessToken, Guid actionId, Guid estateId, CancellationToken cancellationToken) {
        async Task<List<ContractModel>> ClientMethod() {
            List<ContractResponse> contracts = await this.EstateClient.GetContracts(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(contracts);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<EstateModel> GetEstate(String accessToken, Guid actionId, Guid estateId, CancellationToken cancellationToken) {
        async Task<EstateModel> ClientMethod() {
            EstateResponse estate = await this.EstateClient.GetEstate(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(estate);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<FileDetailsModel> GetFileDetails(String accessToken, Guid actionId, Guid estateId, Guid fileId, CancellationToken cancellationToken) {
        async Task<FileDetailsModel> ClientMethod() {
            FileDetails fileDetails = await this.FileProcessorClient.GetFile(accessToken, estateId, fileId, cancellationToken);

            return ModelFactory.ConvertFrom(fileDetails);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<FileImportLogModel> GetFileImportLog(String accessToken, Guid actionId, Guid estateId, Guid fileImportLogId, CancellationToken cancellationToken) {
        async Task<FileImportLogModel> ClientMethod() {
            FileImportLog fileImportLog = await this.FileProcessorClient.GetFileImportLog(accessToken, fileImportLogId, estateId, null, cancellationToken);

            return ModelFactory.ConvertFrom(fileImportLog);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<FileImportLogModel>> GetFileImportLogs(String accessToken, Guid actionId, Guid estateId, Guid? merchantId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken) {
        async Task<List<FileImportLogModel>> ClientMethod() {
            FileImportLogList fileImportLogs = await this.FileProcessorClient.GetFileImportLogs(accessToken, estateId, startDate, endDate, merchantId, cancellationToken);

            List<FileImportLogModel> fileImportLogModels = ModelFactory.ConvertFrom(fileImportLogs);

            return fileImportLogModels;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<MerchantModel> GetMerchant(String accessToken, Guid actionId, Guid estateId, Guid merchantId, CancellationToken cancellationToken) {
        async Task<MerchantModel> ClientMethod() {
            MerchantResponse merchant = await this.EstateClient.GetMerchant(accessToken, estateId, merchantId, cancellationToken);
            MerchantBalanceResponse merchantBalance = await this.TransactionProcessorClient.GetMerchantBalance(accessToken, estateId, merchantId, cancellationToken);

            return ModelFactory.ConvertFrom(merchant, merchantBalance);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<MerchantBalanceModel> GetMerchantBalance(String accessToken, Guid actionId, Guid estateId, Guid merchantId, CancellationToken cancellationToken) {
        async Task<MerchantBalanceModel> ClientMethod() {
            MerchantBalanceResponse merchantBalance = await this.TransactionProcessorClient.GetMerchantBalance(accessToken, estateId, merchantId, cancellationToken);

            return ModelFactory.ConvertFrom(merchantBalance);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<MerchantBalanceHistory>> GetMerchantBalanceHistory(String accessToken, Guid actionId, Guid estateId, Guid merchantId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken) {
        async Task<List<MerchantBalanceHistory>> ClientMethod() {
            List<MerchantBalanceChangedEntryResponse> merchantBalanceHistory = await this.TransactionProcessorClient.GetMerchantBalanceHistory(accessToken, estateId, merchantId, startDate, endDate, cancellationToken);

            return ModelFactory.ConvertFrom(merchantBalanceHistory);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<MerchantModel>> GetMerchants(String accessToken, Guid actionId, Guid estateId, CancellationToken cancellationToken) {
        async Task<List<MerchantModel>> ClientMethod() {
            List<MerchantResponse> merchants = await this.EstateClient.GetMerchants(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(merchants);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<List<MerchantListModel>>> GetMerchantsForReporting(String accessToken, Guid estateId, CancellationToken cancellationToken) {
        async Task<Result<List<MerchantListModel>>> ClientMethod() {
            List<Merchant> merchants = await this.EstateReportingApiClient.GetMerchants(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(merchants);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<List<OperatorListModel>>> GetOperatorsForReporting(String accessToken, Guid estateId, CancellationToken cancellationToken) {
        async Task<Result<List<OperatorListModel>>> ClientMethod() {
            List<Operator> operators = await this.EstateReportingApiClient.GetOperators(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(operators);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<MakeMerchantDepositResponseModel> MakeMerchantDeposit(String accessToken, Guid actionId, Guid estateId, Guid merchantId, MakeMerchantDepositModel makeMerchantDepositModel, CancellationToken cancellationToken) {
        async Task<MakeMerchantDepositResponseModel> ClientMethod() {
            MakeMerchantDepositRequest apiRequest = ModelFactory.ConvertFrom(makeMerchantDepositModel);

            MakeMerchantDepositResponse apiResponse = await this.EstateClient.MakeMerchantDeposit(accessToken, estateId, merchantId, apiRequest, cancellationToken);

            MakeMerchantDepositResponseModel makeMerchantDepositResponseModel = ModelFactory.ConvertFrom(apiResponse);

            return makeMerchantDepositResponseModel;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Guid> UploadFile(String accessToken, Guid actionId, Guid estateId, Guid merchantId, Guid userId, Guid fileProfileId, Byte[] fileData, String fileName, CancellationToken cancellationToken) {
        async Task<Guid> ClientMethod() {
            UploadFileRequest apiRequest = new() {
                EstateId = estateId,
                FileProfileId = fileProfileId,
                MerchantId = merchantId,
                UserId = userId
            };

            Guid apiResponse = await this.FileProcessorClient.UploadFile(accessToken, fileName, fileData, apiRequest, cancellationToken);
            return apiResponse;
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<CalendarDateModel>> GetCalendarDates(String accessToken, Guid estateId, Int32 year, CancellationToken cancellationToken) {
        async Task<List<CalendarDateModel>> ClientMethod() {
            List<CalendarDate> apiResponse = await this.EstateReportingApiClient.GetCalendarDates(accessToken, estateId, year, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<CalendarYearModel>> GetCalendarYears(String accessToken, Guid estateId, CancellationToken cancellationToken) {
        async Task<List<CalendarYearModel>> ClientMethod() {
            List<CalendarYear> apiResponse = await this.EstateReportingApiClient.GetCalendarYears(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<List<ComparisonDateModel>>> GetComparisonDates(String accessToken, Guid estateId, CancellationToken cancellationToken) {
        async Task<Result<List<ComparisonDateModel>>> ClientMethod() {
            List<ComparisonDate> apiResponse = await this.EstateReportingApiClient.GetComparisonDates(accessToken, estateId, cancellationToken);

            return Result.Success(ModelFactory.ConvertFrom(apiResponse));
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<TodaysSalesModel>> GetTodaysSales(String accessToken,
                                                               Guid estateId,
                                                               Int32? merchantReportingId,
                                                               Int32? operatorReportingId,
                                                               DateTime comparisonDate,
                                                               CancellationToken cancellationToken)
    {
        async Task<Result<TodaysSalesModel>> ClientMethod() {
            TodaysSales apiResponse = await this.EstateReportingApiClient.GetTodaysSales(accessToken, estateId, merchantReportingId.GetValueOrDefault(0), operatorReportingId.GetValueOrDefault(0), comparisonDate, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<List<TodaysSalesCountByHourModel>>> GetTodaysSalesCountByHour(string accessToken, Guid estateId, Guid? merchantId, Guid? operatorId, DateTime comparisonDate,
        CancellationToken cancellationToken) {
        async Task<Result<List<TodaysSalesCountByHourModel>>> ClientMethod() {
            List<TodaysSalesCountByHour> apiResponse =
                await this.EstateReportingApiClient.GetTodaysSalesCountByHour(accessToken, estateId, 0, 0,
                    comparisonDate, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<List<TodaysSalesValueByHourModel>>> GetTodaysSalesValueByHour(String accessToken, Guid estateId, Guid? merchantId, Guid? operatorId, DateTime comparisonDate, CancellationToken cancellationToken) {
        async Task<Result<List<TodaysSalesValueByHourModel>>> ClientMethod() {
            List<TodaysSalesValueByHour> apiResponse = await this.EstateReportingApiClient.GetTodaysSalesValueByHour(accessToken, estateId, 0, 0, comparisonDate, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<Result<TodaysSettlementModel>> GetTodaysSettlement(String accessToken,
                                                                         Guid estateId,
                                                                         Guid? merchantId,
                                                                         Guid? operatorId,
                                                                         DateTime comparisonDate,
                                                                         CancellationToken cancellationToken) {
        async Task<Result<TodaysSettlementModel>> ClientMethod() {
            TodaysSettlement apiResponse = await this.EstateReportingApiClient.GetTodaysSettlement(accessToken, estateId, 0, 0, comparisonDate, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<MerchantKpiModel> GetMerchantKpi(String accessToken, Guid estateId, CancellationToken cancellationToken) {
        async Task<MerchantKpiModel> ClientMethod() {
            MerchantKpi apiResponse = await this.EstateReportingApiClient.GetMerchantKpi(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<TodaysSalesModel> GetTodaysFailedSales(String accessToken, Guid estateId, String responseCode, DateTime comparisonDate, CancellationToken cancellationToken) {
        async Task<TodaysSalesModel> ClientMethod() {
            TodaysSales apiResponse = await this.EstateReportingApiClient.GetTodaysFailedSales(accessToken, estateId, 0, 0, responseCode, comparisonDate, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }


    public async Task<List<TopBottomOperatorDataModel>> GetTopBottomOperatorData(String accessToken, Guid estateId, TopBottom topBottom, Int32 resultCount, CancellationToken cancellationToken) {
        async Task<List<TopBottomOperatorDataModel>> ClientMethod() {
            List<TopBottomOperatorData> apiResponse = await this.EstateReportingApiClient.GetTopBottomOperatorData(accessToken, estateId, this.Convert(topBottom), resultCount, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<TopBottomMerchantDataModel>> GetTopBottomMerchantData(String accessToken, Guid estateId, TopBottom topBottom, Int32 resultCount, CancellationToken cancellationToken) {
        async Task<List<TopBottomMerchantDataModel>> ClientMethod() {
            List<TopBottomMerchantData> apiResponse = await this.EstateReportingApiClient.GetTopBottomMerchantData(accessToken, estateId, this.Convert(topBottom), resultCount, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<List<TopBottomProductDataModel>> GetTopBottomProductData(String accessToken, Guid estateId, TopBottom topBottom, Int32 resultCount, CancellationToken cancellationToken) {
        async Task<List<TopBottomProductDataModel>> ClientMethod() {
            List<TopBottomProductData> apiResponse = await this.EstateReportingApiClient.GetTopBottomProductData(accessToken, estateId, this.Convert(topBottom), resultCount, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    public async Task<LastSettlementModel> GetLastSettlement(String accessToken, Guid estateId, Guid? merchantId, Guid? operatorId, CancellationToken cancellationToken) {
        async Task<LastSettlementModel> ClientMethod() {
            LastSettlement apiResponse = await this.EstateReportingApiClient.GetLastSettlement(accessToken, estateId, cancellationToken);

            return ModelFactory.ConvertFrom(apiResponse);
        }

        return await this.CallClientMethod(ClientMethod, cancellationToken);
    }

    private EstateReportingAPI.DataTransferObjects.TopBottom Convert(TopBottom model) {
        return model switch {
            TopBottom.Bottom => EstateReportingAPI.DataTransferObjects.TopBottom.Bottom,
            _ => EstateReportingAPI.DataTransferObjects.TopBottom.Top
        };
    }

    private async Task<Result<T>> CallClientMethod<T>(Func<Task<Result<T>>> clientMethod, CancellationToken cancellationToken)
    {
        try
        {
            return await clientMethod();
        }
        catch (Exception e)
        {
            Logger.LogError(e);
            return Result.Failure(e.Message);

        }
    }

    private async Task<T> CallClientMethod<T>(Func<Task<T>> clientMethod, CancellationToken cancellationToken) {
        try {
            return await clientMethod();
        }
        catch (Exception e) {
            Logger.LogError(e);
            throw;
        }
    }

    private async Task CallClientMethod(Func<Task> clientMethod, CancellationToken cancellationToken) {
        try {
            await clientMethod();
        }
        catch (Exception e) {
            Logger.LogError(e);
        }
    }

    #endregion
}