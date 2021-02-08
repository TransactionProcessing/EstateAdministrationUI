namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionMerchantModel
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public String MerchantName { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the number of transactions.
        /// </summary>
        /// <value>
        /// The number of transactions.
        /// </value>
        public Int32 NumberOfTransactions { get; set; }

        /// <summary>
        /// Gets or sets the value of transactions.
        /// </summary>
        /// <value>
        /// The value of transactions.
        /// </value>
        public Decimal ValueOfTransactions { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public String CurrencyCode { get; set; }
    }
}