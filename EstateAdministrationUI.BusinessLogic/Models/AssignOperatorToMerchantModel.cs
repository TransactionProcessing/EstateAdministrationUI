namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssignOperatorToMerchantModel
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

    [ExcludeFromCodeCoverage]
    public class AssignOperatorToMerchantResponseModel
    {
        public Guid EstateId { get; set; }

        public Guid MerchantId { get; set; }

        public Guid OperatorId { get; set; }
    }
}