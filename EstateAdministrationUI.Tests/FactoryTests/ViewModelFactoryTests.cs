using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.Tests.FactoryTests
{
    using System.Linq;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using Factories;
    using Shouldly;
    using Testing;
    using Xunit;

    public class ViewModelFactoryTests
    {
        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateModel_ModelIsConverted()
        {
            EstateModel model = TestData.EstateModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            EstateViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.EstateName.ShouldBe(model.EstateName);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateModel_NullModel_ErrorThrown()
        {
            EstateModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateMerchantViewModel_ModelIsConverted()
        {
            CreateMerchantViewModel viewModel = TestData.CreateMerchantViewModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            CreateMerchantModel model = viewModelFactory.ConvertFrom(viewModel);

            model.MerchantName.ShouldBe(viewModel.MerchantName);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(viewModel); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModel_ModelIsConverted()
        {
            MerchantModel model = TestData.MerchantModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Operators = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Operators = new List<MerchantOperatorModel>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Devices = null;
            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Devices = new Dictionary<Guid, String>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Addresses = null;
            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Addresses = new List<AddressModel>();
            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Contacts = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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
            MerchantModel model = TestData.MerchantModel;
            model.Contacts = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.Balance.ShouldBe(model.Balance);
            viewModel.MerchantId.ShouldBe(model.MerchantId);
            viewModel.MerchantName.ShouldBe(model.MerchantName);
            viewModel.EstateId.ShouldBe(model.EstateId);
            viewModel.AvailableBalance.ShouldBe(model.AvailableBalance);
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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_ModelIsConverted()
        {
            List<MerchantModel> modelList = new List<MerchantModel>
                                            {
                                                TestData.MerchantModel
                                            };

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Addresses[0] = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Addresses = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Addresses = new List<AddressModel>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Contacts[0] = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Contacts = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Contacts = new List<ContactModel>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Devices = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Devices = new Dictionary<Guid, String>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Operators[0] = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Operators = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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
                                                TestData.MerchantModel
                                            };
            modelList.First().Operators = new List<MerchantOperatorModel>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<MerchantListViewModel> viewModelList = viewModelFactory.ConvertFrom(modelList);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(modelList); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MerchantModelList_EmptyModel_ErrorThrown()
        {
            List<MerchantModel> modelList = new List<MerchantModel>();

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(modelList); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MakeMerchantDepositViewModel_ModelIsConverted()
        {
            MakeMerchantDepositViewModel viewModel = TestData.MakeMerchantDepositViewModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MakeMerchantDepositModel model = viewModelFactory.ConvertFrom(viewModel);

            model.MerchantId.ShouldBe(Guid.Parse(viewModel.MerchantId));
            model.DepositDateTime.ShouldBe(DateTime.ParseExact(viewModel.DepositDate, "dd/MM/yyyy", null));
            model.Reference.ShouldBe(viewModel.Reference);
            model.Amount.ShouldBe(Decimal.Parse(viewModel.Amount));
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_MakeMerchantDepositViewModel_NullModel_ErrorThrown()
        {
            MakeMerchantDepositViewModel viewModel = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(viewModel); });
        }
    }
}
