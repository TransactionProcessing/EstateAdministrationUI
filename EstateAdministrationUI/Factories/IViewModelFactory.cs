namespace EstateAdministrationUI.Factories
{
    using System.Collections.Generic;
    using Areas.Estate.Models;
    using BusinessLogic.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IViewModelFactory
    {
        #region Methods

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="estateModel">The estate model.</param>
        /// <returns></returns>
        EstateViewModel ConvertFrom(EstateModel estateModel);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantModels">The merchant models.</param>
        /// <returns></returns>
        List<MerchantListViewModel> ConvertFrom(List<MerchantModel> merchantModels);

        #endregion
    }
}