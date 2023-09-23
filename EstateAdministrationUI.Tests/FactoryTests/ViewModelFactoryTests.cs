using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.Tests.FactoryTests
{
    using System.Linq;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using Castle.Components.DictionaryAdapter;
    using EstateAdministrationUI.BusinessLogic.Factories;
    using EstateReportingAPI.DataTransferObjects;
    using Factories;
    using Shouldly;
    using Testing;
    using Xunit;
    using FileLineProcessingResult = Areas.Estate.Models.FileLineProcessingResult;

    public class ViewModelFactoryTests
    {
        [Fact]
        public void ModelFactory_ConvertFrom_TodaysSettlement_IsConverted()
        {

            TodaysSettlementModel model = new TodaysSettlementModel()
                                     {
                                         ComparisonSettlementCount = 100,
                                         ComparisonSettlementValue = 101.00m,
                                         TodaysSettlementCount = 200,
                                         TodaysSettlementValue = 202.00m
                                     };
            var result = ViewModelFactory.ConvertFrom(model);
            result.ShouldNotBeNull();
            result.ComparisonSettlementValue.ShouldBe(model.ComparisonSettlementValue);
            result.TodaysSettlementValue.ShouldBe(model.TodaysSettlementValue);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TodaysSettlementModel_ModelIsNull_ErrorThrown()
        {
            TodaysSettlementModel model = null;
            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantKpiModel_IsConverted()
        {

            MerchantKpiModel model = new MerchantKpiModel()
                                {
                                    MerchantsWithNoSaleInLast7Days = 1,
                                    MerchantsWithNoSaleToday = 2,
                                    MerchantsWithSaleInLastHour = 3
                                };
            var result = ViewModelFactory.ConvertFrom(model);
            result.ShouldNotBeNull();
            result.MerchantsWithNoSaleInLast7Days.ShouldBe(model.MerchantsWithNoSaleInLast7Days);
            result.MerchantsWithNoSaleToday.ShouldBe(model.MerchantsWithNoSaleToday);
            result.MerchantsWithSaleInLastHour.ShouldBe(model.MerchantsWithSaleInLastHour);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantKpiModel_ModelIsNull_ErrorThrown()
        {

            MerchantKpiModel model = null;
            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TodaysSalesModel_IsConverted()
        {
            TodaysSalesModel model = new TodaysSalesModel
            {
                                    ComparisonSalesCount = 10,
                                    ComparisonSalesValue = 100.00m,
                                    TodaysSalesCount = 20,
                                    TodaysSalesValue = 200.00m
                                };

            var result = ViewModelFactory.ConvertFrom(model, String.Empty);

            result.ShouldNotBeNull();
            result.ComparisonValueOfTransactions.ShouldBe(model.ComparisonSalesValue);
            result.TodaysValueOfTransactions.ShouldBe(model.TodaysSalesValue);
            result.Label.ShouldBe($" Sales");
            result.Variance.ShouldBe(0.5m);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TodaysSalesModel_ModelIsNull_ErrorThrown()
        {
            TodaysSalesModel model = null;
            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(model, String.Empty); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateModel_ModelIsConverted()
        {
            EstateModel model = TestData.EstateModel;

            EstateViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.EstateName.ShouldBe(model.EstateName);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateModel_NullModel_ErrorThrown()
        {
            EstateModel model = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(model); });
        }

        [Theory]
        [InlineData(0,SettlementSchedule.Immediate)]
        [InlineData(1,SettlementSchedule.Weekly)]
        [InlineData(2,SettlementSchedule.Monthly)]

        public void ViewModelFactory_ConvertFrom_CreateMerchantViewModel_ModelIsConverted(Int32 settlementSchedule,SettlementSchedule expectedSettlementSchedule)
        {
            CreateMerchantViewModel viewModel = TestData.CreateMerchantViewModel(settlementSchedule);

            CreateMerchantModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.MerchantName.ShouldBe(viewModel.MerchantName);
            model.SettlementSchedule.ShouldBe(expectedSettlementSchedule);
            model.Contact.ContactName.ShouldBe(viewModel.ContactName);
            model.Contact.ContactPhoneNumber.ShouldBe(viewModel.ContactPhoneNumber);
            model.Contact.ContactEmailAddress.ShouldBe(viewModel.ContactEmailAddress);

            model.Address.AddressLine1.ShouldBe(viewModel.AddressLine1);
            model.Address.AddressLine2.ShouldBe(viewModel.AddressLine2);
            model.Address.AddressLine3.ShouldBe(viewModel.AddressLine3);
            model.Address.AddressLine4.ShouldBe(viewModel.AddressLine4);
            model.Address.Region.ShouldBe(viewModel.Region);
            model.Address.Town.ShouldBe(viewModel.Town);
            model.Address.Country.ShouldBe(viewModel.Country);
            model.Address.PostalCode.ShouldBe(viewModel.PostalCode);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateMerchantViewModel_NullModel_ErrorThrown()
        {
            CreateMerchantViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(viewModel); });
        }

        [Theory]
        [InlineData(SettlementSchedule.Immediate)]
        [InlineData(SettlementSchedule.Weekly)]
        [InlineData(SettlementSchedule.Monthly)]
        public void ViewModelFactory_ConvertFrom_MerchantModel_ModelIsConverted(SettlementSchedule settlementSchedule)
        {
            MerchantModel model = TestData.MerchantModel(settlementSchedule);

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.SettlementSchedule.ShouldBe((Int32)settlementSchedule);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
                                   {
                                       // Find the contact in the view model
                                       ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                                       viewModelContact.ShouldNotBeNull();
                                       viewModelContact.ContactName.ShouldBe(c.ContactName);
                                       viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                       viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                   });

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
                                    {
                                        // Find the operator in the view model
                                        MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                                        viewModelOperator.ShouldNotBeNull();
                                        viewModelOperator.Name.ShouldBe(o.Name);
                                        viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                        viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                    });
            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
                                    {
                                        // Find the operator in the view model
                                        AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                                        viewModelAddress.ShouldNotBeNull();
                                        viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                        viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                        viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                        viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                        viewModelAddress.Country.ShouldBe(a.Country);
                                        viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                                        viewModelAddress.Region.ShouldBe(a.Region);
                                        viewModelAddress.Town.ShouldBe(a.Town);

                                    });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_NullOperators_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Operators = null;

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                viewModelContact.ShouldNotBeNull();
                viewModelContact.ContactName.ShouldBe(c.ContactName);
                viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.ShouldBeEmpty();

            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                viewModelAddress.ShouldNotBeNull();
                viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                viewModelAddress.Country.ShouldBe(a.Country);
                viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                viewModelAddress.Region.ShouldBe(a.Region);
                viewModelAddress.Town.ShouldBe(a.Town);

            });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_EmptyOperators_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Operators = new List<MerchantOperatorModel>();

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                viewModelContact.ShouldNotBeNull();
                viewModelContact.ContactName.ShouldBe(c.ContactName);
                viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.ShouldBeEmpty();

            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                viewModelAddress.ShouldNotBeNull();
                viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                viewModelAddress.Country.ShouldBe(a.Country);
                viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                viewModelAddress.Region.ShouldBe(a.Region);
                viewModelAddress.Town.ShouldBe(a.Town);

            });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_NullDevices_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Devices = null;

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                viewModelContact.ShouldNotBeNull();
                viewModelContact.ContactName.ShouldBe(c.ContactName);
                viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            viewModel.Devices.ShouldBeEmpty();

            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                viewModelOperator.ShouldNotBeNull();
                viewModelOperator.Name.ShouldBe(o.Name);
                viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                viewModelAddress.ShouldNotBeNull();
                viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                viewModelAddress.Country.ShouldBe(a.Country);
                viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                viewModelAddress.Region.ShouldBe(a.Region);
                viewModelAddress.Town.ShouldBe(a.Town);

            });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_EmptyDevices_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Devices = new Dictionary<Guid, String>();

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                viewModelContact.ShouldNotBeNull();
                viewModelContact.ContactName.ShouldBe(c.ContactName);
                viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            viewModel.Devices.ShouldBeEmpty();

            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                viewModelOperator.ShouldNotBeNull();
                viewModelOperator.Name.ShouldBe(o.Name);
                viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                viewModelAddress.ShouldNotBeNull();
                viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                viewModelAddress.Country.ShouldBe(a.Country);
                viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                viewModelAddress.Region.ShouldBe(a.Region);
                viewModelAddress.Town.ShouldBe(a.Town);

            });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_NullAddresses_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Addresses = null;
            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                viewModelContact.ShouldNotBeNull();
                viewModelContact.ContactName.ShouldBe(c.ContactName);
                viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                viewModelOperator.ShouldNotBeNull();
                viewModelOperator.Name.ShouldBe(o.Name);
                viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            viewModel.Addresses.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_EmptyAddresses_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Addresses = new List<AddressModel>();

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.Count.ShouldBe(model.Contacts.Count);
            model.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactViewModel viewModelContact = viewModel.Contacts.SingleOrDefault(vmc => vmc.ContactId == c.ContactId);
                viewModelContact.ShouldNotBeNull();
                viewModelContact.ContactName.ShouldBe(c.ContactName);
                viewModelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                viewModelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                viewModelOperator.ShouldNotBeNull();
                viewModelOperator.Name.ShouldBe(o.Name);
                viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            viewModel.Addresses.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_NullContacts_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Contacts = null;

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.ShouldBeEmpty();

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                viewModelOperator.ShouldNotBeNull();
                viewModelOperator.Name.ShouldBe(o.Name);
                viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                viewModelAddress.ShouldNotBeNull();
                viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                viewModelAddress.Country.ShouldBe(a.Country);
                viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                viewModelAddress.Region.ShouldBe(a.Region);
                viewModelAddress.Town.ShouldBe(a.Town);

            });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_EmptyContacts_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel();
            model.Contacts = null;

            MerchantViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            //viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            //viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Contacts.ShouldBeEmpty();

            viewModel.Devices.Count.ShouldBe(model.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in model.Devices)
            {
                model.Devices.ContainsKey(device.Key).ShouldBeTrue();
                model.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            viewModel.Operators.Count.ShouldBe(model.Operators.Count);
            model.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorViewModel viewModelOperator = viewModel.Operators.SingleOrDefault(vmo => vmo.OperatorId == o.OperatorId);
                viewModelOperator.ShouldNotBeNull();
                viewModelOperator.Name.ShouldBe(o.Name);
                viewModelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                viewModelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            viewModel.Addresses.Count.ShouldBe(model.Addresses.Count);
            model.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressViewModel viewModelAddress = viewModel.Addresses.SingleOrDefault(vma => vma.AddressId == a.AddressId);
                viewModelAddress.ShouldNotBeNull();
                viewModelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                viewModelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                viewModelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                viewModelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                viewModelAddress.Country.ShouldBe(a.Country);
                viewModelAddress.PostalCode.ShouldBe(a.PostalCode);
                viewModelAddress.Region.ShouldBe(a.Region);
                viewModelAddress.Town.ShouldBe(a.Town);

            });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_NullModel_ErrorThrown()
        {
            MerchantModel model = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantFirstAddressIsNull_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Addresses[0] = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBeEmpty();
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithNullAddress_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Addresses = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBeEmpty();
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithEmptyAddress_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Addresses = new List<AddressModel>();

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBeEmpty();
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantFirstContactIsNull_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Contacts[0] = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBeEmpty();
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithNullContacts_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Contacts = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBeEmpty();
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithEmptyContacts_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Contacts = new List<ContactModel>();

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBeEmpty();
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }
        
        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithNullDevices_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Devices = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(0);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithEmptyDevices_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Devices = new Dictionary<Guid, String>();

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantFirstOperatorIsNull_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Operators[0] = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithNullOperators_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Operators = null;

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(0);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_MerchantWithEmptyOperators_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel()
                                            };
            modelList.First().Operators = new List<MerchantOperatorModel>();

            List<MerchantListViewModel> viewModelList = ViewModelFactory.ConvertFrom(modelList);

            MerchantListViewModel viewModel = viewModelList.SingleOrDefault();
            viewModel.ShouldNotBeNull();
            MerchantModel model = modelList.Single();

            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AddressLine1.ShouldBe(model.Addresses.First().AddressLine1);
            viewModel.ContactName.ShouldBe(model.Contacts.First().ContactName);
            viewModel.NumberOfDevices.ShouldBe(model.Devices.Count);
            viewModel.NumberOfOperators.ShouldBe(model.Operators.Count);
            viewModel.NumberOfUsers.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_NullModel_ErrorThrown()
        {
            List<MerchantModel> modelList = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(modelList); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_EmptyModel_ErrorThrown()
        {
            List<MerchantModel> modelList = new List<MerchantModel>();

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(modelList); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MakeMerchantDepositViewModel_ModelIsConverted()
        {
            MakeMerchantDepositViewModel viewModel = TestData.MakeMerchantDepositViewModel;

            MakeMerchantDepositModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.MerchantId.ShouldBe(Guid.Parse(viewModel.MerchantId));
            model.DepositDateTime.ShouldBe(DateTime.ParseExact(viewModel.DepositDate, "dd/MM/yyyy", null));
            model.Reference.ShouldBe(viewModel.Reference);
            model.Amount.ShouldBe(Decimal.Parse(viewModel.Amount));
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MakeMerchantDepositViewModel_NullModel_ErrorThrown()
        {
            MakeMerchantDepositViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(viewModel); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateOperatorViewModel_ModelIsConverted()
        {
            CreateOperatorViewModel viewModel = TestData.CreateOperatorViewModel;

            CreateOperatorModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.OperatorName.ShouldBe(viewModel.OperatorName);
            model.RequireCustomTerminalNumber.ShouldBe(viewModel.RequireCustomTerminalNumber);
            model.RequireCustomMerchantNumber.ShouldBe(viewModel.RequireCustomMerchantNumber);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateOperatorViewModel_NullModel_ErrorThrown()
        {
            CreateOperatorViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(viewModel); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModel_ModelIsConverted()
        {
            EstateOperatorModel model = TestData.EstateOperatorModel;

            OperatorListViewModel viewModel = ViewModelFactory.ConvertFrom(TestData.EstateId, model);

            viewModel.EstateId.ShouldBe(TestData.EstateId);
            viewModel.OperatorName.ShouldBe(model.Name);
            viewModel.RequireCustomTerminalNumber.ShouldBe(model.RequireCustomTerminalNumber);
            viewModel.RequireCustomMerchantNumber.ShouldBe(model.RequireCustomMerchantNumber);
            viewModel.OperatorId.ShouldBe(model.OperatorId);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModel_NullModel_ErrorThrown()
        {
            EstateOperatorModel model = null;

            Should.Throw<ArgumentNullException>(() => { ViewModelFactory.ConvertFrom(TestData.EstateId, model); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModelList_ModelIsConverted()
        {
            List<EstateOperatorModel> modelList = new List<EstateOperatorModel>
                                                  {
                                                      TestData.EstateOperatorModel
                                                  };

            List<OperatorListViewModel> viewModelList = ViewModelFactory.ConvertFrom(TestData.EstateId, modelList);

            viewModelList.ShouldNotBeNull();
            viewModelList.ShouldNotBeEmpty();
            viewModelList.ShouldHaveSingleItem();
            viewModelList.Single().EstateId.ShouldBe(TestData.EstateId);
            viewModelList.Single().OperatorName.ShouldBe(TestData.EstateOperatorModel.Name);
            viewModelList.Single().RequireCustomTerminalNumber.ShouldBe(TestData.EstateOperatorModel.RequireCustomTerminalNumber);
            viewModelList.Single().RequireCustomMerchantNumber.ShouldBe(TestData.EstateOperatorModel.RequireCustomMerchantNumber);
            viewModelList.Single().OperatorId.ShouldBe(TestData.EstateOperatorModel.OperatorId);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModelList_EmptyList_ErrorThrown()
        {
            List<EstateOperatorModel> modelList = new List<EstateOperatorModel>();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(TestData.EstateId, modelList);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModelList_NullList_ErrorThrown()
        {
            List<EstateOperatorModel> modelList = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(TestData.EstateId, modelList);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractViewModel_IsConverted()
        {
            CreateContractViewModel viewModel = TestData.CreateContractViewModel;

            CreateContractModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.OperatorId.ShouldBe(viewModel.OperatorId);
            model.Description.ShouldBe(viewModel.ContractDescription);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractViewModel_NullModel_ErrorThrown()
        {
            CreateContractViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_IsConverted()
        {
            ContractModel model = TestData.ContractModel;

            ContractProductListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.Count.ShouldBe(model.ContractProducts.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_NullProducts_IsConverted()
        {
            ContractModel model = TestData.ContractModelNullProducts;

            ContractProductListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_EmptyProducts_IsConverted()
        {
            ContractModel model = TestData.ContractModelEmptyProducts;

            ContractProductListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_ProductWithNullValue_IsConverted()
        {
            ContractModel model = TestData.ContractModelWithNullValueProduct;

            ContractProductListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.Count.ShouldBe(model.ContractProducts.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_NullModel_ErrorThrown()
        {
            ContractModel model = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ViewModelFactory_ConvertFrom_ContractProductModel_IsConverted(Int32 productType)
        {
            ContractProductModel model = TestData.ContractProductModel(productType);

            ContractProductTransactionFeesListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.Description.ShouldBe(model.Description);
            viewModel.Value.ShouldBe(model.Value.ToString());
            viewModel.ContractProductId.ShouldBe(model.ContractProductId);
            viewModel.ProductName.ShouldBe(model.ProductName);
            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.TransactionFees.Count.ShouldBe(model.ContractProductTransactionFees.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractProductModel_NullValue_IsConverted()
        {
            ContractProductModel model = TestData.ContractProductModelNullValue;

            ContractProductTransactionFeesListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.Description.ShouldBe(model.Description);
            viewModel.Value.ShouldBe("Variable");
            viewModel.ContractProductId.ShouldBe(model.ContractProductId);
            viewModel.ProductName.ShouldBe(model.ProductName);
            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.TransactionFees.Count.ShouldBe(model.ContractProductTransactionFees.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractProductModel_NullFees_IsConverted()
        {
            ContractProductModel model = TestData.ContractProductModelNullFees;

            ContractProductTransactionFeesListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.Description.ShouldBe(model.Description);
            viewModel.Value.ShouldBe(model.Value.ToString());
            viewModel.ContractProductId.ShouldBe(model.ContractProductId);
            viewModel.ProductName.ShouldBe(model.ProductName);
            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.TransactionFees.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractProductModel_EmptyFees_IsConverted()
        {
            ContractProductModel model = TestData.ContractProductModelEmptyFeeList;

            ContractProductTransactionFeesListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.Description.ShouldBe(model.Description);
            viewModel.Value.ShouldBe(model.Value.ToString());
            viewModel.ContractProductId.ShouldBe(model.ContractProductId);
            viewModel.ProductName.ShouldBe(model.ProductName);
            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.TransactionFees.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractProductModel_NullModel_ErrorThrown()
        {
            ContractProductModel model = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModelList_IsConverted()
        {
            List<ContractModel> modelList = new List<ContractModel>
                                            {
                                                TestData.ContractModel
                                            };

            List<ContractListViewModel> viewModels = ViewModelFactory.ConvertFrom(modelList);

            viewModels.ShouldHaveSingleItem();
            ContractListViewModel viewModel = viewModels.Single();
            viewModel.ContractId.ShouldBe(TestData.ContractModel.ContractId);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModelList_NullList_ErrorThrown()
        {
            List<ContractModel> modelList = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(modelList);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductViewModel_WithValue_IsConverted()
        {
            CreateContractProductViewModel viewModel = TestData.CreateContractProductViewModelWithValue;

            AddProductToContractModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.Value.ShouldBe(viewModel.Value);
            model.DisplayText.ShouldBe(viewModel.DisplayText);
            model.ProductName.ShouldBe(viewModel.ProductName);
            model.ProductType.ShouldBe(Int32.Parse(viewModel.ProductType));
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductViewModel_WithNullValue_IsConverted()
        {
            CreateContractProductViewModel viewModel = TestData.CreateContractProductViewModelWithNullValue;

            AddProductToContractModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.Value.ShouldBeNull();
            model.DisplayText.ShouldBe(viewModel.DisplayText);
            model.ProductName.ShouldBe(viewModel.ProductName);
            model.ProductType.ShouldBe(Int32.Parse(viewModel.ProductType));
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductViewModel_NullViewModel_IsConverted()
        {
            CreateContractProductViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductTransactionFeeViewModel_IsConverted()
        {
            CreateContractProductTransactionFeeViewModel viewModel = TestData.CreateContractProductTransactionFeeViewModel;

            AddTransactionFeeToContractProductModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.Value.ShouldBe(viewModel.Value);
            model.CalculationType.ShouldBe((CalculationType)(viewModel.CalculationType -1));
            model.Description.ShouldBe(viewModel.FeeDescription);
            model.FeeType.ShouldBe((FeeType)(viewModel.FeeType - 1));
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductTransactionFeeViewModel_NullModel_ErrorThrown()
        {
            CreateContractProductTransactionFeeViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(viewModel);
                                                });
        }
        
        [Fact]
        public void ViewModelFactory_CovertFrom_MerchantBalanceHistoryModel_IsConverted()
        {
            List<MerchantBalanceHistory> model = TestData.MerchantBalanceHistoryList;

            MerchantBalanceHistoryListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.MerchantBalanceHistoryViewModels.Count.ShouldBe(model.Count);
        }

        [Fact]
        public void ViewModelFactory_CovertFrom_MerchantBalanceHistoryModel_ModelIsNull_IsConverted()
        {
            List<MerchantBalanceHistory> model = null;

            MerchantBalanceHistoryListViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.MerchantBalanceHistoryViewModels.ShouldBeNull();
        }
        
        [Fact]
        public void ViewModelFactory_CovertFrom_FileImportLogModelList_IsConverted()
        {
            List<FileImportLogModel> modelList = new List<FileImportLogModel>
                                            {
                                                TestData.FileImportLogModel
                                            };


            List<FileImportLogViewModel> viewModel = ViewModelFactory.ConvertFrom(modelList);

            viewModel.ShouldNotBeNull();
            viewModel.Count.ShouldBe(modelList.Count);
            foreach (FileImportLogModel fileImportLogModel in modelList)
            {
                FileImportLogViewModel fileImportLogViewModel = viewModel.SingleOrDefault(v => v.FileImportLogId == fileImportLogModel.FileImportLogId);
                fileImportLogViewModel.ShouldNotBeNull();
                fileImportLogViewModel.FileCount.ShouldBe(fileImportLogModel.FileCount);
                fileImportLogViewModel.ImportLogDate.ShouldBe(fileImportLogModel.ImportLogDate);
                fileImportLogViewModel.ImportLogDateTime.ShouldBe(fileImportLogModel.ImportLogDateTime);
                fileImportLogViewModel.ImportLogTime.ShouldBe(fileImportLogModel.ImportLogTime);
                fileImportLogViewModel.Files.Count.ShouldBe(fileImportLogModel.Files.Count);

                foreach (FileImportLogFileModel fileImportLogFileModel in fileImportLogModel.Files)
                {
                    FileImportLogFileViewModel fileImportLogFileViewModel = fileImportLogViewModel.Files.SingleOrDefault(f => f.FileId == fileImportLogFileModel.FileId);
                    fileImportLogFileViewModel.ShouldNotBeNull();
                    fileImportLogFileViewModel.FileImportLogId.ShouldBe(fileImportLogFileViewModel.FileImportLogId);
                    fileImportLogFileViewModel.FileProfileId.ShouldBe(fileImportLogFileViewModel.FileProfileId);
                    fileImportLogFileViewModel.FilePath.ShouldBe(fileImportLogFileViewModel.FilePath);
                    fileImportLogFileViewModel.FileUploadedDateTime.ShouldBe(fileImportLogFileViewModel.FileUploadedDateTime);
                    fileImportLogFileViewModel.MerchantId.ShouldBe(fileImportLogFileViewModel.MerchantId);
                    fileImportLogFileViewModel.OriginalFileName.ShouldBe(fileImportLogFileViewModel.OriginalFileName);
                    fileImportLogFileViewModel.UserId.ShouldBe(fileImportLogFileViewModel.UserId);
                }
            }
        }

        [Fact]
        public void ViewModelFactory_CovertFrom_FileImportLogModelList_NullModel_IsConverted()
        {
            List<FileImportLogModel> modelList = null;
            
            List<FileImportLogViewModel> viewModel = ViewModelFactory.ConvertFrom(modelList);

            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_CovertFrom_FileImportLogModel_NullModel_IsConverted()
        {
            FileImportLogModel model = null;
            
            FileImportLogViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ShouldBeNull();
        }

        [Fact]
        public void ViewModelFactory_CovertFrom_FileImportLogModelList_EmptyModelList_IsConverted()
        {
            List<FileImportLogModel> modelList = new List<FileImportLogModel>();
            
            List<FileImportLogViewModel> viewModel = ViewModelFactory.ConvertFrom(modelList);

            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_FileDetails_IsConverted()
        {
            FileDetailsModel model = TestData.FileDetailsModel;

            FileDetailsViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ShouldNotBeNull();
            viewModel.UserId.ShouldBe(model.UserId);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.FileId.ShouldBe(model.FileId);
            viewModel.FileImportLogId.ShouldBe(model.FileImportLogId);
            viewModel.FileLocation.ShouldBe(model.FileLocation);
            viewModel.FileProfileId.ShouldBe(model.FileProfileId);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.ProcessingCompleted.ShouldBe(model.ProcessingCompleted);
            viewModel.ProcessingSummary.ShouldNotBeNull();
            viewModel.ProcessingSummary.IgnoredLines.ShouldBe(model.ProcessingSummary.IgnoredLines);
            viewModel.ProcessingSummary.FailedLines.ShouldBe(model.ProcessingSummary.FailedLines);
            viewModel.ProcessingSummary.NotProcessedLines.ShouldBe(model.ProcessingSummary.NotProcessedLines);
            viewModel.ProcessingSummary.RejectedLines.ShouldBe(model.ProcessingSummary.RejectedLines);
            viewModel.ProcessingSummary.SuccessfullyProcessedLines.ShouldBe(model.ProcessingSummary.SuccessfullyProcessedLines);
            viewModel.ProcessingSummary.TotalLines.ShouldBe(model.ProcessingSummary.TotalLines);
            viewModel.FileLines.ShouldNotBeNull();
            viewModel.FileLines.ShouldNotBeEmpty();

            foreach (FileLineModel modelFileLine in model.FileLines)
            {
                var line = viewModel.FileLines.SingleOrDefault(f => f.LineNumber == modelFileLine.LineNumber);
                line.ShouldNotBeNull();
                line.LineData.ShouldBe(modelFileLine.LineData);
                line.TransactionId.ShouldBe(modelFileLine.TransactionId);
                line.RejectionReason.ShouldBe(modelFileLine.RejectionReason);
                line.ProcessingResult.ToString().ShouldBe(modelFileLine.ProcessingResult.ToString());
                line.ProcessingResultString.Replace(" ", "").ShouldBe(modelFileLine.ProcessingResult.ToString());
            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_FileDetails_NullDetails_IsConverted()
        {
            FileDetailsModel response = null;

            FileDetailsViewModel viewModel = ViewModelFactory.ConvertFrom(response);

            viewModel.ShouldBeNull();
        }
        
        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantBalanceModel_ModelIsConverted()
        {
            MerchantBalanceModel model = new MerchantBalanceModel
                                         {
                                             EstateId = TestData.EstateId,
                                             AvailableBalance = TestData.AvailableBalance,
                                             Balance = TestData.Balance,
                                             MerchantId = TestData.MerchantId
                                         };

            MerchantBalanceViewModel viewModel = ViewModelFactory.ConvertFrom(model);

            viewModel.ShouldNotBeNull();
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantBalanceModel_NullModel_ErrorThrown()
        {
            MerchantBalanceModel model = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_AddMerchantDeviceViewModel_ModelIsConverted()
        {
            AddMerchantDeviceViewModel viewModel = new AddMerchantDeviceViewModel
            {
                                                   DeviceIdentifier = TestData.DeviceIdentifier,
                                                   MerchantId = TestData.MerchantId
                                               };

            AddMerchantDeviceModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.ShouldNotBeNull();
            model.DeviceIdentifier.ShouldBe(viewModel.DeviceIdentifier);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_AddMerchantDeviceViewModel_NullModel_ErrorThrown()
        {
            AddMerchantDeviceViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_AssignOperatorToMerchantViewModel_ModelIsConverted()
        {
            AssignOperatorToMerchantViewModel viewModel = new AssignOperatorToMerchantViewModel
            {
                                                              MerchantNumber = TestData.MerchantNumber,
                                                              OperatorId = TestData.OperatorId,
                                                              MerchantId = TestData.MerchantId,
                                                              TerminalNumber = TestData.TerminalNumber
                                                          };

            AssignOperatorToMerchantModel model = ViewModelFactory.ConvertFrom(viewModel);

            model.ShouldNotBeNull();
            model.OperatorId.ShouldBe(viewModel.OperatorId);
            model.MerchantNumber.ShouldBe(viewModel.MerchantNumber);
            model.TerminalNumber.ShouldBe(viewModel.TerminalNumber);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_AssignOperatorToMerchantViewModel_NullModel_ErrorThrown()
        {
            AssignOperatorToMerchantViewModel viewModel = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractProductTypeModelList_ListConverted(){
            List<ContractProductTypeModel> modelList = new List<ContractProductTypeModel>{
                                                                                             new ContractProductTypeModel{
                                                                                                                             Description = "Test ProductType",
                                                                                                                             ProductType = 1
                                                                                                                         }
                                                                                         };

            List<ContractProductTypeViewModel> contractProductTypeViewModels = ViewModelFactory.ConvertFrom(modelList);

            contractProductTypeViewModels.Count.ShouldBe(modelList.Count);
            contractProductTypeViewModels.Single().Description.ShouldBe(modelList.Single().Description);
            contractProductTypeViewModels.Single().ProductType.ShouldBe(modelList.Single().ProductType.ToString());
        }

        [Theory]
        [InlineData(BusinessLogic.Models.FileLineProcessingResult.Failed, FileLineProcessingResult.Failed, "Failed")]
        [InlineData(BusinessLogic.Models.FileLineProcessingResult.Ignored, FileLineProcessingResult.Ignored, "Ignored")]
        [InlineData(BusinessLogic.Models.FileLineProcessingResult.NotProcessed, FileLineProcessingResult.NotProcessed, "Not Processed")]
        [InlineData(BusinessLogic.Models.FileLineProcessingResult.Rejected, FileLineProcessingResult.Rejected, "Rejected")]
        [InlineData(BusinessLogic.Models.FileLineProcessingResult.Successful, FileLineProcessingResult.Successful, "Successful")]
        [InlineData(BusinessLogic.Models.FileLineProcessingResult.Unknown, FileLineProcessingResult.Unknown, "Unknown")]
        [InlineData((BusinessLogic.Models.FileLineProcessingResult)99, FileLineProcessingResult.Unknown, "Unknown")]

        public void ViewModelFactory_ConvertFrom_FileLineProcessingResult_ResultConverted(BusinessLogic.Models.FileLineProcessingResult processingResult, 
                                                                                          FileLineProcessingResult expectedResult, String stringResult){
            (FileLineProcessingResult result, String stringResult) result = ViewModelFactory.ConvertFrom(processingResult);
            result.result.ShouldBe(expectedResult);
            result.stringResult.ShouldBe(stringResult);
        }

        [Theory]
        [InlineData(ProductType.BillPayment, "Bill Payment")]
        [InlineData(ProductType.MobileTopup, "Mobile Topup")]
        [InlineData(ProductType.Voucher, "Voucher")]
        public void ViewModelFactory_GetProductTypeName_TypeConverted(ProductType productType, String expectedValue){
            String result = ViewModelFactory.GetProductTypeName(productType);

            result.ShouldBe(expectedValue);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomOperatorDataModel_IsConverted()
        {
            List<TopBottomOperatorDataModel> model = new List<TopBottomOperatorDataModel>{
                                                                                             new TopBottomOperatorDataModel(){
                                                                                                                            OperatorName = "Operator 1",
                                                                                                                            SalesValue = 100
                                                                                                                        },
                                                                                             new TopBottomOperatorDataModel(){
                                                                                                                            OperatorName = "Operator 2",
                                                                                                                            SalesValue = 200
                                                                                                                        }
                                                                                         };
            var result = ViewModelFactory.ConvertFrom(model);
            result.ShouldNotBeNull();
            result.Operators.Count.ShouldBe(model.Count);
            foreach (TopBottomOperatorDataModel topBottomOperatorData in model)
            {
                var d = result.Operators.SingleOrDefault(r => r.OperatorName == topBottomOperatorData.OperatorName);
                d.ShouldNotBeNull();
                d.SalesValue.ShouldBe(topBottomOperatorData.SalesValue);
            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomOperatorDataModel_NullModel_ErrorThrown()
        {
            List<TopBottomOperatorDataModel> model = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomOperatorDataModel_EmptyModel_ErrorThrown()
        {
            List<TopBottomOperatorDataModel> model = new List<TopBottomOperatorDataModel>();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomMerchantDataModel_IsConverted()
        {
            List<TopBottomMerchantDataModel> model = new List<TopBottomMerchantDataModel>{
                                                                                             new TopBottomMerchantDataModel(){
                                                                                                                                 MerchantName = "Merchant 1",
                                                                                                                                 SalesValue = 100
                                                                                                                             },
                                                                                             new TopBottomMerchantDataModel(){
                                                                                                                                 MerchantName = "Merchant 2",
                                                                                                                                 SalesValue = 200
                                                                                                                             }
                                                                                         };
            var result = ViewModelFactory.ConvertFrom(model);
            result.ShouldNotBeNull();
            result.Merchants.Count.ShouldBe(model.Count);
            foreach (TopBottomMerchantDataModel topBottomOperatorData in model)
            {
                var d = result.Merchants.SingleOrDefault(r => r.MerchantName == topBottomOperatorData.MerchantName);
                d.ShouldNotBeNull();
                d.SalesValue.ShouldBe(topBottomOperatorData.SalesValue);
            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomMerchantDataModel_NullModel_ErrorThrown()
        {
            List<TopBottomMerchantDataModel> model = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomMerchantDataModel_EmptyModel_ErrorThrown()
        {
            List<TopBottomMerchantDataModel> model = new List<TopBottomMerchantDataModel>();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomProductData_IsConverted()
        {
            List<TopBottomProductDataModel> model = new List<TopBottomProductDataModel>{
                                                                                           new TopBottomProductDataModel(){
                                                                                                                              ProductName = "Product 1",
                                                                                                                              SalesValue = 100
                                                                                                                          },
                                                                                           new TopBottomProductDataModel(){
                                                                                                                              ProductName = "Product 2",
                                                                                                                              SalesValue = 200
                                                                                                                          }
                                                                                       };
            var result = ViewModelFactory.ConvertFrom(model);
            result.ShouldNotBeNull();
            result.Products.Count.ShouldBe(model.Count);
            foreach (TopBottomProductDataModel topBottomOperatorData in model)
            {
                var d = result.Products.SingleOrDefault(r => r.ProductName == topBottomOperatorData.ProductName);
                d.ShouldNotBeNull();
                d.SalesValue.ShouldBe(topBottomOperatorData.SalesValue);
            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomProductDataModel_NullModel_ErrorThrown()
        {
            List<TopBottomProductDataModel> model = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TopBottomProductDataModel_EmptyModel_ErrorThrown()
        {
            List<TopBottomProductDataModel> model = new List<TopBottomProductDataModel>();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }


        [Fact]
        public void ViewModelFactory_ConvertFrom_ComparisonDate_IsConverted()
        {
            List<ComparisonDateModel> model = new List<ComparisonDateModel>{
                                                                               new ComparisonDateModel{
                                                                                                          Date = new DateTime(2023,09,22),
                                                                                                          Description = "Yesterday",
                                                                                                          OrderValue = 1
                                                                                                      },
                                                                               new ComparisonDateModel(){
                                                                                                            Date = new DateTime(2023,08,22),
                                                                                                            Description = "Last Month",
                                                                                                            OrderValue = 2
                                                                                                        }
                                                                           };
            var result = ViewModelFactory.ConvertFrom(model);

            result.Count.ShouldBe(model.Count);
            foreach (var comparisonDate in model)
            {
                var d = result.SingleOrDefault(r => r.text == comparisonDate.Description);
                d.value.ShouldBe(comparisonDate.Date.ToString("yyyy-MM-dd"));
            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ComparisonDate_NullModel_ErrorThrown(){
            List<ComparisonDateModel> model = null;
            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ComparisonDate_EmptyModel_ErrorThrown()
        {
            List<ComparisonDateModel> model = new List<ComparisonDateModel>();
            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TodaysSalesValueByHourModel_IsConverted()
        {
            List<TodaysSalesValueByHourModel> models = new List<TodaysSalesValueByHourModel>();
            models.Add(new TodaysSalesValueByHourModel
            {
                           ComparisonSalesValue = 100,
                           Hour = 1,
                           TodaysSalesValue = 101
                       });
            models.Add(new TodaysSalesValueByHourModel
            {
                           ComparisonSalesValue = 200,
                           Hour = 2,
                           TodaysSalesValue = 202
                       });

            var result = ViewModelFactory.ConvertFrom(models);

            result.Count.ShouldBe(models.Count);
            foreach (var todaysSalesValueByHour in models)
            {
                var d = result.SingleOrDefault(r => r.Hour == todaysSalesValueByHour.Hour);
                d.ShouldNotBeNull();
                d.ComparisonValue.ShouldBe(todaysSalesValueByHour.ComparisonSalesValue);
                d.TodaysValue.ShouldBe(todaysSalesValueByHour.TodaysSalesValue);

            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TodaysSalesValueByHourModel_ModelIsNull_ErrorThrown()
        {
            List<TodaysSalesValueByHourModel> models = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(models);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TodaysSalesValueByHourModel_ModelIsEmpty_ErrorThrown()
        {
            List<TodaysSalesValueByHourModel> models = new List<TodaysSalesValueByHourModel>();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(models);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TodaysSalesCountByHourModel_IsConverted()
        {
            List<TodaysSalesCountByHourModel> models = new List<TodaysSalesCountByHourModel>();
            models.Add(new TodaysSalesCountByHourModel
            {
                ComparisonSalesCount = 100,
                           Hour = 1,
                           TodaysSalesCount = 101
                       });
            models.Add(new TodaysSalesCountByHourModel
            {
                ComparisonSalesCount = 200,
                           Hour = 2,
                           TodaysSalesCount = 202
                       });

            var result = ViewModelFactory.ConvertFrom(models);

            result.Count.ShouldBe(models.Count);
            foreach (var todaysSalesValueByHour in models)
            {
                var d = result.SingleOrDefault(r => r.Hour == todaysSalesValueByHour.Hour);
                d.ShouldNotBeNull();
                d.ComparisonCount.ShouldBe(todaysSalesValueByHour.ComparisonSalesCount);
                d.TodaysCount.ShouldBe(todaysSalesValueByHour.TodaysSalesCount);
            }
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TodaysSalesCountByHourModel_ModelIsNull_ErrorThrown()
        {
            List<TodaysSalesCountByHourModel> models = null;

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(models);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TodaysSalesCountByHourModel_ModelIsEmpty_ErrorThrown()
        {
            List<TodaysSalesCountByHourModel> models = new List<TodaysSalesCountByHourModel>();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    ViewModelFactory.ConvertFrom(models);
                                                });
        }
    }
}
