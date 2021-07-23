namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FileImportLogViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file count.
        /// </summary>
        /// <value>
        /// The file count.
        /// </value>
        public Int32 FileCount { get; set; }

        /// <summary>
        /// Gets or sets the file import log identifier.
        /// </summary>
        /// <value>
        /// The file import log identifier.
        /// </value>
        public Guid FileImportLogId { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        public List<FileImportLogFileViewModel> Files { get; set; }

        /// <summary>
        /// Gets or sets the import log date.
        /// </summary>
        /// <value>
        /// The import log date.
        /// </value>
        public DateTime ImportLogDate { get; set; }

        /// <summary>
        /// Gets or sets the import log date time.
        /// </summary>
        /// <value>
        /// The import log date time.
        /// </value>
        public DateTime ImportLogDateTime { get; set; }

        /// <summary>
        /// Gets or sets the import log time.
        /// </summary>
        /// <value>
        /// The import log time.
        /// </value>
        public TimeSpan ImportLogTime { get; set; }

        #endregion
    }
}