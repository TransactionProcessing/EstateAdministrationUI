namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TransactionsByMonthViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month view models.
        /// </summary>
        /// <value>
        /// The transaction month view models.
        /// </value>
        public List<TransactionMonthViewModel> TransactionMonthViewModels { get; set; }

        #endregion
    }
}