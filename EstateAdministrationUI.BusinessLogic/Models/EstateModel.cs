using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.BusinessLogic.Models
{
    public class EstateModel
    {
        public Guid EstateId { get; set; }

        public string EstateName { get; set; }

        public List<EstateOperatorModel> Operators { get; set; }

        public List<SecurityUserModel> SecurityUsers { get; set; }
    }

    public class EstateOperatorModel
    {
        public String Name { get; set; }

        public Guid OperatorId { get; set; }

        public Boolean RequireCustomMerchantNumber { get; set; }

        public Boolean RequireCustomTerminalNumber { get; set; }
    }

    public class SecurityUserModel
    {
        public String EmailAddress { get; set; }

        public Guid SecurityUserId { get; set; }
    }
}
