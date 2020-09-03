namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class ContractProductListViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the contract identifier.
        /// </summary>
        /// <value>
        /// The contract identifier.
        /// </value>
        public Guid ContractId { get; set; }

        /// <summary>
        /// Gets or sets the contract products.
        /// </summary>
        /// <value>
        /// The contract products.
        /// </value>
        public List<ContractProductViewModel> ContractProducts { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }

        #endregion
    }
}