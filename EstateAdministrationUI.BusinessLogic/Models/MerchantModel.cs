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
        
        public List<MerchantDeviceModel> Devices { get; set; }
        
        public Guid EstateId { get; set; }
        
        public Guid MerchantId { get; set; }
        
        public String MerchantName { get; set; }
        
        public List<MerchantOperatorModel> Operators { get; set; }

        public SettlementSchedule SettlementSchedule { get; set; }

        public Decimal AvailableBalance { get; set; }
        public Decimal Balance { get; set; }

        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class MerchantListModel
    {
        #region Properties
        public Int32 MerchantReportingId { get; set; }

        public Guid MerchantId { get; set; }

        public String MerchantName { get; set; }
        
        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class OperatorListModel
    {
        #region Properties
        public Int32 OperatorReportingId { get; set; }
        public Guid OperatorId { get; set; }

        public String OperatorName { get; set; }

        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class MerchantDeviceModel
    {
        #region Properties

        public Guid DeviceId { get; set; }

        public String DeviceIdentifier { get; set; }

        #endregion
    }
}