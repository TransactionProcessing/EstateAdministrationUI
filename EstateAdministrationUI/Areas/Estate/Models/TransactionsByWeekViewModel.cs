namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TransactionsByWeekViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week view models.
        /// </summary>
        /// <value>
        /// The transaction week view models.
        /// </value>
        public List<TransactionWeekViewModel> TransactionWeekViewModels { get; set; }

        #endregion
    }
}