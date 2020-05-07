namespace EstateAdministrationUI.BusinessLogic.Factories
{
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

        #endregion
    }
}