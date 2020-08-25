using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Areas.Estate.Models
{
    public class CreateContractViewModel
    {
        public String ContractDescription { get; set; }
        public Guid OperatorId { get; set; }
    }

    public class CreateContractProductViewModel
    {
        public String ProductName { get; set; }
        public String DisplayText { get; set; }
        public Decimal? Value { get; set; }
        public List<CreateContractProductTransactionFeeViewModel> TransactionFees { get; set; }
    }

    public class CreateContractProductTransactionFeeViewModel
    {
        public Int32 CalculationType { get; set; }
        public Int32 FeeType { get; set; }
        public Decimal Value { get; set; }
        public String Description { get; set; }

    }
}
