using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Factories
{
    using Areas.Estate.Models;
    using BusinessLogic.Models;

    public interface IViewModelFactory
    {
        EstateViewModel ConvertFrom(EstateModel estateModel);
    }

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
    }
}
