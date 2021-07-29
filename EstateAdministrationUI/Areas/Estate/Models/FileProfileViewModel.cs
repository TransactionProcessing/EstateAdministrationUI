namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FileProfileViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file profile identifier.
        /// </summary>
        /// <value>
        /// The file profile identifier.
        /// </value>
        public Guid FileProfileId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file profile.
        /// </summary>
        /// <value>
        /// The name of the file profile.
        /// </value>
        public String FileProfileName { get; set; }

        #endregion
    }
}