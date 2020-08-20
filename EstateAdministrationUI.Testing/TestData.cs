namespace EstateAdministrationUI.Testing
{
    using System;
    using System.Collections.Generic;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using EstateManagement.DataTransferObjects.Responses;

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

        public static EstateModel EstateModel => new EstateModel
                                                {
                                                    EstateId = TestData.EstateId,
                                                    EstateName = TestData.EstateName,
                                                    Operators = TestData.Operators,
                                                    SecurityUsers = TestData.SecurityUsers
                                                };
        
        public static List<EstateOperatorModel> Operators => new List<EstateOperatorModel>
                                                                      {
                                                                          new EstateOperatorModel
                                                                          {
                                                                              Name = TestData.OperatorName,
                                                                              OperatorId = TestData.OperatorId,
                                                                              RequireCustomMerchantNumber = TestData.RequireCustomMerchantNumber,
                                                                              RequireCustomTerminalNumber = TestData.RequireCustomTerminalNumber
                                                                          }
                                                                      };
        
        public static List<SecurityUserModel> SecurityUsers => new List<SecurityUserModel>
                                                              {
                                                                  new SecurityUserModel
                                                                  {
                                                                      EmailAddress = TestData.EmailAddress,
                                                                      SecurityUserId = TestData.SecurityUserId
                                                                  }
                                                              };

        public static CreateMerchantViewModel CreateMerchantViewModel => new CreateMerchantViewModel
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

        public static MerchantModel MerchantModel => new MerchantModel
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

        public static String DepositReference = "Test Deposit";

        public static String DepositDate = "24/06/2020";

        public static Guid DepositId = Guid.Parse("895EE043-26E3-49E1-8255-2A25AAF070B7");

        public static MakeMerchantDepositModel MakeMerchantDepositModel =>
            new MakeMerchantDepositModel
            {
                MerchantId = TestData.MerchantId,
                DepositDateTime = DateTime.ParseExact(TestData.DepositDate, "dd/MM/yyyy",null),
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
    }
}