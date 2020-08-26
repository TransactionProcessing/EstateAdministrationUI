namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ContractModel
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; set; }

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>
        /// The operator identifier.
        /// </value>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the operator.
        /// </summary>
        /// <value>
        /// The name of the operator.
        /// </value>
        public String OperatorName { get; set; }

        /// <summary>
        /// Gets or sets the number of products.
        /// </summary>
        /// <value>
        /// The number of products.
        /// </value>
        public Int32 NumberOfProducts { get; set; }

        #endregion
    }
}