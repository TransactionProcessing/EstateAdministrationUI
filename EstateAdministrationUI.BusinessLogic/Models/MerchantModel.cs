namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MerchantModel
    {
        #region Properties
        
        public List<AddressModel> Addresses { get; set; }

        public List<ContactModel> Contacts { get; set; }
        
        public Dictionary<Guid, String> Devices { get; set; }
        
        public Guid EstateId { get; set; }
        
        public Guid MerchantId { get; set; }
        
        public String MerchantName { get; set; }
        
        public List<MerchantOperatorModel> Operators { get; set; }

        public SettlementSchedule SettlementSchedule { get; set; }

        public Decimal AvailableBalance { get; set; }
        public Decimal Balance { get; set; }

        #endregion
    }
}