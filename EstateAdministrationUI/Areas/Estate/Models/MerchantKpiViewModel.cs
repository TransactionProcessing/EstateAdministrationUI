using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class MerchantKpiViewModel{
    public int MerchantsWithSaleInLastHour { get; set; }

    public int MerchantsWithNoSaleToday { get; set; }

    public int MerchantsWithNoSaleInLast7Days { get; set; }
}