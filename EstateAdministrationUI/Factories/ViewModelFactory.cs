using Microsoft.AspNetCore.Routing.Constraints;

namespace EstateAdministrationUI.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Areas.Estate.Controllers;
    using Areas.Estate.Models;
    using Azure;
    using BusinessLogic.Models;
    using Common;
    using EstateReportingAPI.DataTransferObjects;
    using EstateReportingAPI.DataTrasferObjects;
    using NuGet.Protocol.Core.Types;
    using SimpleResults;
    using FileLineProcessingResult = Areas.Estate.Models.FileLineProcessingResult;
    
    public static class ViewModelFactory
    {
        #region Methods

        public static LastSettlementViewModel ConvertFrom(LastSettlementModel model){
            if (model == null)
                return null;

            LastSettlementViewModel viewModel = new LastSettlementViewModel{
                                                                               FeesValue = model.FeesValue,
                                                                               SalesCount = model.SalesCount,
                                                                               SalesValue = model.SalesValue,
                                                                               SettlementDate = model.SettlementDate
                                                                           };
            return viewModel;
        }

        public static TopBottomMerchantViewModelList ConvertFrom(List<TopBottomMerchantDataModel> models){
            if (models == null || models.Any() == false)
            {
                return new TopBottomMerchantViewModelList();
            }

            TopBottomMerchantViewModelList viewModels = new TopBottomMerchantViewModelList();
            viewModels.Merchants = new List<TopBottomMerchantViewModel>();

            models.ForEach(m => viewModels.Merchants.Add(new TopBottomMerchantViewModel{
                                                                                           MerchantName = m.MerchantName,
                                                                                           SalesValue = m.SalesValue
                                                                                       }));
            return viewModels;
        }

        public static TopBottomProductViewModelList ConvertFrom(List<TopBottomProductDataModel> models)
        {
            if (models == null || models.Any() == false){
                return new TopBottomProductViewModelList();
            }

            TopBottomProductViewModelList viewModels = new TopBottomProductViewModelList();
            viewModels.Products = new List<TopBottomProductViewModel>();

            models.ForEach(m => viewModels.Products.Add(new TopBottomProductViewModel()
            {
                                                             ProductName = m.ProductName,
                                                             SalesValue = m.SalesValue
                                                         }));
            return viewModels;
        }

        public static TopBottomOperatorViewModelList ConvertFrom(List<TopBottomOperatorDataModel> models)
        {
            if (models == null || models.Any() == false)
            {
                return new TopBottomOperatorViewModelList();
            }

            TopBottomOperatorViewModelList viewModels = new TopBottomOperatorViewModelList();
            viewModels.Operators = new List<TopBottomOperatorViewModel>();

            models.ForEach(m => viewModels.Operators.Add(new TopBottomOperatorViewModel()
                                                         {
                                                             OperatorName = m.OperatorName,
                                                             SalesValue = m.SalesValue
                                                         }));
            return viewModels;
        }

        public static List<(String value, String text)> ConvertFrom(Result<List<MerchantListModel>> source)
        {
            if (source.IsFailed || source.Data == null || source.Data.Any() == false) {
                return new List<(String value, String text)>();
            }

            List<(String value, String text)> viewModels = new List<(String value, String text)>();
            foreach (MerchantListModel merchant in source.Data)
            {
                viewModels.Add((merchant.MerchantReportingId.ToString(), merchant.MerchantName));
            }
            return viewModels;
        }

        public static List<(String value, String text)> ConvertFrom(Result<List<OperatorListModel>> source)
        {
            if (source.IsFailed || source.Data == null || source.Data.Any() == false)
            {
                return new List<(String value, String text)>();
            }

            List<(String value, String text)> viewModels = new List<(String value, String text)>();
            foreach (OperatorListModel @operator in source.Data)
            {
                viewModels.Add((@operator.OperatorReportingId.ToString(), @operator.OperatorName));
            }
            return viewModels;
        }

        public static List<(String value, String text)> ConvertFrom(Result<List<ComparisonDateModel>> comparisonDates, String dateFormat = "yyyy-MM-dd"){

            if (comparisonDates.IsFailed || comparisonDates.Data == null || comparisonDates.Data.Any() == false) {
                return new List<(String value, String text)>();
            }

            List<(String value, String text)> viewModels = new();
            foreach (ComparisonDateModel comparisonDate in comparisonDates.Data)
            {
                viewModels.Add((comparisonDate.Date.ToString(dateFormat), comparisonDate.Description));
            }
            return viewModels;
        }

        public static List<HourValueViewModel> ConvertFrom(Result<List<TodaysSalesValueByHourModel>> salesValueByHourModels)
        {
            if (salesValueByHourModels.IsFailed || salesValueByHourModels.Data == null || salesValueByHourModels.Data.Any() == false) {
                return new List<HourValueViewModel>();
            }

            List<HourValueViewModel> viewModels = new List<HourValueViewModel>();

            salesValueByHourModels.Data.ForEach(s => viewModels.Add(new HourValueViewModel{
                                                                                         ComparisonValue = s.ComparisonSalesValue,
                                                                                         Hour = s.Hour,
                                                                                         TodaysValue = s.TodaysSalesValue
                                                                                     }));
            return viewModels;
        }

        public static List<HourCountViewModel> ConvertFrom(Result<List<TodaysSalesCountByHourModel>> salesCountByHourModels)
        {
            if (salesCountByHourModels.IsFailed ||  salesCountByHourModels.Data == null || salesCountByHourModels.Data.Any() == false) {
                return new List<HourCountViewModel>();
            }

            List<HourCountViewModel> viewModels = new List<HourCountViewModel>();

            salesCountByHourModels.Data.ForEach(s => viewModels.Add(new HourCountViewModel
            {
                                                    ComparisonCount = s.ComparisonSalesCount,
                                                    Hour = s.Hour,
                                                    TodaysCount = s.TodaysSalesCount
                                                }));
            return viewModels;
        }

        public static TodaysSalesViewModel ConvertFrom(Result<TodaysSalesModel> todaysSales, String comparisonLabel)
        {
            if (todaysSales.IsFailed || todaysSales.Data == null) {
                return new TodaysSalesViewModel();
            }

            TodaysSalesViewModel viewModel = new TodaysSalesViewModel{
                                                                         ComparisonValueOfTransactions = todaysSales.Data.ComparisonSalesValue,
                                                                         ComparisonCountOfTransactions = todaysSales.Data.ComparisonSalesCount,
                                                                         TodaysValueOfTransactions = todaysSales.Data.TodaysSalesValue,
                                                                         TodaysCountOfTransactions = todaysSales.Data.TodaysSalesCount,
                                                                         Variance = (todaysSales.Data.TodaysSalesValue - todaysSales.Data.ComparisonSalesValue).SafeDivision(todaysSales.Data.TodaysSalesValue),
                                                                         CountVariance = (todaysSales.Data.TodaysSalesCount - todaysSales.Data.ComparisonSalesCount).SafeDivision(todaysSales.Data.TodaysSalesCount),
                Label = $"{comparisonLabel} Sales"
                                                                     };
            return viewModel;

        }

        public static MerchantKpiViewModel ConvertFrom(MerchantKpiModel merchantKpiModel)
        {
            if (merchantKpiModel == null)
            {
                throw new ArgumentNullException(nameof(merchantKpiModel));
            }

            MerchantKpiViewModel viewModel = new MerchantKpiViewModel{
                                                                         MerchantsWithNoSaleInLast7Days = merchantKpiModel.MerchantsWithNoSaleInLast7Days,
                                                                         MerchantsWithNoSaleToday = merchantKpiModel.MerchantsWithNoSaleToday,
                                                                         MerchantsWithSaleInLastHour = merchantKpiModel.MerchantsWithSaleInLastHour,
                                                                     };
            return viewModel;

        }

        public static TodaysSettlementViewModel ConvertFrom(Result<TodaysSettlementModel> todaysSettlement, String comparisonLabel= null){
            if (todaysSettlement.IsFailed || todaysSettlement.Data == null) {
                return new TodaysSettlementViewModel();
            }

            TodaysSettlementViewModel viewModel = new TodaysSettlementViewModel{
                                                                                   ComparisonSettlementValue = todaysSettlement.Data.ComparisonSettlementValue,
                                                                                   TodaysSettlementValue = todaysSettlement.Data.TodaysSettlementValue,
                                                                                   Variance = (todaysSettlement.Data.TodaysSettlementValue - todaysSettlement.Data.ComparisonSettlementValue).SafeDivision(todaysSettlement.Data.TodaysSettlementValue),
                                                                                   Label = $"{comparisonLabel} Settlement",
                                                                               };
            return viewModel;
        }

        public static AssignOperatorToMerchantModel ConvertFrom(AssignOperatorToMerchantViewModel assignOperatorToMerchantViewModel)
        {
            if (assignOperatorToMerchantViewModel == null)
            {
                throw new ArgumentNullException(nameof(assignOperatorToMerchantViewModel));
            }

            AssignOperatorToMerchantModel model = new AssignOperatorToMerchantModel
                                                  {
                                                      MerchantNumber = assignOperatorToMerchantViewModel.MerchantNumber,
                                                      TerminalNumber = assignOperatorToMerchantViewModel.TerminalNumber,
                                                      OperatorId = assignOperatorToMerchantViewModel.OperatorId
                                                  };

            return model;
        }

        public static MerchantBalanceViewModel ConvertFrom(MerchantBalanceModel merchantBalanceModel)
        {
            if (merchantBalanceModel == null)
            {
                throw new ArgumentNullException(nameof(merchantBalanceModel));
            }

            MerchantBalanceViewModel viewModel = new MerchantBalanceViewModel
                                                 {
                                                     EstateId = merchantBalanceModel.EstateId,
                                                     AvailableBalance = merchantBalanceModel.AvailableBalance,
                                                     Balance = merchantBalanceModel.Balance,
                                                     MerchantId = merchantBalanceModel.MerchantId
                                                 };

            return viewModel;
        }

        public static AddMerchantDeviceModel ConvertFrom(AddMerchantDeviceViewModel addMerchantDeviceViewModel)
        {
            if (addMerchantDeviceViewModel == null)
            {
                throw new ArgumentNullException(nameof(addMerchantDeviceViewModel));
            }

            AddMerchantDeviceModel model = new AddMerchantDeviceModel
                                           {
                                               DeviceIdentifier = addMerchantDeviceViewModel.DeviceIdentifier
                                           };
            return model;
        }

        public static EstateViewModel ConvertFrom(EstateModel estateModel)
        {
            if (estateModel == null)
            {
                throw new ArgumentNullException(nameof(estateModel));
            }

            EstateViewModel viewModel = new EstateViewModel
                                        {
                                            EstateName = estateModel.EstateName,
                                            EstateId = estateModel.EstateId
                                        };

            return viewModel;
        }

        public static ContractProductListViewModel ConvertFrom(ContractModel contractModel)
        {
            if (contractModel == null)
            {
                throw new ArgumentNullException(nameof(contractModel));
            }

            ContractProductListViewModel viewModel = new ContractProductListViewModel
                                                     {
                                                         Description = contractModel.Description,
                                                         ContractId = contractModel.ContractId,
                                                         ContractProducts = new List<ContractProductViewModel>()
                                                     };

            if (contractModel.ContractProducts != null && contractModel.ContractProducts.Any())
            {
                contractModel.ContractProducts.ForEach(c =>
                                                       {
                                                           viewModel.ContractProducts.Add(new ContractProductViewModel
                                                                                          {
                                                                                              EstateId = contractModel.EstateId,
                                                                                              ContractId = contractModel.ContractId,
                                                                                              ContractProductId = c.ContractProductId,
                                                                                              DisplayText = c.DisplayText,
                                                                                              ProductName = c.ProductName,
                                                                                              ProductType = GetProductTypeName((ProductType)c.ProductType),
                                                                                              Value = c.Value.HasValue ? c.Value.Value.ToString() : "Variable",
                                                                                              NumberOfTransactionFees = c.ContractProductTransactionFees.Count
                                                                                          });
                                                       });
            }

            return viewModel;
        }

        public static String GetProductTypeName(ProductType productType) =>
            productType switch{
                ProductType.BillPayment => "Bill Payment",
                ProductType.MobileTopup => "Mobile Topup",
                ProductType.Voucher => "Voucher",
                _ => "Not Set"
            };

        public static ContractProductTransactionFeesListViewModel ConvertFrom(ContractProductModel contractProduct)
        {
            if (contractProduct == null)
            {
                throw new ArgumentNullException(nameof(contractProduct));
            }

            ContractProductTransactionFeesListViewModel viewModel = new ContractProductTransactionFeesListViewModel
                                                                    {
                                                                        ContractProductId = contractProduct.ContractProductId,
                                                                        ProductName = contractProduct.ProductName,
                                                                        Description = contractProduct.Description,
                                                                        Value = contractProduct.Value.HasValue ? contractProduct.Value.Value.ToString() : "Variable",
                                                                        ContractId = contractProduct.ContractId,
                                                                        TransactionFees = new List<ContractProductTransactionFeesViewModel>(),
                                                                    };

            if (contractProduct.ContractProductTransactionFees != null && contractProduct.ContractProductTransactionFees.Any())
            {
                foreach (ContractProductTransactionFeeModel transactionFee in contractProduct.ContractProductTransactionFees)
                {
                    viewModel.TransactionFees.Add(new ContractProductTransactionFeesViewModel
                                                  {
                                                      Description = transactionFee.Description,
                                                      Value = transactionFee.Value,
                                                      CalculationType = transactionFee.CalculationType,
                                                      FeeType = transactionFee.FeeType,
                                                      ContractId = contractProduct.ContractId,
                                                      ContractProductId = contractProduct.ContractProductId,
                                                      TransactionFeeId = transactionFee.TransactionFeeId,
                                                      EstateId = contractProduct.EstateId
                                                  });
                }
            }

            return viewModel;
        }

        public static CreateOperatorModel ConvertFrom(CreateOperatorViewModel createOperatorViewModel)
        {
            if (createOperatorViewModel == null)
            {
                throw new ArgumentNullException(nameof(createOperatorViewModel));
            }

            CreateOperatorModel createOperatorModel = new CreateOperatorModel
                                                      {
                                                          RequireCustomMerchantNumber = createOperatorViewModel.RequireCustomMerchantNumber,
                                                          RequireCustomTerminalNumber = createOperatorViewModel.RequireCustomTerminalNumber,
                                                          OperatorName = createOperatorViewModel.OperatorName
                                                      };

            return createOperatorModel;
        }

        public static CreateContractModel ConvertFrom(CreateContractViewModel createContractViewModel)
        {
            if (createContractViewModel == null)
            {
                throw new ArgumentNullException(nameof(createContractViewModel));
            }

            CreateContractModel createContractModel = new CreateContractModel
                                                      {
                                                          OperatorId = createContractViewModel.OperatorId,
                                                          Description = createContractViewModel.ContractDescription
                                                      };

            return createContractModel;
        }

        public static List<OperatorListViewModel> ConvertFrom(Guid estateId,
                                                              List<EstateOperatorModel> estateOperatorModels)
        {
            if (estateOperatorModels == null || estateOperatorModels.Any() == false)
            {
                throw new ArgumentNullException(nameof(estateOperatorModels));
            }

            List<OperatorListViewModel> viewModels = new List<OperatorListViewModel>();

            estateOperatorModels.ForEach(eo => viewModels.Add(ConvertFrom(estateId, eo)));

            return viewModels;
        }

        public static OperatorListViewModel ConvertFrom(Guid estateId,
                                                        EstateOperatorModel estateOperatorModel)
        {
            if (estateOperatorModel == null)
            {
                throw new ArgumentNullException(nameof(estateOperatorModel));
            }

            OperatorListViewModel viewModel = new OperatorListViewModel
                                              {
                                                  EstateId = estateId,
                                                  OperatorId = estateOperatorModel.OperatorId,
                                                  OperatorName = estateOperatorModel.Name,
                                                  RequireCustomMerchantNumber = estateOperatorModel.RequireCustomMerchantNumber,
                                                  RequireCustomTerminalNumber = estateOperatorModel.RequireCustomTerminalNumber
                                              };

            return viewModel;
        }

        public static CreateMerchantModel ConvertFrom(CreateMerchantViewModel createMerchantViewModel)
        {
            if (createMerchantViewModel == null)
            {
                throw new ArgumentNullException(nameof(createMerchantViewModel));
            }

            CreateMerchantModel createMerchantModel = new CreateMerchantModel
                                                      {
                                                          Address = new AddressModel
                                                                    {
                                                                        AddressLine1 = createMerchantViewModel.AddressLine1,
                                                                        AddressLine2 = createMerchantViewModel.AddressLine2,
                                                                        AddressLine3 = createMerchantViewModel.AddressLine3,
                                                                        AddressLine4 = createMerchantViewModel.AddressLine4,
                                                                        Country = createMerchantViewModel.Country,
                                                                        PostalCode = createMerchantViewModel.PostalCode,
                                                                        Region = createMerchantViewModel.Region,
                                                                        Town = createMerchantViewModel.Town,
                                                                    },
                                                          Contact = new ContactModel
                                                                    {
                                                                        ContactPhoneNumber = createMerchantViewModel.ContactPhoneNumber,
                                                                        ContactName = createMerchantViewModel.ContactName,
                                                                        ContactEmailAddress = createMerchantViewModel.ContactEmailAddress
                                                                    },
                                                          MerchantName = createMerchantViewModel.MerchantName,
                                                          SettlementSchedule = createMerchantViewModel.SettlementSchedule switch {
                                                              0 => SettlementSchedule.Immediate,
                                                              1 => SettlementSchedule.Weekly,
                                                              2 => SettlementSchedule.Monthly
                                                          }
                                                      };

            return createMerchantModel;
        }
        
        public static List<MerchantListViewModel> ConvertFrom(List<MerchantModel> merchantModels)
        {
            if (merchantModels == null || merchantModels.Any() == false)
            {
                throw new ArgumentNullException(nameof(merchantModels));
            }

            List<MerchantListViewModel> viewModels = new List<MerchantListViewModel>();

            foreach (MerchantModel merchantModel in merchantModels)
            {
                viewModels.Add(new MerchantListViewModel
                               {
                                   AddressLine1 = merchantModel.Addresses == null ? string.Empty :
                                       merchantModel.Addresses.FirstOrDefault() == null ? string.Empty : merchantModel.Addresses.First().AddressLine1,
                                   MerchantId = merchantModel.MerchantId,
                                   ContactName = merchantModel.Contacts == null ? string.Empty :
                                       merchantModel.Contacts.FirstOrDefault() == null ? string.Empty : merchantModel.Contacts.First().ContactName,
                                   Town = merchantModel.Addresses == null ? string.Empty :
                                       merchantModel.Addresses.FirstOrDefault() == null ? string.Empty : merchantModel.Addresses.First().Town,
                                   MerchantName = merchantModel.MerchantName,
                                   EstateId = merchantModel.EstateId,
                                   NumberOfDevices = merchantModel.Devices != null && merchantModel.Devices.Any() ? merchantModel.Devices.Count : 0,
                                   NumberOfOperators = merchantModel.Operators == null ? 0 :
                                       merchantModel.Operators != null && merchantModel.Operators.Any() ? merchantModel.Operators.Count : 0,
                                   NumberOfUsers = 0
                               });
            }

            return viewModels;
        }
        
        public static List<ContractListViewModel> ConvertFrom(List<ContractModel> contractModels)
        {
            if (contractModels == null)
            {
                throw new ArgumentNullException(nameof(contractModels));
            }

            List<ContractListViewModel> viewModels = new List<ContractListViewModel>();

            foreach (ContractModel contractModel in contractModels)
            {
                viewModels.Add(new ContractListViewModel
                               {
                                   EstateId = contractModel.EstateId,
                                   OperatorName = contractModel.OperatorName,
                                   ContractId = contractModel.ContractId,
                                   Description = contractModel.Description,
                                   OperatorId = contractModel.OperatorId,
                                   NumberOfProducts = contractModel.NumberOfProducts
                               });
            }

            return viewModels;
        }

        public static Dictionary<String, String> ConvertFrom(List<MerchantDeviceModel> merchantDevices){
            Dictionary<String, String> viewModels = new Dictionary<String, String>();

            if (merchantDevices == null || merchantDevices.Any() == false)
            {
                return viewModels;
            }
            
            foreach (MerchantDeviceModel merchantDeviceModel in merchantDevices){
                viewModels.Add(merchantDeviceModel.DeviceId.ToString(), merchantDeviceModel.DeviceIdentifier);
            }
            return viewModels;
        }

        public static MerchantViewModel ConvertFrom(MerchantModel merchantModel)
        {
            if (merchantModel == null)
            {
                throw new ArgumentNullException(nameof(merchantModel));
            }

            MerchantViewModel viewModel = new MerchantViewModel();

            viewModel.EstateId = merchantModel.EstateId;
            viewModel.MerchantId = merchantModel.MerchantId;
            viewModel.MerchantName = merchantModel.MerchantName;
            viewModel.Balance = merchantModel.Balance;
            viewModel.SettlementSchedule = (Int32)merchantModel.SettlementSchedule;
            viewModel.AvailableBalance = merchantModel.AvailableBalance;
            viewModel.Addresses = ConvertFrom(merchantModel.Addresses);
            viewModel.Contacts = ConvertFrom(merchantModel.Contacts);
            viewModel.Operators = ConvertFrom(merchantModel.Operators);
            viewModel.Devices = ConvertFrom(merchantModel.Devices);

            return viewModel;
        }

        public static MerchantBalanceHistoryListViewModel ConvertFrom(List<MerchantBalanceHistory> merchantBalanceModel)
        {
            if (merchantBalanceModel == null)
            {
                return new MerchantBalanceHistoryListViewModel();
            }

            MerchantBalanceHistoryListViewModel viewModel = new MerchantBalanceHistoryListViewModel();
            viewModel.MerchantBalanceHistoryViewModels = new List<MerchantBalanceHistoryViewModel>();

            merchantBalanceModel.ForEach(h =>
                                         {
                                             viewModel.MerchantBalanceHistoryViewModels.Add(new MerchantBalanceHistoryViewModel
                                                                                            {
                                                                                                MerchantId = h.MerchantId,
                                                                                                Balance = h.Balance,
                                                                                                ChangeAmount = h.ChangeAmount,
                                                                                                EntryDateTime = h.EntryDateTime,
                                                                                                EntryType = h.EntryType,
                                                                                                EstateId = h.EstateId,
                                                                                                EventId = h.EventId,
                                                                                                In = h.In,
                                                                                                Out = h.Out,
                                                                                                Reference = h.Reference,
                                                                                                TransactionId = h.TransactionId
                                                                                            });
                                         });

            return viewModel;
        }

        public static MakeMerchantDepositModel ConvertFrom(MakeMerchantDepositViewModel makeMerchantDepositViewModel)
        {
            if (makeMerchantDepositViewModel == null)
            {
                throw new ArgumentNullException(nameof(makeMerchantDepositViewModel));
            }

            MakeMerchantDepositModel makeMerchantDepositModel = new MakeMerchantDepositModel();

            makeMerchantDepositModel.DepositDateTime = DateTime.ParseExact(makeMerchantDepositViewModel.DepositDate, "dd/MM/yyyy", null);
            makeMerchantDepositModel.Amount = decimal.Parse(makeMerchantDepositViewModel.Amount);
            makeMerchantDepositModel.Reference = makeMerchantDepositViewModel.Reference;
            makeMerchantDepositModel.MerchantId = Guid.Parse(makeMerchantDepositViewModel.MerchantId);

            return makeMerchantDepositModel;
        }

        public static AddProductToContractModel ConvertFrom(CreateContractProductViewModel createContractProductViewModel)
        {
            if (createContractProductViewModel == null)
            {
                throw new ArgumentNullException(nameof(createContractProductViewModel));
            }

            AddProductToContractModel addProductToContractModel = new AddProductToContractModel();

            addProductToContractModel.Value = createContractProductViewModel.Value;
            addProductToContractModel.DisplayText = createContractProductViewModel.DisplayText;
            addProductToContractModel.ProductName = createContractProductViewModel.ProductName;
            addProductToContractModel.ProductType = Int32.Parse(createContractProductViewModel.ProductType);

            return addProductToContractModel;
        }

        public static AddTransactionFeeToContractProductModel ConvertFrom(CreateContractProductTransactionFeeViewModel createContractProductTransactionFeeViewModel)
        {
            if (createContractProductTransactionFeeViewModel == null)
            {
                throw new ArgumentNullException(nameof(createContractProductTransactionFeeViewModel));
            }

            AddTransactionFeeToContractProductModel addTransactionFeeToContractProductModel = new AddTransactionFeeToContractProductModel();

            addTransactionFeeToContractProductModel.Value = createContractProductTransactionFeeViewModel.Value;
            
            Int32 calculationType = createContractProductTransactionFeeViewModel.CalculationType - 1;
            addTransactionFeeToContractProductModel.CalculationType = (CalculationType)calculationType;
            addTransactionFeeToContractProductModel.Description = createContractProductTransactionFeeViewModel.FeeDescription;
            Int32 feeType = createContractProductTransactionFeeViewModel.FeeType - 1;
            addTransactionFeeToContractProductModel.FeeType = (FeeType)feeType;

            return addTransactionFeeToContractProductModel;
        }
        
        public static List<FileImportLogViewModel> ConvertFrom(List<FileImportLogModel> models)
        {
            if (models == null || models.Any() == false)
            {
                return new List<FileImportLogViewModel>();
            }

            List<FileImportLogViewModel> viewModels = new List<FileImportLogViewModel>();

            foreach (FileImportLogModel fileImportLogModel in models)
            {
                viewModels.Add(ConvertFrom(fileImportLogModel));
            }

            return viewModels;
        }

        public static FileImportLogViewModel ConvertFrom(FileImportLogModel model)
        {
            if (model == null)
            {
                return null;
            }

            FileImportLogViewModel viewModel = new FileImportLogViewModel
            {
                FileCount = model.FileCount,
                FileImportLogId = model.FileImportLogId,
                Files = new List<FileImportLogFileViewModel>(),
                ImportLogDate = model.ImportLogDate,
                ImportLogDateTime = model.ImportLogDateTime,
                ImportLogTime = model.ImportLogTime
            };

            if (model.Files.Any())
            {
                foreach (FileImportLogFileModel fileImportLogFileModel in model.Files)
                {
                    viewModel.Files.Add(new FileImportLogFileViewModel
                    {
                        FileImportLogId = fileImportLogFileModel.FileImportLogId,
                        FileId = fileImportLogFileModel.FileId,
                        FilePath = fileImportLogFileModel.FilePath,
                        FileProfileId = fileImportLogFileModel.FileProfileId,
                        FileUploadedDateTime = fileImportLogFileModel.FileUploadedDateTime,
                        MerchantId = fileImportLogFileModel.MerchantId,
                        OriginalFileName = fileImportLogFileModel.OriginalFileName,
                        UserId = fileImportLogFileModel.UserId
                    });
                }
            }

            return viewModel;
        }

        public static FileDetailsViewModel ConvertFrom(FileDetailsModel model)
        {
            if (model == null)
            {
                return null;
            }


            FileDetailsViewModel viewModel = new FileDetailsViewModel
                                             {
                                                 ProcessingSummary = new FileProcessingSummaryViewModel
                                                                     {
                                                                         TotalLines = model.ProcessingSummary.TotalLines,
                                                                         IgnoredLines = model.ProcessingSummary.IgnoredLines,
                                                                         SuccessfullyProcessedLines = model.ProcessingSummary.SuccessfullyProcessedLines,
                                                                         FailedLines = model.ProcessingSummary.FailedLines,
                                                                         NotProcessedLines = model.ProcessingSummary.NotProcessedLines,
                                                                         RejectedLines = model.ProcessingSummary.RejectedLines
                                                                     },
                                                 ProcessingCompleted = model.ProcessingCompleted,
                                                 EstateId = model.EstateId,
                                                 FileId = model.FileId,
                                                 FileImportLogId = model.FileImportLogId,
                                                 FileLines = new List<FileLineViewModel>(),
                                                 FileLocation = model.FileLocation,
                                                 FileProfileId = model.FileProfileId,
                                                 MerchantId = model.MerchantId,
                                                 UserId = model.UserId
                                             };

            if (model.FileLines.Any())
            {
                foreach (FileLineModel modelFileLine in model.FileLines)
                {
                    FileLineViewModel fileLineViewModel = new FileLineViewModel
                                                          {
                                                              LineData = modelFileLine.LineData,
                                                              LineNumber = modelFileLine.LineNumber,
                                                              TransactionId = modelFileLine.TransactionId,
                                                              RejectionReason = modelFileLine.RejectionReason
                                                          };

                    // Translate the processing result
                    (FileLineProcessingResult result, String stringResult) processingResult = ConvertFrom(modelFileLine.ProcessingResult);

                    fileLineViewModel.ProcessingResult = processingResult.result;
                    fileLineViewModel.ProcessingResultString = processingResult.stringResult;

                    viewModel.FileLines.Add(fileLineViewModel);

                }
            }

            return viewModel;
        }

        public static List<ContractProductTypeViewModel> ConvertFrom(List<ContractProductTypeModel> contractProductTypeModels){
            List<ContractProductTypeViewModel> result = new List<ContractProductTypeViewModel>();

            foreach (ContractProductTypeModel contractProductTypeModel in contractProductTypeModels){
                result.Add(new ContractProductTypeViewModel{
                                                               Description = contractProductTypeModel.Description,
                                                               ProductType = contractProductTypeModel.ProductType.ToString()
                                                           });
            }
            return result;
        }

        public static (FileLineProcessingResult result,String stringResult) ConvertFrom(BusinessLogic.Models.FileLineProcessingResult model){
            (FileLineProcessingResult, String) viewModel;
            switch (model)
            {
                case BusinessLogic.Models.FileLineProcessingResult.Failed:
                    viewModel = (FileLineProcessingResult.Failed,"Failed");
                    break;
                case BusinessLogic.Models.FileLineProcessingResult.Unknown:
                    viewModel = (FileLineProcessingResult.Unknown, "Unknown");
                    break;
                case BusinessLogic.Models.FileLineProcessingResult.Ignored:
                    viewModel = (FileLineProcessingResult.Ignored, "Ignored");
                    break;
                case BusinessLogic.Models.FileLineProcessingResult.NotProcessed:
                    viewModel = (FileLineProcessingResult.NotProcessed, "Not Processed");
                    break;
                case BusinessLogic.Models.FileLineProcessingResult.Rejected:
                    viewModel = (FileLineProcessingResult.Rejected, "Rejected");
                    break;
                case BusinessLogic.Models.FileLineProcessingResult.Successful:
                    viewModel = (FileLineProcessingResult.Successful, "Successful");
                    break;
                default:
                    viewModel = (FileLineProcessingResult.Unknown, "Unknown");
                    break;
            }

            return viewModel;
        }

        //private static Dictionary<String, String> ConvertFrom(Dictionary<Guid, String> deviceModels)
        //{
        //    Dictionary<String, String> viewModels = new Dictionary<String, String>();

        //    if (deviceModels == null || deviceModels.Any() == false)
        //    {
        //        return viewModels;
        //    }

        //    foreach (KeyValuePair<Guid, String> model in deviceModels)
        //    {
        //        viewModels.Add(model.Key.ToString(), model.Value);
        //    }

        //    return viewModels;
        //}

        private static List<AddressViewModel> ConvertFrom(List<AddressModel> addressModels)
        {
            List<AddressViewModel> viewModels = new List<AddressViewModel>();

            if (addressModels == null || addressModels.Any() == false)
            {
                return viewModels;
            }

            foreach (AddressModel model in addressModels)
            {
                viewModels.Add(ConvertFrom(model));
            }

            return viewModels;
        }

        private static AddressViewModel ConvertFrom(AddressModel addressModel)
        {
            return new AddressViewModel
                   {
                       AddressId = addressModel.AddressId,
                       AddressLine1 = addressModel.AddressLine1,
                       AddressLine2 = addressModel.AddressLine2,
                       AddressLine3 = addressModel.AddressLine3,
                       AddressLine4 = addressModel.AddressLine4,
                       Country = addressModel.Country,
                       PostalCode = addressModel.PostalCode,
                       Region = addressModel.Region,
                       Town = addressModel.Town
                   };
        }

        private static List<ContactViewModel> ConvertFrom(List<ContactModel> contactModels)
        {
            List<ContactViewModel> viewModels = new List<ContactViewModel>();

            if (contactModels == null)
            {
                return viewModels;
            }

            foreach (ContactModel model in contactModels)
            {
                viewModels.Add(ConvertFrom(model));
            }

            return viewModels;
        }

        private static ContactViewModel ConvertFrom(ContactModel contactModel)
        {
            return new ContactViewModel
                   {
                       ContactEmailAddress = contactModel.ContactEmailAddress,
                       ContactId = contactModel.ContactId,
                       ContactName = contactModel.ContactName,
                       ContactPhoneNumber = contactModel.ContactPhoneNumber
                   };
        }

        private static List<MerchantOperatorViewModel> ConvertFrom(List<MerchantOperatorModel> operatorModels)
        {
            List<MerchantOperatorViewModel> viewModels = new List<MerchantOperatorViewModel>();

            if (operatorModels == null || operatorModels.Any() == false)
            {
                return viewModels;
            }

            foreach (MerchantOperatorModel model in operatorModels)
            {
                viewModels.Add(ConvertFrom(model));
            }

            return viewModels;
        }

        private static MerchantOperatorViewModel ConvertFrom(MerchantOperatorModel operatorModel)
        {
            return new MerchantOperatorViewModel
                   {
                       MerchantNumber = operatorModel.MerchantNumber,
                       Name = operatorModel.Name,
                       OperatorId = operatorModel.OperatorId,
                       TerminalNumber = operatorModel.TerminalNumber
                   };
        }

        #endregion
    }
}