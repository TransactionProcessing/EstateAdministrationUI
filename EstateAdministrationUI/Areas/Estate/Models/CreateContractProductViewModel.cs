namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateContractProductViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the contract identifier.
        /// </summary>
        /// <value>
        /// The contract identifier.
        /// </value>
        public Guid ContractId { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public String DisplayText { get; set; }

        public String ProductType { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public String ProductName { get; set; }

        /// <summary>
        /// Gets or sets the transaction fees.
        /// </summary>
        /// <value>
        /// The transaction fees.
        /// </value>
        public List<CreateContractProductTransactionFeeViewModel> TransactionFees { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is variable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is variable; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsVariable { get; set; }

        #endregion
    }
}