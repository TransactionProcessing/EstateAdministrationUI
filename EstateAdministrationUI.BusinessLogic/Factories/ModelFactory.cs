namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System;
    using System.Collections.Generic;
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
        public List<MerchantModel> ConvertFrom(List<MerchantResponse> source)
        {
            if (source == null || source.Any() == false)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<MerchantModel> models = new List<MerchantModel>();

            foreach (MerchantResponse merchantResponse in source)
            {
                MerchantModel merchantModel = new MerchantModel
                                              {
                                                  EstateId = merchantResponse.EstateId,
                                                  MerchantId = merchantResponse.MerchantId,
                                                  MerchantName = merchantResponse.MerchantName,
                                              };

                if (merchantResponse.Addresses != null && merchantResponse.Addresses.Any())
                {
                    merchantModel.Addresses = new List<AddressModel>();
                    merchantResponse.Addresses.ForEach(a => merchantModel.Addresses.Add(new AddressModel
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

                if (merchantResponse.Contacts != null && merchantResponse.Contacts.Any())
                {
                    merchantModel.Contacts = new List<ContactModel>();
                    merchantResponse.Contacts.ForEach(c => merchantModel.Contacts.Add(new ContactModel
                                                                                      {
                                                                                          ContactEmailAddress = c.ContactEmailAddress,
                                                                                          ContactId = c.ContactId,
                                                                                          ContactName = c.ContactName,
                                                                                          ContactPhoneNumber = c.ContactPhoneNumber
                                                                                      }));
                }

                if (merchantResponse.Operators != null && merchantResponse.Operators.Any())
                {
                    merchantModel.Operators = new List<MerchantOperatorModel>();
                    merchantResponse.Operators.ForEach(o => merchantModel.Operators.Add(new MerchantOperatorModel
                                                                                        {
                                                                                            MerchantNumber = o.MerchantNumber,
                                                                                            Name = o.Name,
                                                                                            OperatorId = o.OperatorId,
                                                                                            TerminalNumber = o.TerminalNumber
                                                                                        }));
                }

                if (merchantResponse.Devices != null && merchantResponse.Devices.Any())
                {
                    merchantModel.Devices = new Dictionary<Guid, String>();
                    foreach (KeyValuePair<Guid, String> merchantResponseDevice in merchantResponse.Devices)
                    {
                        merchantModel.Devices.Add(merchantResponseDevice.Key, merchantResponseDevice.Value);
                    }
                }

                models.Add(merchantModel);
            }

            return models;
        }

        /// <summary>
        /// Converts the operators.
        /// </summary>
        /// <param name="estateResponseOperators">The estate response operators.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">estateResponseOperators</exception>
        private List<EstateOperatorModel> ConvertOperators(List<EstateOperatorResponse> estateResponseOperators)
        {
            if (estateResponseOperators == null)
            {
                throw new ArgumentNullException(nameof(estateResponseOperators));
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
            if (estateResponseSecurityUsers == null)
            {
                throw new ArgumentNullException(nameof(estateResponseSecurityUsers));
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