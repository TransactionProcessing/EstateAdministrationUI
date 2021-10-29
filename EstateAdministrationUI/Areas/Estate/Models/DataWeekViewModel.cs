namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DataWeekViewModel
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

        /// <summary>
        /// Gets or sets the week number.
        /// </summary>
        /// <value>
        /// The week number.
        /// </value>
        public Int32 WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public Int32 Year { get; set; }

        #endregion
    }
}