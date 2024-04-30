namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EstateManagement.DataTransferObjects;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Requests.Contract;
    using EstateManagement.DataTransferObjects.Requests.Merchant;
    using EstateManagement.DataTransferObjects.Requests.Operator;
    using EstateManagement.DataTransferObjects.Responses.Contract;
    using EstateManagement.DataTransferObjects.Responses.Estate;
    using EstateManagement.DataTransferObjects.Responses.Merchant;
    using EstateManagement.DataTransferObjects.Responses.Operator;
    using EstateReportingAPI.DataTransferObjects;
    using EstateReportingAPI.DataTrasferObjects;
    using FileProcessor.DataTransferObjects.Responses;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.EntityFrameworkCore.Internal;
    using Models;
    using FileLineProcessingResult = Models.FileLineProcessingResult;
    using SettlementSchedule = EstateManagement.DataTransferObjects.Responses.Merchant.SettlementSchedule;
    using TransactionProcessor.DataTransferObjects;
    using CalculationType = EstateManagement.DataTransferObjects.Responses.Contract.CalculationType;
    using FeeType = EstateManagement.DataTransferObjects.Responses.Contract.FeeType;
    using MerchantResponse = EstateManagement.DataTransferObjects.Responses.Merchant.MerchantResponse;

    public static class ModelFactory
    {

        #region Methods

        public static LastSettlementModel ConvertFrom(LastSettlement source){
            if (source == null){
                return null;
            }

            LastSettlementModel model = new LastSettlementModel{
                                                                   FeesValue = source.FeesValue,
                                                                   SalesCount = source.SalesCount,
                                                                   SalesValue = source.SalesValue,
                                                                   SettlementDate = source.SettlementDate
                                                               };
            return model;
        }

        public static List<MerchantListModel> ConvertFrom(List<Merchant> source){
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<MerchantListModel> result = new List<MerchantListModel>();
            foreach (Merchant merchant in source){
                result.Add(new MerchantListModel{
                    MerchantId = merchant.MerchantId,
                    MerchantName = merchant.Name
                });
            }
            return result;
        }

        public static List<OperatorListModel> ConvertFrom(List<Operator> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<OperatorListModel> result = new List<OperatorListModel>();
            foreach (Operator @operator in source){
                result.Add(new OperatorListModel{
                                                    OperatorId = @operator.OperatorId,
                                                    OperatorName = @operator.Name
                                                });
            }
            return result;
        }

        public static EstateModel ConvertFrom(EstateResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            EstateModel model = new EstateModel
                                {
                                    EstateId = source.EstateId,
                                    EstateName = source.EstateName,
                                    Operators = ConvertOperators(source.Operators),
                                    SecurityUsers = ConvertSecurityUsers(source.SecurityUsers)
                                };
            return model;
        }
        
        public static MerchantBalanceModel ConvertFrom(MerchantBalanceResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            MerchantBalanceModel model = new MerchantBalanceModel
                                         {
                                             MerchantId = source.MerchantId,
                                             AvailableBalance = source.AvailableBalance,
                                             Balance = source.Balance,
                                             EstateId = source.EstateId
                                         };

            return model;
        }
        
        public static CreateOperatorRequest ConvertFrom(CreateOperatorModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateOperatorRequest apiRequest = new CreateOperatorRequest
                                               {
                                                   Name = source.OperatorName,
                                                   RequireCustomTerminalNumber = source.RequireCustomTerminalNumber,
                                                   RequireCustomMerchantNumber = source.RequireCustomMerchantNumber
                                               };

            return apiRequest;
        }

        public static CreateContractRequest ConvertFrom(CreateContractModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateContractRequest apiRequest = new CreateContractRequest
                                               {
                                                   Description = source.Description,
                                                   OperatorId = source.OperatorId
                                               };

            return apiRequest;
        }

        public static CreateOperatorResponseModel ConvertFrom(CreateOperatorResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateOperatorResponseModel createOperatorResponseModel = new CreateOperatorResponseModel
                                                                      {
                                                                          OperatorId = source.OperatorId,
                                                                          EstateId = source.EstateId
                                                                      };

            return createOperatorResponseModel;
        }

        public static CreateContractResponseModel ConvertFrom(CreateContractResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateContractResponseModel createOperatorResponseModel = new CreateContractResponseModel
                                                                      {
                                                                          OperatorId = source.OperatorId,
                                                                          EstateId = source.EstateId,
                                                                          ContractId = source.ContractId
                                                                      };

            return createOperatorResponseModel;
        }

        public static AddProductToContractRequest ConvertFrom(AddProductToContractModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AddProductToContractRequest addProductToContractRequest = new AddProductToContractRequest
                                                                      {
                                                                          Value = source.Value,
                                                                          ProductName = source.ProductName,
                                                                          DisplayText = source.DisplayText,
                                                                          ProductType = (ProductType)source.ProductType,
                                                                      };

            return addProductToContractRequest;
        }

        public static AddMerchantDeviceRequest ConvertFrom(AddMerchantDeviceModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AddMerchantDeviceRequest addMerchantDeviceRequest = new AddMerchantDeviceRequest
                                                                {
                                                                    DeviceIdentifier = source.DeviceIdentifier
                                                                };

            return addMerchantDeviceRequest;
        }

        public static AddMerchantDeviceResponseModel ConvertFrom(AddMerchantDeviceResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AddMerchantDeviceResponseModel addMerchantDeviceResponseModel = new AddMerchantDeviceResponseModel
                                                                            {
                                                                                MerchantId = source.MerchantId,
                                                                                DeviceId = source.DeviceId,
                                                                                EstateId = source.EstateId
                                                                            };

            return addMerchantDeviceResponseModel;
        }

        public static AddProductToContractResponseModel ConvertFrom(AddProductToContractResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AddProductToContractResponseModel addProductToContractResponseModel = new AddProductToContractResponseModel
                                                                                  {
                                                                                      EstateId = source.EstateId,
                                                                                      ProductId = source.ProductId,
                                                                                      ContractId = source.ContractId
                                                                                  };

            return addProductToContractResponseModel;
        }

        public static AddTransactionFeeForProductToContractRequest ConvertFrom(AddTransactionFeeToContractProductModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CalculationType calculationType = Enum.Parse<CalculationType>(source.CalculationType.ToString(), true);
            FeeType feeType = Enum.Parse<FeeType>(source.FeeType.ToString(), true);
            AddTransactionFeeForProductToContractRequest addTransactionFeeForProductToContractRequest = new AddTransactionFeeForProductToContractRequest
                                                                                                        {
                                                                                                            Value = source.Value,
                                                                                                            Description = source.Description,
                                                                                                            FeeType = feeType,
                                                                                                            CalculationType = calculationType
                                                                                                        };

            return addTransactionFeeForProductToContractRequest;


        }

        public static AddTransactionFeeToContractProductResponseModel ConvertFrom(AddTransactionFeeForProductToContractResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AddTransactionFeeToContractProductResponseModel addTransactionFeeToContractProductResponseModel = new AddTransactionFeeToContractProductResponseModel
                                                                                                              {
                                                                                                                  EstateId = source.EstateId,
                                                                                                                  ProductId = source.ProductId,
                                                                                                                  TransactionFeeId = source.TransactionFeeId,
                                                                                                                  ContractId = source.ContractId
                                                                                                              };

            return addTransactionFeeToContractProductResponseModel;
        }

        public static List<MerchantBalanceHistory> ConvertFrom(List<MerchantBalanceChangedEntryResponse> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<MerchantBalanceHistory> model = new List<MerchantBalanceHistory>();

            source.ForEach(s =>
                           {
                               model.Add(new MerchantBalanceHistory
                                         {
                                             MerchantId = s.MerchantId,
                                             Balance = s.Balance,
                                             ChangeAmount = s.ChangeAmount,
                                             EntryDateTime = s.DateTime,
                                             EntryType = s.DebitOrCredit,
                                             EstateId = s.EstateId,
                                             EventId = s.OriginalEventId,
                                             //In = s.In,
                                             //Out = s.Out,
                                             Reference = s.Reference,
                                             //TransactionId = s.TransactionId
                                                         
                                         });
                               });

            return model;
        }

        public static FileDetailsModel ConvertFrom(FileDetails source)
        {
            if (source == null)
            {
                return null;
            }

            FileDetailsModel model = new FileDetailsModel
                                     {
                                         EstateId = source.EstateId,
                                         FileId = source.FileId,
                                         FileImportLogId = source.FileImportLogId,
                                         FileLines = new List<FileLineModel>(),
                                         FileLocation = source.FileLocation,
                                         FileProfileId = source.FileProfileId,
                                         MerchantId = source.MerchantId,
                                         ProcessingCompleted = source.ProcessingCompleted,
                                         ProcessingSummary = new FileProcessingSummaryModel
                                                             {
                                                                 FailedLines = source.ProcessingSummary.FailedLines,
                                                                 IgnoredLines = source.ProcessingSummary.IgnoredLines,
                                                                 NotProcessedLines = source.ProcessingSummary.NotProcessedLines,
                                                                 RejectedLines = source.ProcessingSummary.RejectedLines,
                                                                 SuccessfullyProcessedLines = source.ProcessingSummary.SuccessfullyProcessedLines,
                                                                 TotalLines = source.ProcessingSummary.TotalLines
                                                             },
                                         UserId = source.UserId
                                     };

            foreach (FileLine sourceFileLine in source.FileLines)
            {
                model.FileLines.Add(new FileLineModel
                                    {
                                        LineData = sourceFileLine.LineData,
                                        LineNumber = sourceFileLine.LineNumber,
                                        ProcessingResult = ConvertFrom(sourceFileLine.ProcessingResult),
                                        RejectionReason = sourceFileLine.RejectionReason,
                                        TransactionId = sourceFileLine.TransactionId
                                    });
            }

            return model;
        }

        public static AssignOperatorRequest ConvertFrom(AssignOperatorToMerchantModel source)
        {
            if (source == null)
            {
                return null;
            }

            AssignOperatorRequest assignOperatorRequest = new AssignOperatorRequest
                                                          {
                                                              MerchantNumber = source.MerchantNumber,
                                                              TerminalNumber = source.TerminalNumber,
                                                              OperatorId = source.OperatorId
                                                          };

            return assignOperatorRequest;
        }

        public static AssignOperatorToMerchantResponseModel ConvertFrom(AssignOperatorResponse source)
        {
            if (source == null)
            {
                return null;
            }

            AssignOperatorToMerchantResponseModel assignOperatorToMerchantResponseModel = new AssignOperatorToMerchantResponseModel
                                                                                          {
                                                                                              EstateId = source.EstateId,
                                                                                              MerchantId = source.MerchantId,
                                                                                              OperatorId = source.OperatorId
                                                                                          };

            return assignOperatorToMerchantResponseModel;
        }

        public static FileLineProcessingResult ConvertFrom(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult dto)
        {
            FileLineProcessingResult model = FileLineProcessingResult.Unknown;
            switch (dto)
            {
                case FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Failed:
                    model = FileLineProcessingResult.Failed;
                    break;
                case FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Unknown:
                    model = FileLineProcessingResult.Unknown;
                    break;
                case FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Ignored:
                    model = FileLineProcessingResult.Ignored;
                    break;
                case FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.NotProcessed:
                    model = FileLineProcessingResult.NotProcessed;
                    break;
                case FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Rejected:
                    model = FileLineProcessingResult.Rejected;
                    break;
                case FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Successful:
                    model = FileLineProcessingResult.Successful;
                    break;
                default:
                    model = FileLineProcessingResult.Unknown;
                    break;
            }

            return model;
        }

        public static List<MerchantModel> ConvertFrom(List<MerchantResponse> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<MerchantModel> models = new List<MerchantModel>();

            foreach (MerchantResponse merchantResponse in source)
            {
                MerchantModel merchantModel = ConvertFrom(merchantResponse, null);

                models.Add(merchantModel);
            }

            return models;
        }
        
        public static List<ContractModel> ConvertFrom(List<ContractResponse> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<ContractModel> models = new List<ContractModel>();

            foreach (ContractResponse contractResponse in source)
            {
                ContractModel contractModel = ConvertFrom(contractResponse);

                models.Add(contractModel);
            }

            return models;
        }

        public static ContractModel ConvertFrom(ContractResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ContractModel contractModel = new ContractModel
                                          {
                                              EstateId = source.EstateId,
                                              OperatorName = source.OperatorName,
                                              OperatorId = source.OperatorId,
                                              ContractId = source.ContractId,
                                              Description = source.Description,
                                              NumberOfProducts = source.Products?.Count ?? 0,
                                              ContractProducts = new List<ContractProductModel>()
                                          };

            if (source.Products != null && source.Products.Any())
            {
                source.Products.ForEach(p =>
                                        {
                                            ContractProductModel contractProductModel = new ContractProductModel
                                                                                        {
                                                                                            Description = source.Description,
                                                                                            ContractId = source.ContractId,
                                                                                            EstateId = source.EstateId,
                                                                                            DisplayText = p.DisplayText,
                                                                                            Value = p.Value,
                                                                                            ProductName = p.Name,
                                                                                            ProductType = (Int32)p.ProductType,
                                                                                            ContractProductId = p.ProductId,
                                                                                            ContractProductTransactionFees =
                                                                                                new List<ContractProductTransactionFeeModel>()
                                                                                        };

                                            if (p.TransactionFees != null && p.TransactionFees.Any())
                                            {
                                                p.TransactionFees.ForEach(f =>
                                                                          {
                                                                              contractProductModel
                                                                                  .ContractProductTransactionFees.Add(new ContractProductTransactionFeeModel
                                                                                                                      {
                                                                                                                          Description = f.Description,
                                                                                                                          Value = f.Value.ToString(),
                                                                                                                          CalculationType = f.CalculationType.ToString(),
                                                                                                                          FeeType = f.FeeType.ToString(),
                                                                                                                          TransactionFeeId = f.TransactionFeeId
                                                                                                                      });
                                                                          });
                                            }

                                            contractModel.ContractProducts.Add(contractProductModel);
                                        });
            }

            return contractModel;
        }

        public static MerchantModel ConvertFrom(MerchantResponse merchantResponse, MerchantBalanceResponse merchantBalanceResponse)
        {
            if (merchantResponse == null)
            {
                throw new ArgumentNullException(nameof(merchantResponse));
            }
            
            MerchantModel merchantModel = new MerchantModel
                                          {
                                              EstateId = merchantResponse.EstateId,
                                              MerchantId = merchantResponse.MerchantId,
                                              MerchantName = merchantResponse.MerchantName,
                                              SettlementSchedule = ConvertFrom(merchantResponse.SettlementSchedule),
                                          };

            if (merchantResponse.Addresses != null && merchantResponse.Addresses.Any())
            {
                merchantModel.Addresses = new List<AddressModel>();
                merchantResponse.Addresses.ForEach(a => merchantModel.Addresses.Add(new AddressModel
                                                                                    {
                                                                                        AddressId = a.AddressId,
                                                                                        AddressLine1 = a.AddressLine1,
                                                                                        AddressLine2 = a.AddressLine2,
                                                                                        AddressLine3 = a.AddressLine3,
                                                                                        AddressLine4 = a.AddressLine4,
                                                                                        Country = a.Country,
                                                                                        PostalCode = a.PostalCode,
                                                                                        Region = a.Region,
                                                                                        Town = a.Town,
                                                                                    }));
            }

            if (merchantResponse.Contacts != null && merchantResponse.Contacts.Any())
            {
                merchantModel.Contacts = new List<ContactModel>();
                merchantResponse.Contacts.ForEach(c => merchantModel.Contacts.Add(new ContactModel
                                                                                  {
                                                                                      ContactEmailAddress = c.ContactEmailAddress,
                                                                                      ContactId = c.ContactId,
                                                                                      ContactName = c.ContactName,
                                                                                      ContactPhoneNumber = c.ContactPhoneNumber
                                                                                  }));
            }

            if (merchantResponse.Operators != null && merchantResponse.Operators.Any())
            {
                merchantModel.Operators = new List<MerchantOperatorModel>();
                merchantResponse.Operators.ForEach(o => merchantModel.Operators.Add(new MerchantOperatorModel
                                                                                    {
                                                                                        MerchantNumber = o.MerchantNumber,
                                                                                        Name = o.Name,
                                                                                        OperatorId = o.OperatorId,
                                                                                        TerminalNumber = o.TerminalNumber
                                                                                    }));
            }

            if (merchantResponse.Devices != null && merchantResponse.Devices.Any()){
                merchantModel.Devices = new List<MerchantDeviceModel>();
                foreach (KeyValuePair<Guid, String> merchantResponseDevice in merchantResponse.Devices)
                {
                    merchantModel.Devices.Add(
                                              new MerchantDeviceModel{
                                                                         DeviceId = merchantResponseDevice.Key,
                                                                         DeviceIdentifier = merchantResponseDevice.Value
                                              });
                }
            }

            if (merchantBalanceResponse != null) {
                merchantModel.AvailableBalance = merchantBalanceResponse.AvailableBalance;
                merchantModel.Balance = merchantBalanceResponse.Balance;
            }

            return merchantModel;
        }

        public static CreateMerchantResponseModel ConvertFrom(CreateMerchantResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new CreateMerchantResponseModel
                   {
                       MerchantId = source.MerchantId,
                       EstateId = source.EstateId
                   };
        }

        public static CreateMerchantRequest ConvertFrom(CreateMerchantModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateMerchantRequest apiRequest = new CreateMerchantRequest
                                               {
                                                   Address = new Address
                                                             {
                                                                 AddressLine1 = source.Address.AddressLine1,
                                                                 AddressLine2 = source.Address.AddressLine2,
                                                                 AddressLine3 = source.Address.AddressLine3,
                                                                 AddressLine4 = source.Address.AddressLine4,
                                                                 Country = source.Address.Country,
                                                                 PostalCode = source.Address.PostalCode,
                                                                 Region = source.Address.Region,
                                                                 Town = source.Address.Town
                                                             },
                                                   Contact = new Contact
                                                             {
                                                                 ContactName = source.Contact.ContactName,
                                                                 EmailAddress = source.Contact.ContactEmailAddress,
                                                                 PhoneNumber = source.Contact.ContactPhoneNumber
                                                             },
                                                   Name = source.MerchantName,
                                                   SettlementSchedule = ConvertFrom(source.SettlementSchedule)
                                               };

            return apiRequest;
        }

        public static SettlementSchedule ConvertFrom(Models.SettlementSchedule settlementSchedule)
        {
            return settlementSchedule switch
            {
                Models.SettlementSchedule.Immediate => SettlementSchedule.Immediate,
                Models.SettlementSchedule.Weekly => SettlementSchedule.Weekly,
                Models.SettlementSchedule.Monthly=> SettlementSchedule.Monthly,
            };
        }

        public static Models.SettlementSchedule ConvertFrom(EstateManagement.DataTransferObjects.Responses.Merchant.SettlementSchedule settlementSchedule)
        {
            return settlementSchedule switch
            {
                SettlementSchedule.Immediate => Models.SettlementSchedule.Immediate,
                SettlementSchedule.Weekly => Models.SettlementSchedule.Weekly,
                SettlementSchedule.Monthly => Models.SettlementSchedule.Monthly,
                SettlementSchedule.NotSet => Models.SettlementSchedule.Immediate
            };
        }

        public static MakeMerchantDepositRequest ConvertFrom(MakeMerchantDepositModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            MakeMerchantDepositRequest apiRequest = new MakeMerchantDepositRequest
                                                    {
                                                        DepositDateTime = source.DepositDateTime,
                                                        Reference = source.Reference,
                                                        Amount = source.Amount
                                                    };

            return apiRequest;
        }

        public static List<FileImportLogModel> ConvertFrom(FileImportLogList source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<FileImportLogModel> models = new List<FileImportLogModel>();

            if (source.FileImportLogs.Any())
            {
                foreach (FileImportLog sourceFileImportLog in source.FileImportLogs)
                {
                    models.Add(ConvertFrom(sourceFileImportLog));
                }
            }

            return models;
        }

        public static FileImportLogModel ConvertFrom(FileImportLog source)
        {
            FileImportLogModel model = new FileImportLogModel
                                       {
                                           FileCount = source.FileCount,
                                           FileImportLogId = source.FileImportLogId,
                                           ImportLogDate = source.ImportLogDate,
                                           ImportLogDateTime = source.ImportLogDateTime,
                                           ImportLogTime = source.ImportLogTime
                                       };

            if (source.Files.Any())
            {
                model.Files = new List<FileImportLogFileModel>();
                foreach (FileImportLogFile fileImportLogFile in source.Files)
                {
                    model.Files.Add(new FileImportLogFileModel
                                    {
                                        FileImportLogId = fileImportLogFile.FileImportLogId,
                                        FileId = fileImportLogFile.FileId,
                                        FilePath = fileImportLogFile.FilePath,
                                        FileProfileId = fileImportLogFile.FileProfileId,
                                        FileUploadedDateTime = fileImportLogFile.FileUploadedDateTime,
                                        MerchantId = fileImportLogFile.MerchantId,
                                        OriginalFileName = fileImportLogFile.OriginalFileName,
                                        UserId = fileImportLogFile.UserId
                                    });
                }
            }

            return model;
        }

        public static MakeMerchantDepositResponseModel ConvertFrom(MakeMerchantDepositResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new MakeMerchantDepositResponseModel
                   {
                       MerchantId = source.MerchantId,
                       DepositId = source.DepositId,
                       EstateId = source.EstateId
                   };
        }

        public static List<EstateOperatorModel> ConvertOperators(List<EstateOperatorResponse> estateResponseOperators)
        {
            if (estateResponseOperators == null || estateResponseOperators.Any() == false)
            {
                return null;
            }

            List<EstateOperatorModel> models = new List<EstateOperatorModel>();
            foreach (EstateOperatorResponse estateOperatorResponse in estateResponseOperators)
            {
                models.Add(new EstateOperatorModel
                           {
                               Name = estateOperatorResponse.Name,
                               OperatorId = estateOperatorResponse.OperatorId,
                               RequireCustomMerchantNumber = estateOperatorResponse.RequireCustomMerchantNumber,
                               RequireCustomTerminalNumber = estateOperatorResponse.RequireCustomTerminalNumber
                           });
            }

            return models;
        }

        public static List<SecurityUserModel> ConvertSecurityUsers(List<SecurityUserResponse> estateResponseSecurityUsers)
        {
            if (estateResponseSecurityUsers == null || estateResponseSecurityUsers.Any() == false)
            {
                return null;
            }

            List<SecurityUserModel> models = new List<SecurityUserModel>();
            foreach (SecurityUserResponse estateResponseSecurityUser in estateResponseSecurityUsers)
            {
                models.Add(new SecurityUserModel
                           {
                               EmailAddress = estateResponseSecurityUser.EmailAddress,
                               SecurityUserId = estateResponseSecurityUser.SecurityUserId
                           });
            }

            return models;
        }

        public static List<CalendarDateModel> ConvertFrom(List<CalendarDate> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<CalendarDateModel> models = new List<CalendarDateModel>();
            source.ForEach(s => {
                               models.Add(new CalendarDateModel{
                                                                   Date = s.Date,
                                                                   DayOfWeek = s.DayOfWeek,
                                                                   DayOfWeekNumber = s.DayOfWeekNumber,
                                                               });
                           });

            return models;
        }

        public static List<CalendarYearModel> ConvertFrom(List<CalendarYear> source){
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<CalendarYearModel> models = new List<CalendarYearModel>();
            source.ForEach(s => {
                               models.Add(new CalendarYearModel
                               {
                                              Year = s.Year
                                          });
                           });

            return models;
        }

        public static List<ComparisonDateModel> ConvertFrom(List<ComparisonDate> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<ComparisonDateModel> models = new List<ComparisonDateModel>();
            source.ForEach(s => {
                               models.Add(new ComparisonDateModel
                               {
                                              Date = s.Date,
                                              Description = s.Description,
                                              OrderValue = s.OrderValue
                                          });
                           });

            return models;
        }

        public static TodaysSalesModel ConvertFrom(TodaysSales source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TodaysSalesModel model = new TodaysSalesModel{
                                                             TodaysSalesCount = source.TodaysSalesCount,
                                                             ComparisonSalesCount = source.ComparisonSalesCount,
                                                             ComparisonSalesValue = source.ComparisonSalesValue,
                                                             TodaysSalesValue = source.TodaysSalesValue
                                                         };
            return model;
        }

        public static List<TodaysSalesCountByHourModel> ConvertFrom(List<TodaysSalesCountByHour> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<TodaysSalesCountByHourModel> models = new List<TodaysSalesCountByHourModel>();

            source.ForEach(s => {
                               models.Add(new TodaysSalesCountByHourModel{
                                                                             ComparisonSalesCount = s.ComparisonSalesCount,
                                                                             Hour = s.Hour,
                                                                             TodaysSalesCount = s.TodaysSalesCount,
                                                                         });
                           });
            return models;
        }

        public static List<TodaysSalesValueByHourModel> ConvertFrom(List<TodaysSalesValueByHour> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<TodaysSalesValueByHourModel> models = new List<TodaysSalesValueByHourModel>();
            source.ForEach(s => {
                               models.Add(new TodaysSalesValueByHourModel
                               {
                                              ComparisonSalesValue = s.ComparisonSalesValue,
                                              Hour = s.Hour,
                                              TodaysSalesValue = s.TodaysSalesValue,
                                          });
                           });
            return models;
        }

        #endregion

        public static TodaysSettlementModel ConvertFrom(TodaysSettlement source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TodaysSettlementModel model = new TodaysSettlementModel{
                                                                       ComparisonSettlementCount = source.ComparisonSettlementCount,
                                                                       ComparisonSettlementValue = source.ComparisonSettlementValue,
                                                                       TodaysSettlementCount = source.TodaysSettlementCount,
                                                                       TodaysSettlementValue = source.TodaysSettlementValue
                                                                   };
            return model;
        }

        public static MerchantKpiModel ConvertFrom(MerchantKpi source){
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            MerchantKpiModel model = new MerchantKpiModel{
                                                             MerchantsWithNoSaleInLast7Days = source.MerchantsWithNoSaleInLast7Days,
                                                             MerchantsWithNoSaleToday = source.MerchantsWithNoSaleToday,
                                                             MerchantsWithSaleInLastHour = source.MerchantsWithSaleInLastHour
                                                         };
            return model;
        }

        public static List<TopBottomOperatorDataModel> ConvertFrom(List<TopBottomOperatorData> source){
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<TopBottomOperatorDataModel> models = new List<TopBottomOperatorDataModel>();
            source.ForEach(s => {
                               models.Add(new TopBottomOperatorDataModel
                               {
                                              SalesValue = s.SalesValue,
                                              OperatorName = s.OperatorName,
                                          });
                           });
            return models;
        }

        public static List<TopBottomMerchantDataModel> ConvertFrom(List<TopBottomMerchantData> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<TopBottomMerchantDataModel> models = new List<TopBottomMerchantDataModel>();
            source.ForEach(s => {
                               models.Add(new TopBottomMerchantDataModel
                               {
                                              SalesValue = s.SalesValue,
                                              MerchantName = s.MerchantName,
                                          });
                           });
            return models;
        }

        public static List<TopBottomProductDataModel> ConvertFrom(List<TopBottomProductData> source)
        {
            if (source == null || source.Any() == false)
            {
                return null;
            }

            List<TopBottomProductDataModel> models = new List<TopBottomProductDataModel>();
            source.ForEach(s => {
                               models.Add(new TopBottomProductDataModel
                               {
                                              SalesValue = s.SalesValue,
                                              ProductName = s.ProductName,
                                          });
                           });
            return models;
        }
    }
}