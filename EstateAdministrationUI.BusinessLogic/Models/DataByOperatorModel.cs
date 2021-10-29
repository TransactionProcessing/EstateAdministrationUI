namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DataByOperatorModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction operator models.
        /// </summary>
        /// <value>
        /// The transaction operator models.
        /// </value>
        public List<DataOperatorModel> DataOperatorModels { get; set; }

        #endregion
    }
}