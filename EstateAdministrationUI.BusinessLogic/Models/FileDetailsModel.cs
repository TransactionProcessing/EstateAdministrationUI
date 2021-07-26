namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FileDetailsModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; set; }

        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public Guid FileId { get; set; }

        /// <summary>
        /// Gets or sets the file import log identifier.
        /// </summary>
        /// <value>
        /// The file import log identifier.
        /// </value>
        public Guid FileImportLogId { get; set; }

        /// <summary>
        /// Gets or sets the file lines.
        /// </summary>
        /// <value>
        /// The file lines.
        /// </value>
        public List<FileLineModel> FileLines { get; set; }

        /// <summary>
        /// Gets or sets the file location.
        /// </summary>
        /// <value>
        /// The file location.
        /// </value>
        public String FileLocation { get; set; }

        /// <summary>
        /// Gets or sets the file profile identifier.
        /// </summary>
        /// <value>
        /// The file profile identifier.
        /// </value>
        public Guid FileProfileId { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [processing completed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [processing completed]; otherwise, <c>false</c>.
        /// </value>
        public Boolean ProcessingCompleted { get; set; }

        /// <summary>
        /// Gets or sets the processing summary.
        /// </summary>
        /// <value>
        /// The processing summary.
        /// </value>
        public FileProcessingSummaryModel ProcessingSummary { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileLineProcessingResult
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The not processed
        /// </summary>
        NotProcessed,

        /// <summary>
        /// The successful
        /// </summary>
        Successful,

        /// <summary>
        /// The failed
        /// </summary>
        Failed,

        /// <summary>
        /// The ignored
        /// </summary>
        Ignored,

        /// <summary>
        /// The rejected
        /// </summary>
        Rejected,
    }
}