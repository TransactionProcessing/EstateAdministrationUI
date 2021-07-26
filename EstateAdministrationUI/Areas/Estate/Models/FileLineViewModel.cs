namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class FileLineViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the line data.
        /// </summary>
        /// <value>
        /// The line data.
        /// </value>
        public String LineData { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public Int32 LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the processing result.
        /// </summary>
        /// <value>
        /// The processing result.
        /// </value>
        public FileLineProcessingResult ProcessingResult { get; set; }

        /// <summary>
        /// Gets or sets the processing result string.
        /// </summary>
        /// <value>
        /// The processing result string.
        /// </value>
        public String ProcessingResultString { get; set; }

        /// <summary>
        /// Gets or sets the rejection reason.
        /// </summary>
        /// <value>
        /// The rejection reason.
        /// </value>
        public String RejectionReason { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public Guid TransactionId { get; set; }

        #endregion
    }
}