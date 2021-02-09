namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionsByOperatorViewModel
    {
        #region Properties

        public List<TransactionOperatorViewModel> TransactionOperatorViewModels { get; set; }

        #endregion
    }
}