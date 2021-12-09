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
    using FileLineProcessingResult = Models.FileLineProcessingResult;
    using SettlementSchedule = EstateManagement.DataTransferObjects.SettlementSchedule;
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

        public DataByOperatorModel ConvertFrom(SettlementByOperatorResponse source)
        {
            if (source == null || source.SettlementOperatorResponses == null || source.SettlementOperatorResponses.Any() == false)
            {
                return null;
            }

            DataByOperatorModel model = new DataByOperatorModel
                                        {
                                            DataOperatorModels = new List<DataOperatorModel>()
                                        };

            source.SettlementOperatorResponses.ForEach(t => model.DataOperatorModels.Add(new DataOperatorModel
                                                                                          {
                                                                                              CurrencyCode = t.CurrencyCode,
                                                                                              Count = t.NumberOfTransactionsSettled,
                                                                                              Value = t.ValueOfSettlement,
                                                                                              OperatorName = t.OperatorName
                                                                                          }));
            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public MerchantBalanceModel ConvertFrom(MerchantBalanceResponse source)
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

        public AddMerchantDeviceRequest ConvertFrom(AddMerchantDeviceModel source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public AddMerchantDeviceResponseModel ConvertFrom(AddMerchantDeviceResponse source)
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
        public DataByDateModel ConvertFrom(TransactionsByDayResponse source)
        {
            if (source == null || source.TransactionDayResponses == null || source.TransactionDayResponses.Any() == false)
            {
                return null;
            }

            DataByDateModel model = new DataByDateModel
                                            {
                                                DataDateModels = new List<DataDateModel>()
                                            };

            source.TransactionDayResponses.ForEach(t => model.DataDateModels.Add(new DataDateModel
                                                                                        {
                                                                                            Count = t.NumberOfTransactions,
                                                                                            Value = t.ValueOfTransactions,
                                                                                            CurrencyCode = t.CurrencyCode,
                                                                                            Date = t.Date
                                                                                        }));
            return model;
        }

        public DataByDateModel ConvertFrom(SettlementByDayResponse source)
        {
            if (source == null || source.SettlementDayResponses == null || source.SettlementDayResponses.Any() == false)
            {
                return null;
            }

            DataByDateModel model = new DataByDateModel
                                    {
                                        DataDateModels = new List<DataDateModel>()
                                    };

            source.SettlementDayResponses.ForEach(t => model.DataDateModels.Add(new DataDateModel
                                                                                 {
                                                                                     Count = t.NumberOfTransactionsSettled,
                                                                                     Value = t.ValueOfSettlement,
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
        public DataByWeekModel ConvertFrom(TransactionsByWeekResponse source)
        {
            if (source == null || source.TransactionWeekResponses == null || source.TransactionWeekResponses.Any() == false)
            {
                return null;
            }

            DataByWeekModel model = new DataByWeekModel
            {
                                                DataWeekModels = new List<DataWeekModel>()
                                            };

            source.TransactionWeekResponses.ForEach(t => model.DataWeekModels.Add(new DataWeekModel
                                                                                        {
                                                                                            Count = t.NumberOfTransactions,
                                                                                            Value = t.ValueOfTransactions,
                                                                                            CurrencyCode = t.CurrencyCode,
                                                                                            WeekNumber = t.WeekNumber,
                                                                                            Year = t.Year
                                                                                        }));
            return model;
        }

        public DataByWeekModel ConvertFrom(SettlementByWeekResponse source)
        {
            if (source == null || source.SettlementWeekResponses == null || source.SettlementWeekResponses.Any() == false)
            {
                return null;
            }

            DataByWeekModel model = new DataByWeekModel
                                    {
                                        DataWeekModels = new List<DataWeekModel>()
                                    };

            source.SettlementWeekResponses.ForEach(t => model.DataWeekModels.Add(new DataWeekModel
                                                                                  {
                                                                                      Count = t.NumberOfTransactionsSettled,
                                                                                      Value = t.ValueOfSettlement,
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
        public DataByMonthModel ConvertFrom(TransactionsByMonthResponse source)
        {
            if (source == null || source.TransactionMonthResponses == null || source.TransactionMonthResponses.Any() == false)
            {
                return null;
            }

            DataByMonthModel model = new DataByMonthModel
            {
                                                DataMonthModels = new List<DataMonthModel>()
                                            };

            source.TransactionMonthResponses.ForEach(t => model.DataMonthModels.Add(new DataMonthModel
                                                                                          {
                                                                                              Count = t.NumberOfTransactions,
                                                                                              Value = t.ValueOfTransactions,
                                                                                              CurrencyCode = t.CurrencyCode,
                                                                                              MonthNumber = t.MonthNumber,
                                                                                              Year = t.Year
                                                                                          }));
            return model;
        }

        public DataByMonthModel ConvertFrom(SettlementByMonthResponse source)
        {
            if (source == null || source.SettlementMonthResponses == null || source.SettlementMonthResponses.Any() == false)
            {
                return null;
            }

            DataByMonthModel model = new DataByMonthModel
                                     {
                                         DataMonthModels = new List<DataMonthModel>()
                                     };

            source.SettlementMonthResponses.ForEach(t => model.DataMonthModels.Add(new DataMonthModel
                                                                                    {
                                                                                        Count = t.NumberOfTransactionsSettled,
                                                                                        Value = t.ValueOfSettlement,
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
        public DataByMerchantModel ConvertFrom(TransactionsByMerchantResponse source)
        {
            if (source == null || source.TransactionMerchantResponses == null || source.TransactionMerchantResponses.Any() == false)
            {
                return null;
            }

            DataByMerchantModel model = new DataByMerchantModel
            {
                                                    DataMerchantModels = new List<DataMerchantModel>()
                                                };

            source.TransactionMerchantResponses.ForEach(t => model.DataMerchantModels.Add(new DataMerchantModel
                                                                                                 {
                                                                                                     Count = t.NumberOfTransactions,
                                                                                                     Value = t.ValueOfTransactions,
                                                                                                     CurrencyCode = t.CurrencyCode,
                                                                                                     MerchantId = t.MerchantId,
                                                                                                     MerchantName = t.MerchantName
                                                                                                 }));
            return model;
        }

        public DataByMerchantModel ConvertFrom(SettlementByMerchantResponse source)
        {
            if (source == null || source.SettlementMerchantResponses == null || source.SettlementMerchantResponses.Any() == false)
            {
                return null;
            }

            DataByMerchantModel model = new DataByMerchantModel
            {
                                                    DataMerchantModels = new List<DataMerchantModel>()
                                                };

            source.SettlementMerchantResponses.ForEach(t => model.DataMerchantModels.Add(new DataMerchantModel
            {
                                                                                                     Count = t.NumberOfTransactionsSettled,
                                                                                                     Value = t.ValueOfSettlement,
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
        public DataByOperatorModel ConvertFrom(TransactionsByOperatorResponse source)
        {
            if (source == null || source.TransactionOperatorResponses == null || source.TransactionOperatorResponses.Any() == false)
            {
                return null;
            }

            DataByOperatorModel model = new DataByOperatorModel
                                                {
                                                    DataOperatorModels = new List<DataOperatorModel>()
                                                };

            source.TransactionOperatorResponses.ForEach(t=> model.DataOperatorModels.Add(new DataOperatorModel
                                                                                                {
                                                                                                    CurrencyCode = t.CurrencyCode,
                                                                                                    Count = t.NumberOfTransactions,
                                                                                                    Value = t.ValueOfTransactions,
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
        public FileDetailsModel ConvertFrom(FileDetails source)
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
                                        ProcessingResult = this.ConvertFrom(sourceFileLine.ProcessingResult),
                                        RejectionReason = sourceFileLine.RejectionReason,
                                        TransactionId = sourceFileLine.TransactionId
                                    });
            }

            return model;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public AssignOperatorRequest ConvertFrom(AssignOperatorToMerchantModel source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public AssignOperatorToMerchantResponseModel ConvertFrom(AssignOperatorResponse source)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        private FileLineProcessingResult ConvertFrom(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult dto)
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
                                              AvailableBalance = source.AvailableBalance,
                                              SettlementSchedule = ConvertFrom(source.SettlementSchedule)
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
                                                   Name = source.MerchantName,
                                                   SettlementSchedule = ConvertFrom(source.SettlementSchedule)
                                               };

            return apiRequest;
        }

        private SettlementSchedule ConvertFrom(Models.SettlementSchedule settlementSchedule)
        {
            return settlementSchedule switch
            {
                Models.SettlementSchedule.Immediate => SettlementSchedule.Immediate,
                Models.SettlementSchedule.Weekly => SettlementSchedule.Weekly,
                Models.SettlementSchedule.Monthly=> SettlementSchedule.Monthly,
            };
        }

        private Models.SettlementSchedule ConvertFrom(SettlementSchedule settlementSchedule)
        {
            return settlementSchedule switch
            {
                SettlementSchedule.Immediate => Models.SettlementSchedule.Immediate,
                SettlementSchedule.Weekly => Models.SettlementSchedule.Weekly,
                SettlementSchedule.Monthly => Models.SettlementSchedule.Monthly,
                SettlementSchedule.NotSet => Models.SettlementSchedule.Immediate
            };
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
                                                        Amount = source.Amount
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
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