﻿namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class MerchantOperatorViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the merchant number.
        /// </summary>
        /// <value>
        /// The merchant number.
        /// </value>
        public String MerchantNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>
        /// The operator identifier.
        /// </value>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// Gets or sets the terminal number.
        /// </summary>
        /// <value>
        /// The terminal number.
        /// </value>
        public String TerminalNumber { get; set; }

        #endregion
    }
}