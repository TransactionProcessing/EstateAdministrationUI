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
        /// <param name="createMerchantViewModel">The create merchant view model.</param>
        /// <returns></returns>
        CreateMerchantModel ConvertFrom(CreateMerchantViewModel createMerchantViewModel);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantModels">The merchant models.</param>
        /// <returns></returns>
        List<MerchantListViewModel> ConvertFrom(List<MerchantModel> merchantModels);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="merchantModel">The merchant model.</param>
        /// <returns></returns>
        MerchantViewModel ConvertFrom(MerchantModel merchantModel);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="makeMerchantDepositViewModel">The make merchant deposit view model.</param>
        /// <returns></returns>
        MakeMerchantDepositModel ConvertFrom(MakeMerchantDepositViewModel makeMerchantDepositViewModel);

        #endregion
    }
}