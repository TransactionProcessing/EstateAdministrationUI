namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DataOperatorViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public String CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the operator.
        /// </summary>
        /// <value>
        /// The name of the operator.
        /// </value>
        public String OperatorName { get; set; }

        /// <summary>
        /// Gets or sets the number of transactions.
        /// </summary>
        /// <value>
        /// The number of transactions.
        /// </value>
        public Int32 Count { get; set; }

        /// <summary>
        /// Gets or sets the value of transactions.
        /// </summary>
        /// <value>
        /// The value of transactions.
        /// </value>
        public Decimal Value { get; set; }

        #endregion
    }
}