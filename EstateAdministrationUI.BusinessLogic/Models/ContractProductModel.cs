namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class ContractProductModel
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
        /// Gets or sets the contract product identifier.
        /// </summary>
        /// <value>
        /// The contract product identifier.
        /// </value>
        public Guid ContractProductId { get; set; }

        /// <summary>
        /// Gets or sets the contract product transaction fees.
        /// </summary>
        /// <value>
        /// The contract product transaction fees.
        /// </value>
        public List<ContractProductTransactionFeeModel> ContractProductTransactionFees { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public String DisplayText { get; set; }

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; set; }

        /// <summary>
        /// Gets or sets the number of transaction fees.
        /// </summary>
        /// <value>
        /// The number of transaction fees.
        /// </value>
        public Int32 NumberOfTransactionFees { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public String ProductName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Decimal? Value { get; set; }

        #endregion
    }
}