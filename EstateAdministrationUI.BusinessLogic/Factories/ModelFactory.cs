namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System;
    using System.Collections.Generic;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using Microsoft.EntityFrameworkCore.Internal;
    using Models;

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
                                              NumberOfProducts = source.Products?.Count ?? 0
                                          };

            return contractModel;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
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
            if (estateResponseOperators == null || estateResponseOperators.Any()==false)
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