namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class MerchantListViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The address line1.
        /// </value>
        public String AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>
        /// The name of the contact.
        /// </value>
        public String ContactName { get; set; }

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the merchant.
        /// </summary>
        /// <value>
        /// The name of the merchant.
        /// </value>
        public String MerchantName { get; set; }

        /// <summary>
        /// Gets or sets the number of devices.
        /// </summary>
        /// <value>
        /// The number of devices.
        /// </value>
        public Int32 NumberOfDevices { get; set; }

        /// <summary>
        /// Gets or sets the number of operators.
        /// </summary>
        /// <value>
        /// The number of operators.
        /// </value>
        public Int32 NumberOfOperators { get; set; }

        /// <summary>
        /// Gets or sets the number of users.
        /// </summary>
        /// <value>
        /// The number of users.
        /// </value>
        public Int32 NumberOfUsers { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        public String Town { get; set; }

        #endregion
    }
}