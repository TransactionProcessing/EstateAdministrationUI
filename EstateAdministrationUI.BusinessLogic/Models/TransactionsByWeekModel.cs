namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TransactionsByWeekModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week models.
        /// </summary>
        /// <value>
        /// The transaction week models.
        /// </value>
        public List<TransactionWeekModel> TransactionWeekModels { get; set; }

        #endregion
    }
}