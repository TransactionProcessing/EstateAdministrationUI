namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class MerchantBalanceHistoryListViewModel
    {
        /// <summary>
        /// Gets or sets the merchant balance history view models.
        /// </summary>
        /// <value>
        /// The merchant balance history view models.
        /// </value>
        public List<MerchantBalanceHistoryViewModel> MerchantBalanceHistoryViewModels { get; set; }
    }
}