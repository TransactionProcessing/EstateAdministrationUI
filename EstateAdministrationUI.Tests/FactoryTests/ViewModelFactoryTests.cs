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

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateOperatorViewModel_ModelIsConverted()
        {
            CreateOperatorViewModel viewModel = TestData.CreateOperatorViewModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            CreateOperatorModel model = viewModelFactory.ConvertFrom(viewModel);

            model.OperatorName.ShouldBe(viewModel.OperatorName);
            model.RequireCustomTerminalNumber.ShouldBe(viewModel.RequireCustomTerminalNumber);
            model.RequireCustomMerchantNumber.ShouldBe(viewModel.RequireCustomMerchantNumber);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateOperatorViewModel_NullModel_ErrorThrown()
        {
            CreateOperatorViewModel viewModel = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(viewModel); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModel_ModelIsConverted()
        {
            EstateOperatorModel model = TestData.EstateOperatorModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            OperatorListViewModel viewModel = viewModelFactory.ConvertFrom(TestData.EstateId, model);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() => { viewModelFactory.ConvertFrom(TestData.EstateId, model); });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModelList_ModelIsConverted()
        {
            List<EstateOperatorModel> modelList = new List<EstateOperatorModel>
                                                  {
                                                      TestData.EstateOperatorModel
                                                  };

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<OperatorListViewModel> viewModelList = viewModelFactory.ConvertFrom(TestData.EstateId, modelList);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(TestData.EstateId, modelList);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_EstateOperatorModelList_NullList_ErrorThrown()
        {
            List<EstateOperatorModel> modelList = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(TestData.EstateId, modelList);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractViewModel_IsConverted()
        {
            CreateContractViewModel viewModel = TestData.CreateContractViewModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            CreateContractModel model = viewModelFactory.ConvertFrom(viewModel);

            model.OperatorId.ShouldBe(viewModel.OperatorId);
            model.Description.ShouldBe(viewModel.ContractDescription);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractViewModel_NullModel_ErrorThrown()
        {
            CreateContractViewModel viewModel = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_IsConverted()
        {
            ContractModel model = TestData.ContractModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductListViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.Count.ShouldBe(model.ContractProducts.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_NullProducts_IsConverted()
        {
            ContractModel model = TestData.ContractModelNullProducts;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductListViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_EmptyProducts_IsConverted()
        {
            ContractModel model = TestData.ContractModelEmptyProducts;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductListViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_ProductWithNullValue_IsConverted()
        {
            ContractModel model = TestData.ContractModelWithNullValueProduct;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductListViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.ContractId.ShouldBe(model.ContractId);
            viewModel.Description.ShouldBe(model.Description);
            viewModel.ContractProducts.Count.ShouldBe(model.ContractProducts.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModel_NullModel_ErrorThrown()
        {
            ContractModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractProductModel_IsConverted()
        {
            ContractProductModel model = TestData.ContractProductModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductTransactionFeesListViewModel viewModel = viewModelFactory.ConvertFrom(model);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductTransactionFeesListViewModel viewModel = viewModelFactory.ConvertFrom(model);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductTransactionFeesListViewModel viewModel = viewModelFactory.ConvertFrom(model);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            ContractProductTransactionFeesListViewModel viewModel = viewModelFactory.ConvertFrom(model);

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

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModelList_IsConverted()
        {
            List<ContractModel> modelList = new List<ContractModel>
                                            {
                                                TestData.ContractModel
                                            };

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            List<ContractListViewModel> viewModels = viewModelFactory.ConvertFrom(modelList);

            viewModels.ShouldHaveSingleItem();
            ContractListViewModel viewModel = viewModels.Single();
            viewModel.ContractId.ShouldBe(TestData.ContractModel.ContractId);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_ContractModelList_NullList_ErrorThrown()
        {
            List<ContractModel> modelList = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(modelList);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductViewModel_WithValue_IsConverted()
        {
            CreateContractProductViewModel viewModel = TestData.CreateContractProductViewModelWithValue;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            AddProductToContractModel model = viewModelFactory.ConvertFrom(viewModel);

            model.Value.ShouldBe(viewModel.Value);
            model.DisplayText.ShouldBe(viewModel.DisplayText);
            model.ProductName.ShouldBe(viewModel.ProductName);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductViewModel_WithNullValue_IsConverted()
        {
            CreateContractProductViewModel viewModel = TestData.CreateContractProductViewModelWithNullValue;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            AddProductToContractModel model = viewModelFactory.ConvertFrom(viewModel);

            model.Value.ShouldBeNull();
            model.DisplayText.ShouldBe(viewModel.DisplayText);
            model.ProductName.ShouldBe(viewModel.ProductName);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductViewModel_NullViewModel_IsConverted()
        {
            CreateContractProductViewModel viewModel = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductTransactionFeeViewModel_IsConverted()
        {
            CreateContractProductTransactionFeeViewModel viewModel = TestData.CreateContractProductTransactionFeeViewModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            AddTransactionFeeToContractProductModel model = viewModelFactory.ConvertFrom(viewModel);

            model.Value.ShouldBe(viewModel.Value);
            model.CalculationType.ShouldBe((CalculationType)(viewModel.CalculationType -1));
            model.Description.ShouldBe(viewModel.FeeDescription);
            model.FeeType.ShouldBe((FeeType)(viewModel.FeeType - 1));
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_CreateContractProductTransactionFeeViewModel_NullModel_ErrorThrown()
        {
            CreateContractProductTransactionFeeViewModel viewModel = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    viewModelFactory.ConvertFrom(viewModel);
                                                });
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionForPeriodModel_IsConverted()
        {
            TransactionForPeriodModel model = TestData.TransactionForPeriodModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionPeriodViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.NumberOfTransactions.ShouldBe(model.NumberOfTransactions);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionForPeriodModel_NullModel_IsConverted()
        {
            TransactionForPeriodModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionPeriodViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.ShouldNotBeNull();
            viewModel.NumberOfTransactions.ShouldBe(0);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByDateModel_IsConverted()
        {
            TransactionsByDateModel model = TestData.TransactionsByDateModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByDateViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionDateViewModels.Count.ShouldBe(model.TransactionDateModels.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByDateModel_ModelIsNull_IsConverted()
        {
            TransactionsByDateModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByDateViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionDateViewModels.ShouldBeNull();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByWeekModel_IsConverted()
        {
            TransactionsByWeekModel model = TestData.TransactionsByWeekModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByWeekViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionWeekViewModels.Count.ShouldBe(model.TransactionWeekModels.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByWeekModel_ModelIsNull_IsConverted()
        {
            TransactionsByWeekModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByWeekViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionWeekViewModels.ShouldBeNull();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByMonthModel_IsConverted()
        {
            TransactionsByMonthModel model = TestData.TransactionsByMonthModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByMonthViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionMonthViewModels.Count.ShouldBe(model.TransactionMonthModels.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByMonthModel_ModelIsNull_IsConverted()
        {
            TransactionsByMonthModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByMonthViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionMonthViewModels.ShouldBeNull();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByMerchantModel_IsConverted()
        {
            TransactionsByMerchantModel model = TestData.TransactionsByMerchantModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByMerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionMerchantViewModels.Count.ShouldBe(model.TransactionMerchantModels.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByMerchantModel_ModelIsNull_IsConverted()
        {
            TransactionsByMerchantModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByMerchantViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionMerchantViewModels.ShouldBeNull();
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByOperatorModel_IsConverted()
        {
            TransactionsByOperatorModel model = TestData.TransactionsByOperatorModel;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByOperatorViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionOperatorViewModels.Count.ShouldBe(model.TransactionOperatorModels.Count);
        }

        [Fact]
        public void ViewModelFactory_ConvertFrom_TransactionsByOperatorModel_ModelIsNull_IsConverted()
        {
            TransactionsByOperatorModel model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            TransactionsByOperatorViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.TransactionOperatorViewModels.ShouldBeNull();
        }

        [Fact]
        public void ViewModelFactory_CovertFrom_MerchantBalanceHistoryModel_IsConverted()
        {
            List<MerchantBalanceHistory> model = TestData.MerchantBalanceHistoryList;

            ViewModelFactory viewModelFactory = new ViewModelFactory();

            MerchantBalanceHistoryListViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.MerchantBalanceHistoryViewModels.Count.ShouldBe(model.Count);
        }

        [Fact]
        public void ViewModelFactory_CovertFrom_MerchantBalanceHistoryModel_ModelIsNull_IsConverted()
        {
            List<MerchantBalanceHistory> model = null;

            ViewModelFactory viewModelFactory = new ViewModelFactory();
            
            MerchantBalanceHistoryListViewModel viewModel = viewModelFactory.ConvertFrom(model);

            viewModel.MerchantBalanceHistoryViewModels.ShouldBeNull();
        }
    }
}
