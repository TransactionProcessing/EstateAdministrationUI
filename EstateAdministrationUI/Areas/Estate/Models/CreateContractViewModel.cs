namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateContractViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the contract description.
        /// </summary>
        /// <value>
        /// The contract description.
        /// </value>
        public String ContractDescription { get; set; }

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>
        /// The operator identifier.
        /// </value>
        public Guid OperatorId { get; set; }

        #endregion
    }
}