namespace EstateAdministrationUI.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Areas.Estate.Models;
    using BusinessLogic.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    public class ViewModelFactory : IViewModelFactory
    {
        public EstateViewModel ConvertFrom(EstateModel estateModel)
        {
            if (estateModel == null)
            {
                throw  new ArgumentNullException(nameof(estateModel));
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
                                   AddressLine1 = merchantModel.Addresses.FirstOrDefault() == null ? String.Empty : merchantModel.Addresses.First().AddressLine1,
                                   MerchantId = merchantModel.MerchantId,
                                   ContactName = merchantModel.Contacts.FirstOrDefault() == null ? String.Empty : merchantModel.Contacts.First().ContactName,
                                   Town = merchantModel.Addresses.FirstOrDefault() == null ? String.Empty : merchantModel.Addresses.First().Town,
                                   MerchantName = merchantModel.MerchantName,
                                   EstateId = merchantModel.EstateId,
                                   NumberOfDevices = merchantModel.Devices != null && merchantModel.Devices.Any() ? merchantModel.Devices.Count : 0,
                                   NumberOfOperators = merchantModel.Operators.Any() ? merchantModel.Operators.Count : 0,
                                   NumberOfUsers = 0
                               });
            }

            return viewModels;
        }
    }
}