namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System.Collections.Generic;
    using EstateManagement.DataTransferObjects.Responses;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IModelFactory
    {
        #region Methods

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        EstateModel ConvertFrom(EstateResponse source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        List<MerchantModel> ConvertFrom(List<MerchantResponse> source);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        MerchantModel ConvertFrom(MerchantResponse source);

        #endregion
    }
}