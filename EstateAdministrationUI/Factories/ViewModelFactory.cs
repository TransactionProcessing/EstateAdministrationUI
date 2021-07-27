namespace EstateAdministrationUI.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using FileLineProcessingResult = Areas.Estate.Models.FileLineProcessingResult;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateAdministrationUI.Factories.IViewModelFactory" />
    /// <seealso cref="IViewModelFactory" />
    public class ViewModelFactory : IViewModelFactory
    {
        #region Methods

        public MerchantBalanceViewModel ConvertFrom(MerchantBalanceModel merchantBalanceModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="estateModel">The estate model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">estateModel</exception>
        public EstateViewModel ConvertFrom(EstateModel estateModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="contractModel">The contract model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">contractModel</exception>
        public ContractProductListViewModel ConvertFrom(ContractModel contractModel)
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
                                                                                              Value = c.Value.HasValue ? c.Value.Value.ToString() : "Variable",
                                                                                              NumberOfTransactionFees = c.ContractProductTransactionFees.Count
                                                                                          });
                                                       });
            }

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="contractProduct">The contract product.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">contractProduct</exception>
        public ContractProductTransactionFeesListViewModel ConvertFrom(ContractProductModel contractProduct)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="createOperatorViewModel">The create operator view model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">createOperatorViewModel</exception>
        public CreateOperatorModel ConvertFrom(CreateOperatorViewModel createOperatorViewModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="createContractViewModel">The create contract view model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">createContractViewModel</exception>
        public CreateContractModel ConvertFrom(CreateContractViewModel createContractViewModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="estateOperatorModels">The estate operator models.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">estateOperatorModels</exception>
        public List<OperatorListViewModel> ConvertFrom(Guid estateId,
                                                       List<EstateOperatorModel> estateOperatorModels)
        {
            if (estateOperatorModels == null || estateOperatorModels.Any() == false)
            {
                throw new ArgumentNullException(nameof(estateOperatorModels));
            }

            List<OperatorListViewModel> viewModels = new List<OperatorListViewModel>();

            estateOperatorModels.ForEach(eo => viewModels.Add(this.ConvertFrom(estateId, eo)));

            return viewModels;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="estateOperatorModel">The estate operator model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">estateOperatorModel</exception>
        public OperatorListViewModel ConvertFrom(Guid estateId,
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="createMerchantViewModel">The create merchant view model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">createMerchantViewModel</exception>
        public CreateMerchantModel ConvertFrom(CreateMerchantViewModel createMerchantViewModel)
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
                                                          MerchantName = createMerchantViewModel.MerchantName
                                                      };

            return createMerchantModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantModels">The merchant models.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">merchantModels</exception>
        public List<MerchantListViewModel> ConvertFrom(List<MerchantModel> merchantModels)
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

        public MerchantCountViewModel ConvertFrom(MerchantModel[] merchants)
        {
            if (merchants == null || merchants.Any() == false)
            {
                return new MerchantCountViewModel
                       {
                           NumberOfMerchants = 0
                       };
            }

            MerchantCountViewModel viewModel = new MerchantCountViewModel();

            viewModel.NumberOfMerchants = merchants.Length;

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="contractModels">The contract models.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">contractModels</exception>
        public List<ContractListViewModel> ConvertFrom(List<ContractModel> contractModels)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantModel">The merchant model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">merchantModel</exception>
        public MerchantViewModel ConvertFrom(MerchantModel merchantModel)
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
            viewModel.AvailableBalance = merchantModel.AvailableBalance;
            viewModel.Addresses = this.ConvertFrom(merchantModel.Addresses);
            viewModel.Contacts = this.ConvertFrom(merchantModel.Contacts);
            viewModel.Operators = this.ConvertFrom(merchantModel.Operators);
            viewModel.Devices = this.ConvertFrom(merchantModel.Devices);

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantBalanceModel">The merchant balance model.</param>
        /// <returns></returns>
        public MerchantBalanceHistoryListViewModel ConvertFrom(List<MerchantBalanceHistory> merchantBalanceModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="makeMerchantDepositViewModel">The make merchant deposit view model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">makeMerchantDepositViewModel</exception>
        public MakeMerchantDepositModel ConvertFrom(MakeMerchantDepositViewModel makeMerchantDepositViewModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="createContractProductViewModel">The create contract product view model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">createContractProductViewModel</exception>
        public AddProductToContractModel ConvertFrom(CreateContractProductViewModel createContractProductViewModel)
        {
            if (createContractProductViewModel == null)
            {
                throw new ArgumentNullException(nameof(createContractProductViewModel));
            }

            AddProductToContractModel addProductToContractModel = new AddProductToContractModel();

            addProductToContractModel.Value = createContractProductViewModel.Value;
            addProductToContractModel.DisplayText = createContractProductViewModel.DisplayText;
            addProductToContractModel.ProductName = createContractProductViewModel.ProductName;

            return addProductToContractModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="createContractProductTransactionFeeViewModel">The create contract product transaction fee view model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">createContractProductTransactionFeeViewModel</exception>
        public AddTransactionFeeToContractProductModel ConvertFrom(CreateContractProductTransactionFeeViewModel createContractProductTransactionFeeViewModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionPeriodViewModel ConvertFrom(TransactionForPeriodModel model)
        {
            TransactionPeriodViewModel viewModel = new TransactionPeriodViewModel();

            if (model == null)
            {
                return viewModel;
            }

            viewModel.NumberOfTransactions = model.NumberOfTransactions;

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByDateViewModel ConvertFrom(TransactionsByDateModel model)
        {
            if (model == null)
            {
                return new TransactionsByDateViewModel();
            }

            TransactionsByDateViewModel viewModel = new TransactionsByDateViewModel();
            viewModel.TransactionDateViewModels = new List<TransactionDateViewModel>();
            foreach (TransactionDateModel transactionDateModel in model.TransactionDateModels)
            {
                viewModel.TransactionDateViewModels.Add(new TransactionDateViewModel
                                                        {
                    CurrencyCode = transactionDateModel.CurrencyCode,
                    Date = transactionDateModel.Date,
                    NumberOfTransactions = transactionDateModel.NumberOfTransactions,
                    ValueOfTransactions = transactionDateModel.ValueOfTransactions
                                                        });
            }

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByWeekViewModel ConvertFrom(TransactionsByWeekModel model)
        {
            if (model == null)
            {
                return new TransactionsByWeekViewModel();
            }

            TransactionsByWeekViewModel viewModel = new TransactionsByWeekViewModel();
            viewModel.TransactionWeekViewModels = new List<TransactionWeekViewModel>();
            
            foreach (TransactionWeekModel transactionWeekModel in model.TransactionWeekModels)
            {
                viewModel.TransactionWeekViewModels.Add(new TransactionWeekViewModel
                                                        {
                                                            CurrencyCode = transactionWeekModel.CurrencyCode,
                                                            WeekNumber = transactionWeekModel.WeekNumber,
                                                            Year = transactionWeekModel.Year,
                                                            NumberOfTransactions = transactionWeekModel.NumberOfTransactions,
                                                            ValueOfTransactions = transactionWeekModel.ValueOfTransactions
                                                        });
            };

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByMonthViewModel ConvertFrom(TransactionsByMonthModel model)
        {
            if (model == null)
            {
                return new TransactionsByMonthViewModel();
            }

            TransactionsByMonthViewModel viewModel = new TransactionsByMonthViewModel();
            viewModel.TransactionMonthViewModels=new List<TransactionMonthViewModel>();


            foreach (TransactionMonthModel transactionMonthModel in model.TransactionMonthModels)
            {
                viewModel.TransactionMonthViewModels.Add(new TransactionMonthViewModel
                                                        {
                                                            CurrencyCode = transactionMonthModel.CurrencyCode,
                                                            MonthNumber = transactionMonthModel.MonthNumber,
                                                            Year = transactionMonthModel.Year,
                                                            NumberOfTransactions = transactionMonthModel.NumberOfTransactions,
                                                            ValueOfTransactions = transactionMonthModel.ValueOfTransactions
                                                        });
            }

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByMerchantViewModel ConvertFrom(TransactionsByMerchantModel model)
        {
            if (model == null)
            {
                return new TransactionsByMerchantViewModel();
            }

            TransactionsByMerchantViewModel viewModel = new TransactionsByMerchantViewModel();
            viewModel.TransactionMerchantViewModels = new List<TransactionMerchantViewModel>();


            foreach (TransactionMerchantModel transactionMerchantModel in model.TransactionMerchantModels)
            {
                viewModel.TransactionMerchantViewModels.Add(new TransactionMerchantViewModel
                                                         {
                                                             CurrencyCode = transactionMerchantModel.CurrencyCode,
                                                             MerchantId = transactionMerchantModel.MerchantId,
                                                             MerchantName = transactionMerchantModel.MerchantName,
                                                             NumberOfTransactions = transactionMerchantModel.NumberOfTransactions,
                                                             ValueOfTransactions = transactionMerchantModel.ValueOfTransactions
                                                         });
            }

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByOperatorViewModel ConvertFrom(TransactionsByOperatorModel model)
        {
            if (model == null)
            {
                return new TransactionsByOperatorViewModel();
            }

            TransactionsByOperatorViewModel viewModel = new TransactionsByOperatorViewModel();
            viewModel.TransactionOperatorViewModels = new List<TransactionOperatorViewModel>();

            foreach (TransactionOperatorModel transactionOperatorModel in model.TransactionOperatorModels)
            {
                viewModel.TransactionOperatorViewModels.Add(new TransactionOperatorViewModel
                                                            {
                                                                ValueOfTransactions = transactionOperatorModel.ValueOfTransactions,
                                                                CurrencyCode = transactionOperatorModel.CurrencyCode,
                                                                NumberOfTransactions = transactionOperatorModel.NumberOfTransactions,
                                                                OperatorName = transactionOperatorModel.OperatorName
                                                            });
            }

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns></returns>
        public List<FileImportLogViewModel> ConvertFrom(List<FileImportLogModel> models)
        {
            if (models == null || models.Any() == false)
            {
                return new List<FileImportLogViewModel>();
            }

            List<FileImportLogViewModel> viewModels = new List<FileImportLogViewModel>();

            foreach (FileImportLogModel fileImportLogModel in models)
            {
                viewModels.Add(this.ConvertFrom(fileImportLogModel));
            }

            return viewModels;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The models.</param>
        /// <returns></returns>
        public FileImportLogViewModel ConvertFrom(FileImportLogModel model)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public FileDetailsViewModel ConvertFrom(FileDetailsModel model)
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
                    (FileLineProcessingResult result, String stringResult) processingResult = this.ConvertFrom(modelFileLine.ProcessingResult);

                    fileLineViewModel.ProcessingResult = processingResult.result;
                    fileLineViewModel.ProcessingResultString = processingResult.stringResult;

                    viewModel.FileLines.Add(fileLineViewModel);

                }
            }

            return viewModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private (FileLineProcessingResult result,String stringResult) ConvertFrom(BusinessLogic.Models.FileLineProcessingResult model)
        {
            (FileLineProcessingResult, String) viewModel = (FileLineProcessingResult.Unknown,"Unknown");
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
        
        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="deviceModels">The device models.</param>
        /// <returns></returns>
        private Dictionary<String, String> ConvertFrom(Dictionary<Guid, String> deviceModels)
        {
            Dictionary<String, String> viewModels = new Dictionary<String, String>();

            if (deviceModels == null || deviceModels.Any() == false)
            {
                return viewModels;
            }

            foreach (KeyValuePair<Guid, String> model in deviceModels)
            {
                viewModels.Add(model.Key.ToString(), model.Value);
            }

            return viewModels;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="addressModels">The address models.</param>
        /// <returns></returns>
        private List<AddressViewModel> ConvertFrom(List<AddressModel> addressModels)
        {
            List<AddressViewModel> viewModels = new List<AddressViewModel>();

            if (addressModels == null || addressModels.Any() == false)
            {
                return viewModels;
            }

            foreach (AddressModel model in addressModels)
            {
                viewModels.Add(this.ConvertFrom(model));
            }

            return viewModels;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="addressModel">The address model.</param>
        /// <returns></returns>
        private AddressViewModel ConvertFrom(AddressModel addressModel)
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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="contactModels">The contact models.</param>
        /// <returns></returns>
        private List<ContactViewModel> ConvertFrom(List<ContactModel> contactModels)
        {
            List<ContactViewModel> viewModels = new List<ContactViewModel>();

            if (contactModels == null)
            {
                return viewModels;
            }

            foreach (ContactModel model in contactModels)
            {
                viewModels.Add(this.ConvertFrom(model));
            }

            return viewModels;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="contactModel">The contact model.</param>
        /// <returns></returns>
        private ContactViewModel ConvertFrom(ContactModel contactModel)
        {
            return new ContactViewModel
                   {
                       ContactEmailAddress = contactModel.ContactEmailAddress,
                       ContactId = contactModel.ContactId,
                       ContactName = contactModel.ContactName,
                       ContactPhoneNumber = contactModel.ContactPhoneNumber
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="operatorModels">The operator models.</param>
        /// <returns></returns>
        private List<MerchantOperatorViewModel> ConvertFrom(List<MerchantOperatorModel> operatorModels)
        {
            List<MerchantOperatorViewModel> viewModels = new List<MerchantOperatorViewModel>();

            if (operatorModels == null || operatorModels.Any() == false)
            {
                return viewModels;
            }

            foreach (MerchantOperatorModel model in operatorModels)
            {
                viewModels.Add(this.ConvertFrom(model));
            }

            return viewModels;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="operatorModel">The operator model.</param>
        /// <returns></returns>
        private MerchantOperatorViewModel ConvertFrom(MerchantOperatorModel operatorModel)
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