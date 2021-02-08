namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionsByMerchantViewModel
    {
        #region Properties
        
        public List<TransactionMerchantViewModel> TransactionMerchantViewModels { get; set; }

        #endregion
    }
}