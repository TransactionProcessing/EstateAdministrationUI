namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;

    public class CreateContractModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>
        /// The operator identifier.
        /// </value>
        public Guid OperatorId { get; set; }

        #endregion
    }

    public class CreateContractResponseModel
    {
        public Guid ContractId { get; set; }

        public Guid EstateId { get; set; }

        public Guid OperatorId { get; set; }
    }
}