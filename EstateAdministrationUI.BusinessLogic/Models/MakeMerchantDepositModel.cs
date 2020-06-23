namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;

    public class MakeMerchantDepositModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the deposit date time.
        /// </summary>
        /// <value>
        /// The deposit date time.
        /// </value>
        public DateTime DepositDateTime { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public String Reference { get; set; }

        #endregion
    }
}