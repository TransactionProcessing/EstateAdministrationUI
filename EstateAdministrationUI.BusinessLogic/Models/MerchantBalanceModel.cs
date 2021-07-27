using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAdministrationUI.BusinessLogic.Models
{
    using EstateManagement.DataTransferObjects.Responses;

    public class MerchantBalanceModel
    {
        public Guid EstateId { get; set; }

        public Guid MerchantId { get; set; }

        public Decimal AvailableBalance { get; set; }
        
        public Decimal Balance { get; set; }
    }
}
