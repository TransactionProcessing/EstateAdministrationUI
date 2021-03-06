﻿namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TransactionsByDateViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction date view models.
        /// </summary>
        /// <value>
        /// The transaction date view models.
        /// </value>
        public List<TransactionDateViewModel> TransactionDateViewModels { get; set; }

        #endregion
    }
}