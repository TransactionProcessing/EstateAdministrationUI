namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ContactViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the contact email address.
        /// </summary>
        /// <value>
        /// The contact email address.
        /// </value>
        public String ContactEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>
        /// The contact identifier.
        /// </value>
        public Guid ContactId { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>
        /// The name of the contact.
        /// </value>
        public String ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number.
        /// </summary>
        /// <value>
        /// The contact phone number.
        /// </value>
        public Object ContactPhoneNumber { get; set; }

        #endregion
    }
}