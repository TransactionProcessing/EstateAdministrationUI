using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.BusinessLogic.Models
{
    public class CreateMerchantModel
    {
        public String MerchantName { get; set; }

        public AddressModel Address { get; set; }

        public ContactModel Contact { get; set; }
    }

    public class CreateMerchantResponseModel
    {
        public Guid EstateId { get; set; }
        public Guid MerchantId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ContactId { get; set; }

    }
}
