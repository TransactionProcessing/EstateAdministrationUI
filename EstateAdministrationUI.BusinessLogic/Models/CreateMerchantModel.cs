﻿namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class CreateMerchantModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public AddressModel Address { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        public ContactModel Contact { get; set; }

        /// <summary>
        /// Gets or sets the name of the merchant.
        /// </summary>
        /// <value>
        /// The name of the merchant.
        /// </value>
        public String MerchantName { get; set; }

        #endregion
    }
}