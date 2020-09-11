using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.BusinessLogic.Tests.FactoryTests
{
    using System.Linq;
    using Areas.Estate.Models;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using Factories;
    using Models;
    using Shouldly;
    using Testing;
    using Xunit;

    public class ModelFactoryTests
    {
        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantModel_ModelIsConverted()
        {
            CreateMerchantModel model = TestData.CreateMerchantModel;

            ModelFactory modelFactory = new ModelFactory();

            CreateMerchantRequest request =  modelFactory.ConvertFrom(model);

            request.Contact.ShouldNotBeNull();
            request.Contact.EmailAddress.ShouldBe(model.Contact.ContactEmailAddress);
            request.Contact.PhoneNumber.ShouldBe(model.Contact.ContactPhoneNumber);
            request.Contact.ContactName.ShouldBe(model.Contact.ContactName);

            request.Address.ShouldNotBeNull();
            request.Address.AddressLine1.ShouldBe(model.Address.AddressLine1);
            request.Address.AddressLine2.ShouldBe(model.Address.AddressLine2);
            request.Address.AddressLine3.ShouldBe(model.Address.AddressLine3);
            request.Address.AddressLine4.ShouldBe(model.Address.AddressLine4);
            request.Address.Town.ShouldBe(model.Address.Town);
            request.Address.Region.ShouldBe(model.Address.Region);
            request.Address.Country.ShouldBe(model.Address.Country);
            request.Address.PostalCode.ShouldBe(model.Address.PostalCode);

            request.Name.ShouldBe(model.MerchantName);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantModel_NullModel_ErrorThrown()
        {
            CreateMerchantModel model = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositModel_ModelIsConverted()
        {
            MakeMerchantDepositModel model = TestData.MakeMerchantDepositModel;

            ModelFactory modelFactory = new ModelFactory();

            MakeMerchantDepositRequest request = modelFactory.ConvertFrom(model);

            request.Reference.ShouldBe(model.Reference);
            request.DepositDateTime.ShouldBe(model.DepositDateTime);
            request.Source.ShouldBe(MerchantDepositSource.Manual);
            request.Amount.ShouldBe(model.Amount);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositModel_NullModel_ErrorThrown()
        {
            MakeMerchantDepositModel model = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_ModelIsConverted()
        {
            EstateResponse response = TestData.EstateResponse;

            ModelFactory modelFactory = new ModelFactory();

            EstateModel model = modelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
                                       {
                                           EstateOperatorModel operatorModel = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           operatorModel.ShouldNotBeNull();
                                           operatorModel.RequireCustomMerchantNumber.ShouldBe(o.RequireCustomMerchantNumber);
                                           operatorModel.RequireCustomTerminalNumber.ShouldBe(o.RequireCustomTerminalNumber);
                                           operatorModel.Name.ShouldBe(o.Name);
                                       });
            response.SecurityUsers.ForEach(u =>
                                       {
                                           SecurityUserModel securityUserModel = model.SecurityUsers.SingleOrDefault(su => su.SecurityUserId == u.SecurityUserId);
                                           securityUserModel.ShouldNotBeNull();
                                           securityUserModel.EmailAddress.ShouldBe(u.EmailAddress);
                                       });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_NullOperators_ModelIsConverted()
        {
            EstateResponse response = TestData.EstateResponse;
            response.Operators = null;

            ModelFactory modelFactory = new ModelFactory();

            EstateModel model = modelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.ShouldBeNull();
            response.SecurityUsers.ForEach(u =>
                                           {
                                               SecurityUserModel securityUserModel = model.SecurityUsers.SingleOrDefault(su => su.SecurityUserId == u.SecurityUserId);
                                               securityUserModel.ShouldNotBeNull();
                                               securityUserModel.EmailAddress.ShouldBe(u.EmailAddress);
                                           });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_EmptyOperators_ModelIsConverted()
        {
            EstateResponse response = TestData.EstateResponse;
            response.Operators = new List<EstateOperatorResponse>();

            ModelFactory modelFactory = new ModelFactory();

            EstateModel model = modelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.ShouldBeNull();
            response.SecurityUsers.ForEach(u =>
                                           {
                                               SecurityUserModel securityUserModel = model.SecurityUsers.SingleOrDefault(su => su.SecurityUserId == u.SecurityUserId);
                                               securityUserModel.ShouldNotBeNull();
                                               securityUserModel.EmailAddress.ShouldBe(u.EmailAddress);
                                           });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_NullSecurityUsers_ModelIsConverted()
        {
            EstateResponse response = TestData.EstateResponse;
            response.SecurityUsers = null;

            ModelFactory modelFactory = new ModelFactory();

            EstateModel model = modelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
                                       {
                                           EstateOperatorModel operatorModel = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           operatorModel.ShouldNotBeNull();
                                           operatorModel.RequireCustomMerchantNumber.ShouldBe(o.RequireCustomMerchantNumber);
                                           operatorModel.RequireCustomTerminalNumber.ShouldBe(o.RequireCustomTerminalNumber);
                                           operatorModel.Name.ShouldBe(o.Name);
                                       });
            model.SecurityUsers.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_EmptySecurityUsers_ModelIsConverted()
        {
            EstateResponse response = TestData.EstateResponse;
            response.SecurityUsers = new List<SecurityUserResponse>(
                                                                    );
            ModelFactory modelFactory = new ModelFactory();

            EstateModel model = modelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
                                       {
                                           EstateOperatorModel operatorModel = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           operatorModel.ShouldNotBeNull();
                                           operatorModel.RequireCustomMerchantNumber.ShouldBe(o.RequireCustomMerchantNumber);
                                           operatorModel.RequireCustomTerminalNumber.ShouldBe(o.RequireCustomTerminalNumber);
                                           operatorModel.Name.ShouldBe(o.Name);
                                       });
            model.SecurityUsers.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_NullResponse_ErrorThrown()
        {
            EstateResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullAddress_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Addresses = null;

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyAddress_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Addresses = new List<AddressResponse>();

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullContacts_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Contacts = null;

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.ShouldBeNull();

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyContacts_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Contacts = new List<ContactResponse>();

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.ShouldBeNull();

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullOperators_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Operators = null;

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.ShouldBeNull();
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyOperators_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Operators = new List<MerchantOperatorResponse>();

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.ShouldBeNull();
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullDevices_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Devices = null;

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.ShouldBeNull();

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyDevices_ModelIsConverted()
        {
            MerchantResponse response = TestData.MerchantResponse;
            response.Devices = new Dictionary<Guid, String>();

            ModelFactory modelFactory = new ModelFactory();

            MerchantModel model = modelFactory.ConvertFrom(response);

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.ShouldBeNull();

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullResponse_ErrorThrown()
        {
            MerchantResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponseList_ModelIsConverted()
        {
            List<MerchantResponse> responseList = new List<MerchantResponse>
                                              {
                                                  TestData.MerchantResponse
                                              };

            ModelFactory modelFactory = new ModelFactory();

            List<MerchantModel> modelList = modelFactory.ConvertFrom(responseList);

            modelList.ShouldNotBeNull();
            modelList.ShouldNotBeEmpty();
            MerchantModel model = modelList.Single();
            MerchantResponse response = responseList.Single();

            model.Balance.ShouldBe(response.Balance);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c =>
            {
                // Find the contact in the view model
                ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                modelContact.ShouldNotBeNull();
                modelContact.ContactName.ShouldBe(c.ContactName);
                modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
            });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices)
            {
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }


            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o =>
            {
                // Find the operator in the view model
                MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                modelOperator.ShouldNotBeNull();
                modelOperator.Name.ShouldBe(o.Name);
                modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
            });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a =>
            {
                // Find the operator in the view model
                AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                modelAddress.ShouldNotBeNull();
                modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                modelAddress.Country.ShouldBe(a.Country);
                modelAddress.PostalCode.ShouldBe(a.PostalCode);
                modelAddress.Region.ShouldBe(a.Region);
                modelAddress.Town.ShouldBe(a.Town);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponseList_NullResponse_ErrorThrown()
        {
            List<MerchantResponse> responseList = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
            {
                modelFactory.ConvertFrom(responseList);
            });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantResponse_ModelIsConverted()
        {
            CreateMerchantResponse response = TestData.CreateMerchantResponse;

            ModelFactory modelFactory = new ModelFactory();

            CreateMerchantResponseModel model = modelFactory.ConvertFrom(response);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.EstateId.ShouldBe(response.EstateId);
            model.AddressId.ShouldBe(response.AddressId);
            model.ContactId.ShouldBe(response.ContactId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantResponse_NullResponse_ErrorThrown()
        {
            CreateMerchantResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositResponse_ModelIsConverted()
        {
            MakeMerchantDepositResponse response = TestData.MakeMerchantDepositResponse;

            ModelFactory modelFactory = new ModelFactory();

            MakeMerchantDepositResponseModel model = modelFactory.ConvertFrom(response);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.EstateId.ShouldBe(response.EstateId);
            model.DepositId.ShouldBe(response.DepositId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositResponse_NullResponse_ErrorThrown()
        {
            MakeMerchantDepositResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorModel_ModelIsConverted()
        {
            CreateOperatorModel model = TestData.CreateOperatorModel;

            ModelFactory modelFactory = new ModelFactory();

            CreateOperatorRequest request = modelFactory.ConvertFrom(model);

            request.RequireCustomTerminalNumber.ShouldBe(model.RequireCustomTerminalNumber);
            request.RequireCustomMerchantNumber.ShouldBe(model.RequireCustomMerchantNumber);
            request.Name.ShouldBe(model.OperatorName);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorModel_NullModel_ErrorThrown()
        {
            CreateOperatorModel model = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorResponse_ModelIsConverted()
        {
            CreateOperatorResponse response = TestData.CreateOperatorResponse;

            ModelFactory modelFactory = new ModelFactory();

            CreateOperatorResponseModel model = modelFactory.ConvertFrom(response);

            model.OperatorId.ShouldBe(response.OperatorId);
            model.EstateId.ShouldBe(response.EstateId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorResponse_NullResponse_ErrorThrown()
        {
            CreateOperatorResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractModel_IsConverted()
        {
            CreateContractModel model = TestData.CreateContractModel;

            ModelFactory modelFactory = new ModelFactory();

            CreateContractRequest request = modelFactory.ConvertFrom(model);

            request.Description.ShouldBe(model.Description);
            request.OperatorId.ShouldBe(model.OperatorId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractModel_NullModel_ErrorThrown()
        {
            CreateContractModel model = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(model);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractResponse_IsConverted()
        {
            CreateContractResponse response = TestData.CreateContractResponse;

            ModelFactory modelFactory = new ModelFactory();

            CreateContractResponseModel model = modelFactory.ConvertFrom(response);

            model.EstateId.ShouldBe(response.EstateId);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.ContractId.ShouldBe(response.ContractId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractResponse_NullResponse_ErrorThrown()
        {
            CreateContractResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_IsConverted()
        {
            ContractResponse response = TestData.ContractResponse;

            ModelFactory modelFactory = new ModelFactory();

            ContractModel model = modelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(response.Products.Count);
            model.ContractProducts.Count.ShouldBe(response.Products.Count);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_NullProducts_IsConverted()
        {
            ContractResponse response = TestData.ContractResponseNullProducts;

            ModelFactory modelFactory = new ModelFactory();

            ContractModel model = modelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(0);
            model.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_EmptyProducts_IsConverted()
        {
            ContractResponse response = TestData.ContractResponseEmptyProducts;

            ModelFactory modelFactory = new ModelFactory();

            ContractModel model = modelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(0);
            model.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_ProductWithNullFees_IsConverted()
        {
            ContractResponse response = TestData.ContractResponseProductWithNullFees;

            ModelFactory modelFactory = new ModelFactory();

            ContractModel model = modelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(response.Products.Count);
            model.ContractProducts.Count.ShouldBe(response.Products.Count);
            model.ContractProducts.Single().NumberOfTransactionFees.ShouldBe(0);
            model.ContractProducts.Single().ContractProductTransactionFees.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_ProductWithEmptyFees_IsConverted()
        {
            ContractResponse response = TestData.ContractResponseProductWithEmptyFees;

            ModelFactory modelFactory = new ModelFactory();

            ContractModel model = modelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(response.Products.Count);
            model.ContractProducts.Count.ShouldBe(response.Products.Count);
            model.ContractProducts.Single().NumberOfTransactionFees.ShouldBe(0);
            model.ContractProducts.Single().ContractProductTransactionFees.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_NullResponse_ErrorThrown()
        {
            ContractResponse response = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(response);
                                                });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponseList_IsConverted()
        {
            List<ContractResponse> responseList = new List<ContractResponse>
                                                  {
                                                      TestData.ContractResponse
                                                  };

            ModelFactory modelFactory = new ModelFactory();

            List<ContractModel> model = modelFactory.ConvertFrom(responseList);
            model.ShouldHaveSingleItem();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponseList_NullList_ErrorThrown()
        {
            List<ContractResponse> responseList = null;

            ModelFactory modelFactory = new ModelFactory();

            Should.Throw<ArgumentNullException>(() =>
                                                {
                                                    modelFactory.ConvertFrom(responseList);
                                                });
        }
    }
}
