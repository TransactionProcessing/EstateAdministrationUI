namespace EstateAdministrationUI.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateAdministrationUI.Factories.IViewModelFactory" />
    public class ViewModelFactory : IViewModelFactory
    {
        #region Methods

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
        /// <param name="merchantModels">The merchant models.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">merchantModels</exception>
        public List<MerchantListViewModel> ConvertFrom(List<MerchantModel> merchantModels)
        {
            if (merchantModels == null || EnumerableExtensions.Any(merchantModels) == false)
            {
                throw new ArgumentNullException(nameof(merchantModels));
            }

            List<MerchantListViewModel> viewModels = new List<MerchantListViewModel>();

            foreach (MerchantModel merchantModel in merchantModels)
            {
                viewModels.Add(new MerchantListViewModel
                               {
                                   AddressLine1 = merchantModel.Addresses.FirstOrDefault() == null ? string.Empty : merchantModel.Addresses.First().AddressLine1,
                                   MerchantId = merchantModel.MerchantId,
                                   ContactName = merchantModel.Contacts.FirstOrDefault() == null ? string.Empty : merchantModel.Contacts.First().ContactName,
                                   Town = merchantModel.Addresses.FirstOrDefault() == null ? string.Empty : merchantModel.Addresses.First().Town,
                                   MerchantName = merchantModel.MerchantName,
                                   EstateId = merchantModel.EstateId,
                                   NumberOfDevices = merchantModel.Devices != null && merchantModel.Devices.Any() ? merchantModel.Devices.Count : 0,
                                   NumberOfOperators = merchantModel.Operators.Any() ? merchantModel.Operators.Count : 0,
                                   NumberOfUsers = 0
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
            viewModel.Addresses = this.ConvertFrom(merchantModel.Addresses);
            viewModel.Contacts = this.ConvertFrom(merchantModel.Contacts);
            viewModel.Operators = this.ConvertFrom(merchantModel.Operators);
            viewModel.Devices = this.ConvertFrom(merchantModel.Devices);

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

            if (deviceModels == null)
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

            if (addressModels == null)
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

            if (operatorModels == null)
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