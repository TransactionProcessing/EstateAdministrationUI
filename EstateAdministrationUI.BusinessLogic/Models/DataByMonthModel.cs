namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DataByMonthModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month models.
        /// </summary>
        /// <value>
        /// The transaction month models.
        /// </value>
        public List<DataMonthModel> DataMonthModels { get; set; }

        #endregion
    }
}