namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DataByWeekModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week models.
        /// </summary>
        /// <value>
        /// The transaction week models.
        /// </value>
        public List<DataWeekModel> DataWeekModels { get; set; }

        #endregion
    }
}