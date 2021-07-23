namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using EstateReporting.DataTransferObjects;
    using FileProcessor.DataTransferObjects.Responses;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.EntityFrameworkCore.Internal;
    using Models;
    using SortDirection = EstateReporting.DataTransferObjects.SortDirection;
    using SortField = EstateReporting.DataTransferObjects.SortField;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateAdministrationUI.BusinessLogic.Factories.IModelFactory" />
    public class ModelFactory : IModelFactory
    {
        #region Methods

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public EstateModel ConvertFrom(EstateResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            EstateModel model = new EstateModel
                                {
                                    EstateId = source.EstateId,
                                    EstateName = source.EstateName,
                                    Operators = this.ConvertOperators(source.Operators),
                                    SecurityUsers = this.ConvertSecurityUsers(source.SecurityUsers)
                                };
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public CreateOperatorRequest ConvertFrom(CreateOperatorModel source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public CreateContractRequest ConvertFrom(CreateContractModel source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public CreateOperatorResponseModel ConvertFrom(CreateOperatorResponse source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public CreateContractResponseModel ConvertFrom(CreateContractResponse source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public AddProductToContractRequest ConvertFrom(AddProductToContractModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AddProductToContractRequest addProductToContractRequest = new AddProductToContractRequest
                                                                      {
                                                                          Value = source.Value,
                                                                          ProductName = source.ProductName,
                                                                          DisplayText = source.DisplayText
                                                                      };

            return addProductToContractRequest;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public AddProductToContractResponseModel ConvertFrom(AddProductToContractResponse source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public AddTransactionFeeForProductToContractRequest ConvertFrom(AddTransactionFeeToContractProductModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            EstateManagement.DataTransferObjects.CalculationType calculationType = Enum.Parse<EstateManagement.DataTransferObjects.CalculationType>(source.CalculationType.ToString(), true);
            EstateManagement.DataTransferObjects.FeeType feeType = Enum.Parse<EstateManagement.DataTransferObjects.FeeType>(source.FeeType.ToString(), true);
            AddTransactionFeeForProductToContractRequest addTransactionFeeForProductToContractRequest = new AddTransactionFeeForProductToContractRequest
                                                                                                        {
                                                                                                            Value = source.Value,
                                                                                                            Description = source.Description,
                                                                                                            FeeType = feeType,
                                                                                                            CalculationType = calculationType
                                                                                                        };

            return addTransactionFeeForProductToContractRequest;


        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public AddTransactionFeeToContractProductResponseModel ConvertFrom(AddTransactionFeeForProductToContractResponse source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransactionForPeriodModel ConvertToPeriodModel(TransactionsByDayResponse source)
        {
            if (source == null)
            {
                return null;
            }

            TransactionForPeriodModel model = new TransactionForPeriodModel();
            
                model.NumberOfTransactions = source.TransactionDayResponses.Sum(x => x.NumberOfTransactions);
                model.ValueOfTransactions = source.TransactionDayResponses.Sum(x => x.ValueOfTransactions);
                model.CurrencyCode = source.TransactionDayResponses.Select(x => x.CurrencyCode).First();

                return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransactionsByDateModel ConvertFrom(TransactionsByDayResponse source)
        {
            if (source == null || source.TransactionDayResponses == null || source.TransactionDayResponses.Any() == false)
            {
                return null;
            }

            TransactionsByDateModel model = new TransactionsByDateModel
                                            {
                                                TransactionDateModels = new List<TransactionDateModel>()
                                            };

            source.TransactionDayResponses.ForEach(t => model.TransactionDateModels.Add(new TransactionDateModel
                                                                                        {
                                                                                            NumberOfTransactions = t.NumberOfTransactions,
                                                                                            ValueOfTransactions = t.ValueOfTransactions,
                                                                                            CurrencyCode = t.CurrencyCode,
                                                                                            Date = t.Date
                                                                                        }));
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransactionsByWeekModel ConvertFrom(TransactionsByWeekResponse source)
        {
            if (source == null || source.TransactionWeekResponses == null || source.TransactionWeekResponses.Any() == false)
            {
                return null;
            }

            TransactionsByWeekModel model = new TransactionsByWeekModel
            {
                                                TransactionWeekModels = new List<TransactionWeekModel>()
                                            };

            source.TransactionWeekResponses.ForEach(t => model.TransactionWeekModels.Add(new TransactionWeekModel
                                                                                        {
                                                                                            NumberOfTransactions = t.NumberOfTransactions,
                                                                                            ValueOfTransactions = t.ValueOfTransactions,
                                                                                            CurrencyCode = t.CurrencyCode,
                                                                                            WeekNumber = t.WeekNumber,
                                                                                            Year = t.Year
                                                                                        }));
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransactionsByMonthModel ConvertFrom(TransactionsByMonthResponse source)
        {
            if (source == null || source.TransactionMonthResponses == null || source.TransactionMonthResponses.Any() == false)
            {
                return null;
            }

            TransactionsByMonthModel model = new TransactionsByMonthModel
            {
                                                TransactionMonthModels = new List<TransactionMonthModel>()
                                            };

            source.TransactionMonthResponses.ForEach(t => model.TransactionMonthModels.Add(new TransactionMonthModel
                                                                                          {
                                                                                              NumberOfTransactions = t.NumberOfTransactions,
                                                                                              ValueOfTransactions = t.ValueOfTransactions,
                                                                                              CurrencyCode = t.CurrencyCode,
                                                                                              MonthNumber = t.MonthNumber,
                                                                                              Year = t.Year
                                                                                          }));
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        public SortDirection ConvertFrom(Models.SortDirection sortDirection)
        {
            SortDirection result = SortDirection.Ascending;
            switch (sortDirection)
            {
                case Models.SortDirection.Ascending:
                    result = SortDirection.Ascending;
                    break;
                case Models.SortDirection.Descending:
                    result = SortDirection.Descending;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        /// <returns></returns>
        public SortField ConvertFrom(Models.SortField sortField)
        {
            SortField result = SortField.Value;
            switch (sortField)
            {
                case Models.SortField.Value:
                    result = SortField.Value;
                    break;
                case Models.SortField.Count:
                    result = SortField.Count;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransactionsByMerchantModel ConvertFrom(TransactionsByMerchantResponse source)
        {
            if (source == null || source.TransactionMerchantResponses == null || source.TransactionMerchantResponses.Any() == false)
            {
                return null;
            }

            TransactionsByMerchantModel model = new TransactionsByMerchantModel
            {
                                                    TransactionMerchantModels = new List<TransactionMerchantModel>()
                                                };

            source.TransactionMerchantResponses.ForEach(t => model.TransactionMerchantModels.Add(new TransactionMerchantModel
                                                                                                 {
                                                                                                     NumberOfTransactions = t.NumberOfTransactions,
                                                                                                     ValueOfTransactions = t.ValueOfTransactions,
                                                                                                     CurrencyCode = t.CurrencyCode,
                                                                                                     MerchantId = t.MerchantId,
                                                                                                     MerchantName = t.MerchantName
                                                                                                 }));
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TransactionsByOperatorModel ConvertFrom(TransactionsByOperatorResponse source)
        {
            if (source == null || source.TransactionOperatorResponses == null || source.TransactionOperatorResponses.Any() == false)
            {
                return null;
            }

            TransactionsByOperatorModel model = new TransactionsByOperatorModel
                                                {
                                                    TransactionOperatorModels = new List<TransactionOperatorModel>()
                                                };

            source.TransactionOperatorResponses.ForEach(t=> model.TransactionOperatorModels.Add(new TransactionOperatorModel
                                                                                                {
                                                                                                    CurrencyCode = t.CurrencyCode,
                                                                                                    NumberOfTransactions = t.NumberOfTransactions,
                                                                                                    ValueOfTransactions = t.ValueOfTransactions,
                                                                                                    OperatorName = t.OperatorName
                                                                                                }));
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public List<MerchantBalanceHistory> ConvertFrom(List<MerchantBalanceHistoryResponse> source)
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
                                             EntryDateTime = s.EntryDateTime,
                                             EntryType = s.EntryType,
                                             EstateId = s.EstateId,
                                             EventId = s.EventId,
                                             In = s.In,
                                             Out = s.Out,
                                             Reference = s.Reference,
                                             TransactionId = s.TransactionId
                                                         
                                         });
                               });

            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public List<MerchantModel> ConvertFrom(List<MerchantResponse> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<MerchantModel> models = new List<MerchantModel>();

            foreach (MerchantResponse merchantResponse in source)
            {
                MerchantModel merchantModel = this.ConvertFrom(merchantResponse);

                models.Add(merchantModel);
            }

            return models;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public List<ContractModel> ConvertFrom(List<ContractResponse> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<ContractModel> models = new List<ContractModel>();

            foreach (ContractResponse contractResponse in source)
            {
                ContractModel contractModel = this.ConvertFrom(contractResponse);

                models.Add(contractModel);
            }

            return models;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public ContractModel ConvertFrom(ContractResponse source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public MerchantModel ConvertFrom(MerchantResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            MerchantModel merchantModel = new MerchantModel
                                          {
                                              EstateId = source.EstateId,
                                              MerchantId = source.MerchantId,
                                              MerchantName = source.MerchantName,
                                              Balance = source.Balance,
                                              AvailableBalance = source.AvailableBalance
                                          };

            if (source.Addresses != null && source.Addresses.Any())
            {
                merchantModel.Addresses = new List<AddressModel>();
                source.Addresses.ForEach(a => merchantModel.Addresses.Add(new AddressModel
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

            if (source.Contacts != null && source.Contacts.Any())
            {
                merchantModel.Contacts = new List<ContactModel>();
                source.Contacts.ForEach(c => merchantModel.Contacts.Add(new ContactModel
                                                                        {
                                                                            ContactEmailAddress = c.ContactEmailAddress,
                                                                            ContactId = c.ContactId,
                                                                            ContactName = c.ContactName,
                                                                            ContactPhoneNumber = c.ContactPhoneNumber
                                                                        }));
            }

            if (source.Operators != null && source.Operators.Any())
            {
                merchantModel.Operators = new List<MerchantOperatorModel>();
                source.Operators.ForEach(o => merchantModel.Operators.Add(new MerchantOperatorModel
                                                                          {
                                                                              MerchantNumber = o.MerchantNumber,
                                                                              Name = o.Name,
                                                                              OperatorId = o.OperatorId,
                                                                              TerminalNumber = o.TerminalNumber
                                                                          }));
            }

            if (source.Devices != null && source.Devices.Any())
            {
                merchantModel.Devices = new Dictionary<Guid, String>();
                foreach (KeyValuePair<Guid, String> merchantResponseDevice in source.Devices)
                {
                    merchantModel.Devices.Add(merchantResponseDevice.Key, merchantResponseDevice.Value);
                }
            }

            return merchantModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public CreateMerchantResponseModel ConvertFrom(CreateMerchantResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new CreateMerchantResponseModel
                   {
                       AddressId = source.AddressId,
                       ContactId = source.ContactId,
                       MerchantId = source.MerchantId,
                       EstateId = source.EstateId
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public CreateMerchantRequest ConvertFrom(CreateMerchantModel source)
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
                                                   Name = source.MerchantName
                                               };

            return apiRequest;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public MakeMerchantDepositRequest ConvertFrom(MakeMerchantDepositModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            MakeMerchantDepositRequest apiRequest = new MakeMerchantDepositRequest
                                                    {
                                                        DepositDateTime = source.DepositDateTime,
                                                        Reference = source.Reference,
                                                        Amount = source.Amount,
                                                        Source = MerchantDepositSource.Manual // Hard code this currently
                                                    };

            return apiRequest;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public List<FileImportLogModel> ConvertFrom(FileImportLogList source)
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
                    models.Add(this.ConvertFrom(sourceFileImportLog));
                }
            }

            return models;
        }

        public FileImportLogModel ConvertFrom(FileImportLog source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public MakeMerchantDepositResponseModel ConvertFrom(MakeMerchantDepositResponse source)
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

        /// <summary>
        /// Converts the operators.
        /// </summary>
        /// <param name="estateResponseOperators">The estate response operators.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">estateResponseOperators</exception>
        private List<EstateOperatorModel> ConvertOperators(List<EstateOperatorResponse> estateResponseOperators)
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

        /// <summary>
        /// Converts the security users.
        /// </summary>
        /// <param name="estateResponseSecurityUsers">The estate response security users.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">estateResponseSecurityUsers</exception>
        private List<SecurityUserModel> ConvertSecurityUsers(List<SecurityUserResponse> estateResponseSecurityUsers)
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

        #endregion
    }
}