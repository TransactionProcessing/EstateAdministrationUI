namespace EstateAdministrationUI.Testing
{
    using System;
    using System.Collections.Generic;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using EstateManagement.DataTransferObjects;
    using EstateManagement.DataTransferObjects.Responses;
    using EstateReporting.DataTransferObjects;
    using FileProcessor.DataTransferObjects.Responses;
    using DTOCalculationType = EstateManagement.DataTransferObjects.CalculationType;
    using DTOFeeType = EstateManagement.DataTransferObjects.FeeType;
    using FeeType = BusinessLogic.Models.FeeType;
    using CalculationType = BusinessLogic.Models.CalculationType;
    using FileLineProcessingResult = FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult;

    public class TestData
    {
        public static Guid EstateId = Guid.Parse("FC657462-2620-4B35-B744-1BD7E60C1748");

        public static String EstateName = "Test Estate 1";

        public static String OperatorName = "Test Operator 1";

        public static Guid OperatorId = Guid.Parse("DECA8293-F045-41C5-A2F7-30F2792FD273");

        public static Boolean RequireCustomMerchantNumber = true;

        public static Boolean RequireCustomTerminalNumber = true;

        public static String EmailAddress = "estateuser1@testestate1.co.uk";

        public static Guid SecurityUserId = Guid.Parse("6A3C1B74-F01E-4017-85D5-6E6082038AB8");

        public static String MerchantAddressLine1 = "Address Line 1";

        public static String MerchantAddressLine2 = "Address Line 2";

        public static String MerchantAddressLine3 = "Address Line 3";

        public static String MerchantAddressLine4 = "Address Line 4";

        public static String MerchantContactEmailAddress = "testcontact@merchant1.co.uk";

        public static String MerchantContactName = "Mr Test Contact";

        public static String MerchantContactPhoneNumber = "1234567890";

        public static String MerchantCountry = "United Kingdom";

        public static String MerchantName = "Test Merchant 1";

        public static String MerchantPostalCode = "TE571NG";

        public static String MerchantRegion = "Test Region";

        public static String MerchantTown = "Test Town";

        public static Guid MerchantId = Guid.Parse("BD9281C6-2906-4979-8E6B-F802E509FDDF");

        public static Decimal AvailableBalance = 1000.00m;

        public static Decimal Balance = 1000.00m;

        public static Guid AddressId = Guid.Parse("24F1E552-54F9-468F-81DD-E5899B2B8BDD");

        public static Guid ContactId = Guid.Parse("445AC5A8-B599-4143-B8EE-5FF23367EE5B");

        public static Guid DeviceId = Guid.Parse("7B6A1899-BAB0-4FD4-AEA0-BB7A0C085A35");

        public static String DeviceIdentifier = "Device1";

        public static String MerchantNumber = "00000001";

        public static String TerminalNumber = "10000001";

        public static EstateModel EstateModel =>
            new EstateModel
            {
                EstateId = TestData.EstateId,
                EstateName = TestData.EstateName,
                Operators = TestData.Operators,
                SecurityUsers = TestData.SecurityUsers
            };

        public static List<EstateOperatorModel> Operators =>
            new List<EstateOperatorModel>
            {
                new EstateOperatorModel
                {
                    Name = TestData.OperatorName,
                    OperatorId = TestData.OperatorId,
                    RequireCustomMerchantNumber = TestData.RequireCustomMerchantNumber,
                    RequireCustomTerminalNumber = TestData.RequireCustomTerminalNumber
                }
            };

        public static List<SecurityUserModel> SecurityUsers =>
            new List<SecurityUserModel>
            {
                new SecurityUserModel
                {
                    EmailAddress = TestData.EmailAddress,
                    SecurityUserId = TestData.SecurityUserId
                }
            };

        public static CreateMerchantViewModel CreateMerchantViewModel =>
            new CreateMerchantViewModel
            {
                MerchantName = TestData.MerchantName,
                ContactPhoneNumber = TestData.MerchantContactPhoneNumber,
                ContactName = TestData.MerchantContactName,
                ContactEmailAddress = TestData.MerchantContactEmailAddress,
                AddressLine4 = TestData.MerchantAddressLine4,
                AddressLine2 = TestData.MerchantAddressLine2,
                AddressLine3 = TestData.MerchantAddressLine3,
                Country = TestData.MerchantCountry,
                PostalCode = TestData.MerchantPostalCode,
                Region = TestData.MerchantRegion,
                Town = TestData.MerchantTown,
                AddressLine1 = TestData.MerchantAddressLine1
            };

        public static MerchantModel MerchantModel =>
            new MerchantModel
            {
                MerchantId = TestData.MerchantId,
                MerchantName = TestData.MerchantName,
                AvailableBalance = TestData.AvailableBalance,
                EstateId = TestData.EstateId,
                Balance = TestData.Balance,
                Addresses = new List<AddressModel>
                            {
                                new AddressModel
                                {
                                    AddressLine4 = TestData.MerchantAddressLine4,
                                    AddressLine1 = TestData.MerchantAddressLine1,
                                    AddressLine2 = TestData.MerchantAddressLine2,
                                    AddressLine3 = TestData.MerchantAddressLine3,
                                    Country = TestData.MerchantCountry,
                                    PostalCode = TestData.MerchantPostalCode,
                                    Region = TestData.MerchantRegion,
                                    Town = TestData.MerchantTown,
                                    AddressId = TestData.AddressId
                                }
                            },
                Contacts = new List<ContactModel>
                           {
                               new ContactModel
                               {
                                   ContactName = TestData.MerchantContactName,
                                   ContactPhoneNumber = TestData.MerchantContactPhoneNumber,
                                   ContactEmailAddress = TestData.MerchantContactEmailAddress,
                                   ContactId = TestData.ContactId
                               }
                           },
                Devices = new Dictionary<Guid, String>
                          {
                              {TestData.DeviceId, TestData.DeviceIdentifier}
                          },
                Operators = new List<MerchantOperatorModel>
                            {
                                new MerchantOperatorModel
                                {
                                    Name = TestData.OperatorName,
                                    MerchantNumber = TestData.MerchantNumber,
                                    OperatorId = TestData.OperatorId,
                                    TerminalNumber = TestData.TerminalNumber
                                }
                            }

            };

        public static MakeMerchantDepositViewModel MakeMerchantDepositViewModel =>
            new MakeMerchantDepositViewModel
            {
                MerchantName = TestData.MerchantName,
                Amount = TestData.DepositAmount,
                Reference = TestData.DepositReference,
                DepositDate = TestData.DepositDate,
                MerchantId = TestData.MerchantId.ToString()
            };

        public static CreateOperatorViewModel CreateOperatorViewModel =>
            new CreateOperatorViewModel
            {
                RequireCustomMerchantNumber = TestData.RequireCustomMerchantNumber,
                RequireCustomTerminalNumber = TestData.RequireCustomTerminalNumber,
                OperatorName = TestData.OperatorName
            };

        public static EstateOperatorModel EstateOperatorModel =>
            new EstateOperatorModel
            {
                RequireCustomTerminalNumber = TestData.RequireCustomTerminalNumber,
                OperatorId = TestData.OperatorId,
                RequireCustomMerchantNumber = TestData.RequireCustomMerchantNumber,
                Name = TestData.OperatorName
            };

        public static CreateOperatorModel CreateOperatorModel =>
            new CreateOperatorModel
            {
                RequireCustomTerminalNumber = TestData.RequireCustomTerminalNumber,
                RequireCustomMerchantNumber = TestData.RequireCustomMerchantNumber,
                OperatorName = TestData.OperatorName
            };

        public static CreateOperatorResponse CreateOperatorResponse =>
            new CreateOperatorResponse
            {
                OperatorId = TestData.OperatorId,
                EstateId = TestData.EstateId
            };

        public static String DepositAmount = "1000";

        public static Decimal DepositAmountDecimal = 1000;

        public static String DepositReference = "Test Deposit";

        public static String DepositDate = "24/06/2020";

        public static DateTime DepositDateTime = new DateTime(2021, 2, 27);

        public static Guid DepositId = Guid.Parse("895EE043-26E3-49E1-8255-2A25AAF070B7");

        public static MakeMerchantDepositModel MakeMerchantDepositModel =>
            new MakeMerchantDepositModel
            {
                MerchantId = TestData.MerchantId,
                DepositDateTime = DateTime.ParseExact(TestData.DepositDate, "dd/MM/yyyy", null),
                Reference = TestData.DepositReference,
                Amount = Decimal.Parse(TestData.DepositAmount)
            };

        public static CreateMerchantModel CreateMerchantModel =>
            new CreateMerchantModel
            {
                MerchantName = TestData.MerchantName,
                Contact = new ContactModel
                          {
                              ContactName = TestData.MerchantContactName,
                              ContactPhoneNumber = TestData.MerchantContactPhoneNumber,
                              ContactEmailAddress = TestData.MerchantContactEmailAddress
                          },
                Address = new AddressModel
                          {
                              AddressLine4 = TestData.MerchantAddressLine4,
                              AddressLine1 = TestData.MerchantAddressLine1,
                              AddressLine2 = TestData.MerchantAddressLine2,
                              AddressLine3 = TestData.MerchantAddressLine3,
                              Country = TestData.MerchantCountry,
                              PostalCode = TestData.MerchantPostalCode,
                              Region = TestData.MerchantRegion,
                              Town = TestData.MerchantTown
                          }
            };

        public static MakeMerchantDepositResponse MakeMerchantDepositResponse =>
            new MakeMerchantDepositResponse
            {
                MerchantId = TestData.MerchantId,
                EstateId = TestData.EstateId,
                DepositId = TestData.DepositId
            };

        public static CreateMerchantResponse CreateMerchantResponse =>
            new CreateMerchantResponse
            {
                MerchantId = TestData.MerchantId,
                EstateId = TestData.EstateId,
                ContactId = TestData.ContactId,
                AddressId = TestData.AddressId
            };


        public static MerchantResponse MerchantResponse =>
            new MerchantResponse
            {
                MerchantId = TestData.MerchantId,
                Contacts = new List<ContactResponse>
                           {
                               new ContactResponse
                               {
                                   ContactName = TestData.MerchantContactName,
                                   ContactPhoneNumber = TestData.MerchantContactPhoneNumber,
                                   ContactId = TestData.ContactId,
                                   ContactEmailAddress = TestData.MerchantContactEmailAddress
                               }
                           },
                EstateId = TestData.EstateId,
                MerchantName = TestData.MerchantName,
                AvailableBalance = TestData.AvailableBalance,
                Devices = new Dictionary<Guid, String>
                          {
                              {TestData.DeviceId, TestData.DeviceIdentifier}
                          },
                Operators = new List<MerchantOperatorResponse>
                            {
                                new MerchantOperatorResponse
                                {
                                    OperatorId = TestData.OperatorId,
                                    MerchantNumber = TestData.MerchantNumber,
                                    TerminalNumber = TestData.TerminalNumber,
                                    Name = TestData.MerchantName
                                }
                            },
                Balance = TestData.Balance,
                Addresses = new List<AddressResponse>
                            {
                                new AddressResponse
                                {
                                    AddressLine4 = TestData.MerchantAddressLine4,
                                    AddressLine1 = TestData.MerchantAddressLine1,
                                    AddressLine2 = TestData.MerchantAddressLine2,
                                    AddressLine3 = TestData.MerchantAddressLine3,
                                    Country = TestData.MerchantCountry,
                                    PostalCode = TestData.MerchantPostalCode,
                                    Region = TestData.MerchantRegion,
                                    Town = TestData.MerchantTown,
                                    AddressId = TestData.AddressId
                                }
                            }
            };


        public static EstateResponse EstateResponse =>
            new EstateResponse
            {
                EstateName = TestData.EstateName,
                Operators = new List<EstateOperatorResponse>
                            {
                                new EstateOperatorResponse
                                {
                                    OperatorId = TestData.OperatorId,
                                    Name = TestData.OperatorName,
                                    RequireCustomMerchantNumber = TestData.RequireCustomMerchantNumber,
                                    RequireCustomTerminalNumber = TestData.RequireCustomTerminalNumber
                                }
                            },
                EstateId = TestData.EstateId,
                SecurityUsers = new List<SecurityUserResponse>
                                {
                                    new SecurityUserResponse
                                    {
                                        EmailAddress = TestData.EmailAddress,
                                        SecurityUserId = TestData.SecurityUserId

                                    }
                                }
            };

        public static String ContractDescription = "Test Contract 1";

        public static String ContractProductDescription = "Test Product 1";

        public static String ContractProductName = "Product 1";

        public static String ContractProductDisplayText = "Product1";

        public static Decimal ContractProductValue = 100.00m;

        public static Guid ContractProductId = Guid.Parse("6CAE4549-6334-4CC4-82CD-A915BDE559D3");

        public static String TransactionFeeDescription = "Test Fee 1";

        public static Decimal TransactionFeeValue = 1.00m;

        public static Decimal? ContractProductValueNull = null;

        public static Guid TransactionFeeId = Guid.Parse("982CB7C9-2383-4361-BA44-FF7BAE3B03E6");

        public static ContractResponse ContractResponse =>
            new ContractResponse
            {
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                Products = new List<ContractProduct>
                           {
                               new ContractProduct
                               {
                                   Value = TestData.ContractProductValue,
                                   TransactionFees = new List<ContractProductTransactionFee>
                                                     {
                                                         new ContractProductTransactionFee
                                                         {
                                                             Description = TestData.TransactionFeeDescription,
                                                             Value = TestData.TransactionFeeValue,
                                                             FeeType = DTOFeeType.Merchant,
                                                             TransactionFeeId = TestData.TransactionFeeId,
                                                             CalculationType = DTOCalculationType.Fixed
                                                         }
                                                     },
                                   ProductId = TestData.ContractProductId,
                                   Name = TestData.ContractProductName,
                                   DisplayText = TestData.ContractProductDisplayText
                               }
                           },
                Description = TestData.ContractDescription,
                ContractId = TestData.ContactId
            };

        public static ContractResponse ContractResponseProductWithNullFees =>
            new ContractResponse
            {
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                Products = new List<ContractProduct>
                           {
                               new ContractProduct
                               {
                                   Value = TestData.ContractProductValue,
                                   TransactionFees = null,
                                   ProductId = TestData.ContractProductId,
                                   Name = TestData.ContractProductName,
                                   DisplayText = TestData.ContractProductDisplayText
                               }
                           },
                Description = TestData.ContractDescription,
                ContractId = TestData.ContactId
            };

        public static ContractResponse ContractResponseProductWithEmptyFees =>
            new ContractResponse
            {
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                Products = new List<ContractProduct>
                           {
                               new ContractProduct
                               {
                                   Value = TestData.ContractProductValue,
                                   TransactionFees = new List<ContractProductTransactionFee>(),
                                   ProductId = TestData.ContractProductId,
                                   Name = TestData.ContractProductName,
                                   DisplayText = TestData.ContractProductDisplayText
                               }
                           },
                Description = TestData.ContractDescription,
                ContractId = TestData.ContactId
            };

        public static ContractResponse ContractResponseNullProducts =>
            new ContractResponse
            {
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                Products = null,
                Description = TestData.ContractDescription,
                ContractId = TestData.ContactId
            };

        public static ContractResponse ContractResponseEmptyProducts =>
            new ContractResponse
            {
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                Products = new List<ContractProduct>
                           {

                           },
                Description = TestData.ContractDescription,
                ContractId = TestData.ContactId
            };

        public static CreateContractResponse CreateContractResponse =>
            new CreateContractResponse
            {
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                ContractId = TestData.ContactId
            };

        public static CreateContractModel CreateContractModel =>
            new CreateContractModel
            {
                OperatorId = TestData.OperatorId,
                Description = TestData.ContractDescription
            };

        public static CreateContractViewModel CreateContractViewModel =>
            new CreateContractViewModel
            {
                OperatorId = TestData.OperatorId,
                ContractDescription = TestData.ContractDescription
            };

        public static FeeType ModelTransactionFeeType = FeeType.Merchant;

        public static CalculationType ModelTransactionFeeCalculationType = CalculationType.Fixed;

        public static DTOFeeType DTOTransactionFeeType = DTOFeeType.Merchant;

        public static DTOCalculationType DTOTransactionFeeCalculationType = DTOCalculationType.Fixed;

        public static List<MerchantBalanceHistory> MerchantBalanceHistoryList => new List<MerchantBalanceHistory>
                                                                                {
                                                                                    new MerchantBalanceHistory
                                                                                    {
                                                                                        Balance = TestData.Balance,
                                                                                        MerchantId = TestData.MerchantId,
                                                                                        Reference = TestData.DepositReference,
                                                                                        ChangeAmount = TestData.DepositAmountDecimal,
                                                                                        EntryDateTime = TestData.DepositDateTime,
                                                                                        EntryType = "C",
                                                                                        EstateId = TestData.EstateId,
                                                                                        EventId = TestData.EventId,
                                                                                        In = TestData.DepositAmountDecimal,
                                                                                        Out = null,
                                                                                        TransactionId = TestData.TransactionId
                                                                                    }
                                                                                };

        public static Guid EventId = Guid.Parse("2EF14DC4-C234-4175-BCA1-D5AB7F58943E");

        public static Guid TransactionId = Guid.Parse("3FB06E57-B307-4C30-8D5A-EE20C916A81D");

        public static Guid FileImportLogId = Guid.Parse("45F5C142-D034-4AE3-B27B-B733FE200028");

        public static Int32 FileCount = 1;

        public  static DateTime ImportLogDateTime = new DateTime(2021,7,22,9,9,25);

        public static String FilePath = "/home/txnproc/uploadedfile;";

        public static Guid FileProfileId = Guid.Parse("711EFF6A-05F1-4B80-A60C-2728E9C9081C");

        public static DateTime FileUploadedDateTime = new DateTime(2021, 7, 22, 10, 9, 25);

        public static String OriginalFileName = "OriginalFileName.txt";

        public static Guid FileId = Guid.Parse("2FC540D7-8606-4F3C-A5F0-B179015D7928");

        public static String FileLocation = "/home/txnproc/file.txt";

        public static Boolean ProcessingCompleted = true;

        public static Int32 TotalLines = 7;

        public static FileImportLogList FileImportLogList =>
        new FileImportLogList
        {
            FileImportLogs = new List<FileImportLog>
                             {
                                 new FileImportLog
                                 {
                                     FileCount = TestData.FileCount,
                                     FileImportLogId = TestData.FileImportLogId,
                                     ImportLogDate = TestData.ImportLogDateTime.Date,
                                     ImportLogDateTime = TestData.ImportLogDateTime,
                                     ImportLogTime = TestData.ImportLogDateTime.TimeOfDay,
                                     Files = new List<FileImportLogFile>
                                             {
                                                 new FileImportLogFile
                                                 {
                                                     FileImportLogId = TestData.FileImportLogId,
                                                     FileId = TestData.FileId,
                                                     FilePath = TestData.FilePath,
                                                     FileProfileId = TestData.FileProfileId,
                                                     FileUploadedDateTime = TestData.FileUploadedDateTime,
                                                     MerchantId = TestData.MerchantId,
                                                     OriginalFileName = TestData.OriginalFileName,
                                                     UserId = TestData.SecurityUserId
                                                 }
                                             }
                                 }
                             }
        };

        public static List<MerchantBalanceHistoryResponse> MerchantBalanceHistoryResponseList =>
            new List<MerchantBalanceHistoryResponse>
            {
                new MerchantBalanceHistoryResponse
                {
                    Balance = TestData.Balance,
                    MerchantId = TestData.MerchantId,
                    Reference = TestData.DepositReference,
                    ChangeAmount = TestData.DepositAmountDecimal,
                    EntryDateTime = TestData.DepositDateTime,
                    EntryType = "C",
                    EstateId = TestData.EstateId,
                    EventId = TestData.EventId,
                    In = TestData.DepositAmountDecimal,
                    Out = null,
                    TransactionId = TestData.TransactionId
                }
            };

        public static TransactionsByOperatorModel TransactionsByOperatorModel =>
            new TransactionsByOperatorModel
            {
                TransactionOperatorModels = new List<TransactionOperatorModel>
                                            {
                                                new TransactionOperatorModel
                                                {
                                                    CurrencyCode = String.Empty,
                                                    NumberOfTransactions = 10,
                                                    ValueOfTransactions = 1000,
                                                    OperatorName = TestData.OperatorName
                                                }
                                            }
            };

        public static TransactionsByOperatorResponse TransactionsByOperatorResponse =>
            new TransactionsByOperatorResponse
            {
                TransactionOperatorResponses = new List<TransactionOperatorResponse>
                                               {
                                                   new TransactionOperatorResponse
                                                   {
                                                       CurrencyCode = String.Empty,
                                                       NumberOfTransactions = 10,
                                                       ValueOfTransactions = 1000,
                                                       OperatorName = TestData.OperatorName
                                                   }
                                               }
            };

        public static TransactionsByMerchantModel TransactionsByMerchantModel => new TransactionsByMerchantModel
                                                                                 {
                                                                                     TransactionMerchantModels = new List<TransactionMerchantModel>
                                                                                                                 {
                                                                                                                     new TransactionMerchantModel
                                                                                                                     {
                                                                                                                         CurrencyCode = String.Empty,
                                                                                                                         MerchantId = TestData.MerchantId,
                                                                                                                         MerchantName = TestData.MerchantName,
                                                                                                                         NumberOfTransactions = 10,
                                                                                                                         ValueOfTransactions = 1000
                                                                                                                     }
                                                                                                                 }
                                                                                 };

        public static TransactionsByMerchantResponse TransactionsByMerchantResponse =>
            new TransactionsByMerchantResponse
            {   
                TransactionMerchantResponses = new List<TransactionMerchantResponse>
                                               {
                                                   new TransactionMerchantResponse
                                                   {
                                                       CurrencyCode = String.Empty,
                                                       MerchantId = TestData.MerchantId,
                                                       MerchantName = TestData.MerchantName,
                                                       NumberOfTransactions = 10,
                                                       ValueOfTransactions = 1000
                                                   }
                                               }
            };

        public static AddTransactionFeeForProductToContractResponse AddTransactionFeeForProductToContractResponse =>
            new AddTransactionFeeForProductToContractResponse
            {
                EstateId = TestData.EstateId,
                ProductId = TestData.ContractProductId,
                TransactionFeeId = TestData.TransactionFeeId,
                ContractId = TestData.ContactId
            };

        public static AddTransactionFeeToContractProductModel AddTransactionFeeToContractProductModel =>
            new AddTransactionFeeToContractProductModel
            {
                Value = TestData.TransactionFeeValue,
                Description = TestData.TransactionFeeDescription,
                FeeType = TestData.ModelTransactionFeeType,
                CalculationType = TestData.ModelTransactionFeeCalculationType
            };

        public static CreateContractProductTransactionFeeViewModel CreateContractProductTransactionFeeViewModel =>
            new CreateContractProductTransactionFeeViewModel
                   {
                       FeeType = 1,
                       Value = TestData.TransactionFeeValue,
                       FeeDescription = TestData.TransactionFeeDescription,
                       ContractProductId = TestData.ContractProductId,
                       CalculationType = 1,
                       ContractId = TestData.ContactId
                   };
            

        public static CreateContractProductViewModel CreateContractProductViewModelWithValue =>
            new CreateContractProductViewModel
            {
                Value = TestData.ContractProductValue,
                ProductName = TestData.ContractProductName,
                DisplayText = TestData.ContractProductDisplayText,
                TransactionFees = null,
                ContractId = TestData.ContactId,
                IsVariable = false
            };

        public static CreateContractProductViewModel CreateContractProductViewModelWithNullValue =>
            new CreateContractProductViewModel
            {
                Value = null,
                ProductName = TestData.ContractProductName,
                DisplayText = TestData.ContractProductDisplayText,
                TransactionFees = null,
                ContractId = TestData.ContactId,
                IsVariable = true
            };

        public static AddProductToContractResponse AddProductToContractResponse =>
            new AddProductToContractResponse
            {
                EstateId = TestData.EstateId,
                ProductId = TestData.ContractProductId,
                ContractId = TestData.ContactId
            };

        public static AddProductToContractModel AddProductToContractModelWithValue =>
            new AddProductToContractModel
            {
                Value = TestData.ContractProductValue,
                DisplayText = TestData.ContractProductDisplayText,
                ProductName = TestData.ContractProductName
            };

        public static AddProductToContractModel AddProductToContractModelWithNullValue =>
            new AddProductToContractModel
            {
                Value = TestData.ContractProductValueNull,
                DisplayText = TestData.ContractProductDisplayText,
                ProductName = TestData.ContractProductName
            };

        public static ContractProductTransactionFeeModel ContractProductTransactionFeeModel =>
            new ContractProductTransactionFeeModel
            {
                Description = TestData.TransactionFeeDescription,
                Value = TestData.TransactionFeeValue.ToString(),
                FeeType = TestData.ModelTransactionFeeType.ToString(),
                CalculationType = TestData.ModelTransactionFeeCalculationType.ToString(),
                TransactionFeeId = TestData.TransactionFeeId
            };

        public static ContractProductModel ContractProductModel =>
            new ContractProductModel
            {
                EstateId = TestData.EstateId,
                Description = TestData.ContractProductDescription,
                Value = TestData.ContractProductValue,
                ContractId = TestData.ContactId,
                ContractProductId = TestData.ContractProductId,
                ProductName = TestData.ContractProductName,
                DisplayText = TestData.ContractProductDisplayText,
                NumberOfTransactionFees = 1,
                ContractProductTransactionFees = new List<ContractProductTransactionFeeModel>
                                                 {
                                                     TestData.ContractProductTransactionFeeModel
                                                 }
            };

        public static ContractProductModel ContractProductModelNullValue =>
            new ContractProductModel
            {
                EstateId = TestData.EstateId,
                Description = TestData.ContractProductDescription,
                Value = TestData.ContractProductValueNull,
                ContractId = TestData.ContactId,
                ContractProductId = TestData.ContractProductId,
                ProductName = TestData.ContractProductName,
                DisplayText = TestData.ContractProductDisplayText,
                NumberOfTransactionFees = 1,
                ContractProductTransactionFees = new List<ContractProductTransactionFeeModel>
                                                 {
                                                     TestData.ContractProductTransactionFeeModel
                                                 }
            };

        public static ContractProductModel ContractProductModelNullFees =>
            new ContractProductModel
            {
                EstateId = TestData.EstateId,
                Description = TestData.ContractProductDescription,
                Value = TestData.ContractProductValue,
                ContractId = TestData.ContactId,
                ContractProductId = TestData.ContractProductId,
                ProductName = TestData.ContractProductName,
                DisplayText = TestData.ContractProductDisplayText,
                NumberOfTransactionFees = 0,
                ContractProductTransactionFees = null
            };

        public static ContractProductModel ContractProductModelEmptyFeeList =>
            new ContractProductModel
            {
                EstateId = TestData.EstateId,
                Description = TestData.ContractProductDescription,
                Value = TestData.ContractProductValue,
                ContractId = TestData.ContactId,
                ContractProductId = TestData.ContractProductId,
                ProductName = TestData.ContractProductName,
                DisplayText = TestData.ContractProductDisplayText,
                NumberOfTransactionFees = 0,
                ContractProductTransactionFees = new List<ContractProductTransactionFeeModel>()
            };

        public static ContractModel ContractModel =>
            new ContractModel
            {
                ContractId = TestData.ContactId,
                Description = TestData.ContractDescription,
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                ContractProducts = new List<ContractProductModel>
                                   {
                                       TestData.ContractProductModel
                                   },
                NumberOfProducts = 1
            };

        public static ContractModel ContractModelWithNullValueProduct =>
            new ContractModel
            {
                ContractId = TestData.ContactId,
                Description = TestData.ContractDescription,
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                ContractProducts = new List<ContractProductModel>
                                   {
                                       TestData.ContractProductModelNullValue
                                   },
                NumberOfProducts = 1
            };

        public static ContractModel ContractModelNullProducts =>
            new ContractModel
            {
                ContractId = TestData.ContactId,
                Description = TestData.ContractDescription,
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                ContractProducts = null,
                NumberOfProducts = 0
            };

        public static ContractModel ContractModelEmptyProducts =>
            new ContractModel
            {
                ContractId = TestData.ContactId,
                Description = TestData.ContractDescription,
                EstateId = TestData.EstateId,
                OperatorId = TestData.OperatorId,
                OperatorName = TestData.OperatorName,
                ContractProducts = new List<ContractProductModel>(),
                NumberOfProducts = 0
            };

        public static TransactionsByDayResponse TransactionsByDayResponseWithSingleDate =>
            new TransactionsByDayResponse
            {
                TransactionDayResponses = new List<TransactionDayResponse>
                                          {
                                              new TransactionDayResponse
                                              {
                                                  CurrencyCode = "KES",
                                                  Date = new DateTime(2020,10,1),
                                                  NumberOfTransactions = 10,
                                                  ValueOfTransactions = 1000
                                              }
                                          }
            };

        public static TransactionsByDayResponse TransactionsByDayResponse =>
            new TransactionsByDayResponse
            {
                TransactionDayResponses = new List<TransactionDayResponse>
                                          {
                                              new TransactionDayResponse
                                              {
                                                  CurrencyCode = "KES",
                                                  Date = new DateTime(2020,10,1),
                                                  NumberOfTransactions = 10,
                                                  ValueOfTransactions = 1000
                                              },
                                              new TransactionDayResponse
                                              {
                                                  CurrencyCode = "KES",
                                                  Date = new DateTime(2020,10,2),
                                                  NumberOfTransactions = 20,
                                                  ValueOfTransactions = 2000
                                              }
                                          }
            };

        public static TransactionsByDateModel TransactionsByDateModel =>
            new TransactionsByDateModel
            {
                TransactionDateModels = new List<TransactionDateModel>
                                          {
                                              new TransactionDateModel
                                              {
                                                  CurrencyCode = "KES",
                                                  Date = new DateTime(2020,10,1),
                                                  NumberOfTransactions = 10,
                                                  ValueOfTransactions = 1000
                                              },
                                              new TransactionDateModel
                                              {
                                                  CurrencyCode = "KES",
                                                  Date = new DateTime(2020,10,2),
                                                  NumberOfTransactions = 20,
                                                  ValueOfTransactions = 2000
                                              }
                                          }
            };

        public static TransactionsByWeekResponse TransactionsByWeekResponse =>
            new TransactionsByWeekResponse
            {
                TransactionWeekResponses = new List<TransactionWeekResponse>
                                          {
                                              new TransactionWeekResponse
                                              {
                                                  CurrencyCode = "KES",
                                                  WeekNumber = 1,
                                                  Year = 2020,
                                                  NumberOfTransactions = 10,
                                                  ValueOfTransactions = 1000
                                              },
                                              new TransactionWeekResponse
                                              {
                                                  CurrencyCode = "KES",
                                                  WeekNumber = 2,
                                                  Year = 2020,
                                                  NumberOfTransactions = 20,
                                                  ValueOfTransactions = 2000
                                              }
                                          }
            };

        public static TransactionsByWeekModel TransactionsByWeekModel =>
            new TransactionsByWeekModel
            {
                TransactionWeekModels = new List<TransactionWeekModel>
                                           {
                                               new TransactionWeekModel
                                               {
                                                   CurrencyCode = "KES",
                                                   WeekNumber = 1,
                                                   Year = 2020,
                                                   NumberOfTransactions = 10,
                                                   ValueOfTransactions = 1000
                                               },
                                               new TransactionWeekModel
                                               {
                                                   CurrencyCode = "KES",
                                                   WeekNumber = 2,
                                                   Year = 2020,
                                                   NumberOfTransactions = 20,
                                                   ValueOfTransactions = 2000
                                               }
                                           }
            };

        public static TransactionsByMonthResponse TransactionsByMonthResponse =>
            new TransactionsByMonthResponse
            {
                TransactionMonthResponses = new List<TransactionMonthResponse>
                                           {
                                               new TransactionMonthResponse
                                               {
                                                   CurrencyCode = "KES",
                                                   MonthNumber = 1,
                                                   Year = 2020,
                                                   NumberOfTransactions = 10,
                                                   ValueOfTransactions = 1000
                                               },
                                               new TransactionMonthResponse
                                               {
                                                   CurrencyCode = "KES",
                                                   MonthNumber = 2,
                                                   Year = 2020,
                                                   NumberOfTransactions = 20,
                                                   ValueOfTransactions = 2000
                                               }
                                           }
            };

        public static TransactionsByMonthModel TransactionsByMonthModel =>
            new TransactionsByMonthModel
            {
                TransactionMonthModels = new List<TransactionMonthModel>
                                            {
                                                new TransactionMonthModel
                                                {
                                                    CurrencyCode = "KES",
                                                    MonthNumber = 1,
                                                    Year = 2020,
                                                    NumberOfTransactions = 10,
                                                    ValueOfTransactions = 1000
                                                },
                                                new TransactionMonthModel
                                                {
                                                    CurrencyCode = "KES",
                                                    MonthNumber = 2,
                                                    Year = 2020,
                                                    NumberOfTransactions = 20,
                                                    ValueOfTransactions = 2000
                                                }
                                            }
            };

        public static TransactionForPeriodModel TransactionForPeriodModel =>
            new TransactionForPeriodModel
            {
                NumberOfTransactions = 20,
                CurrencyCode = "KES",
                ValueOfTransactions = 2000
            };

        public static FileImportLogModel FileImportLogModel =>
            new FileImportLogModel
            {
                FileImportLogId = TestData.FileImportLogId,
                FileCount = TestData.FileCount,
                ImportLogDate = TestData.ImportLogDateTime.Date,
                ImportLogDateTime = TestData.ImportLogDateTime,
                ImportLogTime = TestData.ImportLogDateTime.TimeOfDay,
                Files = new List<FileImportLogFileModel>
                        {
                            new FileImportLogFileModel
                            {
                                FileImportLogId = TestData.FileImportLogId,
                                FileId = TestData.FileId,
                                FilePath = TestData.FilePath,
                                FileProfileId = TestData.FileProfileId,
                                FileUploadedDateTime = TestData.FileUploadedDateTime,
                                MerchantId = TestData.MerchantId,
                                OriginalFileName = TestData.OriginalFileName,
                                UserId = TestData.SecurityUserId
                            }
                        }
            };

        public static Int32 IgnoredLines = 2;
        public static Int32 NotProcessedLines = 2;
        public static Int32 RejectedLines = 1;
        public static Int32 SuccessfullyProcessedLines = 1;
        public static Int32 FailedLines = 1;

        public static FileDetails FileDetails =>
            new FileDetails
            {
                EstateId = TestData.EstateId,
                FileId = TestData.FileId,
                FileImportLogId = TestData.FileImportLogId,
                FileLocation = TestData.FileLocation,
                FileProfileId = TestData.FileProfileId,
                MerchantId = TestData.MerchantId,
                ProcessingCompleted = TestData.ProcessingCompleted,
                ProcessingSummary = new FileProcessingSummary
                                    {
                                        TotalLines = TestData.TotalLines,
                                        IgnoredLines = TestData.IgnoredLines,
                                        NotProcessedLines = TestData.NotProcessedLines,
                                        RejectedLines = TestData.RejectedLines,
                                        SuccessfullyProcessedLines = TestData.SuccessfullyProcessedLines,
                                        FailedLines = TestData.FailedLines
                                    },
                UserId = TestData.SecurityUserId,
                FileLines = new List<FileLine>
                            {
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.Ignored,
                                    LineNumber = 1,
                                    LineData = "H",
                                },
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.Successful,
                                    LineNumber = 2,
                                    LineData = "D",
                                    TransactionId = Guid.Parse("F327C705-28D7-4F11-AC93-75A0D614D1FB")
                                },
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.Rejected,
                                    LineNumber = 3,
                                    LineData = "D",
                                    RejectionReason = "Invalid Format"
                                },
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.Failed,
                                    LineNumber = 4,
                                    LineData = "D",
                                    TransactionId = Guid.Parse("0C0F278B-B9C9-4767-A367-AFD6B9D95BEA")
                                },
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.NotProcessed,
                                    LineNumber = 5,
                                    LineData = "D",
                                },
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.Unknown,
                                    LineNumber = 6,
                                    LineData = "D",
                                },
                                new FileLine
                                {
                                    ProcessingResult = FileLineProcessingResult.Ignored,
                                    LineNumber = 7,
                                    LineData = "T",
                                }
                            }
            };

        public static FileDetailsModel FileDetailsModel =>
            new FileDetailsModel
            {
                EstateId = TestData.EstateId,
                FileId = TestData.FileId,
                FileImportLogId = TestData.FileImportLogId,
                FileLocation = TestData.FileLocation,
                FileProfileId = TestData.FileProfileId,
                MerchantId = TestData.MerchantId,
                ProcessingCompleted = TestData.ProcessingCompleted,
                ProcessingSummary = new FileProcessingSummaryModel
                {
                    TotalLines = TestData.TotalLines,
                    IgnoredLines = TestData.IgnoredLines,
                    NotProcessedLines = TestData.NotProcessedLines,
                    RejectedLines = TestData.RejectedLines,
                    SuccessfullyProcessedLines = TestData.SuccessfullyProcessedLines,
                    FailedLines = TestData.FailedLines
                },
                UserId = TestData.SecurityUserId,
                FileLines = new List<FileLineModel>
                            {
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.Ignored,
                                    LineNumber = 1,
                                    LineData = "H",
                                },
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.Successful,
                                    LineNumber = 2,
                                    LineData = "D",
                                    TransactionId = Guid.Parse("F327C705-28D7-4F11-AC93-75A0D614D1FB")
                                },
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.Rejected,
                                    LineNumber = 3,
                                    LineData = "D",
                                    RejectionReason = "Invalid Format"
                                },
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.Failed,
                                    LineNumber = 4,
                                    LineData = "D",
                                    TransactionId = Guid.Parse("0C0F278B-B9C9-4767-A367-AFD6B9D95BEA")
                                },
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.NotProcessed,
                                    LineNumber = 5,
                                    LineData = "D",
                                },
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.Unknown,
                                    LineNumber = 6,
                                    LineData = "D",
                                },
                                new FileLineModel
                                {
                                    ProcessingResult = BusinessLogic.Models.FileLineProcessingResult.Ignored,
                                    LineNumber = 7,
                                    LineData = "T",
                                }
                            }
            };
    }
}