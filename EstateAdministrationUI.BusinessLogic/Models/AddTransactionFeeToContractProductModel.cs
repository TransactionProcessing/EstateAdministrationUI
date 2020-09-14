namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AddTransactionFeeToContractProductModel
    {
        public CalculationType CalculationType { get; set; }

        public string Description { get; set; }

        public FeeType FeeType { get; set; }

        public Decimal Value { get; set; }
    }

    public enum FeeType
    {
        Merchant,
        ServiceProvider,
    }

    public enum CalculationType
    {
        Fixed,
        Percentage,
    }

    [ExcludeFromCodeCoverage]
    public class AddTransactionFeeToContractProductResponseModel
    {
        public Guid ContractId { get; set; }

        public Guid EstateId { get; set; }

        public Guid ProductId { get; set; }

        public Guid TransactionFeeId { get; set; }
    }
}