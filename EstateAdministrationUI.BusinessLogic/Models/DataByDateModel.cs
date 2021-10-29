namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DataByDateModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction date models.
        /// </summary>
        /// <value>
        /// The transaction date models.
        /// </value>
        public List<DataDateModel> DataDateModels { get; set; }

        #endregion
    }
}