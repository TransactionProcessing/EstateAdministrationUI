namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionsByMerchantModel
    {
        #region Properties

        public List<TransactionMerchantModel> TransactionMerchantModels { get; set; }

        #endregion
    }
}