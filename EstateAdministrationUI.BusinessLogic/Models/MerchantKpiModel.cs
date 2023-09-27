﻿using System.Diagnostics.CodeAnalysis;

namespace EstateAdministrationUI.BusinessLogic.Models;
[ExcludeFromCodeCoverage]
public class MerchantKpiModel
{
    public int MerchantsWithSaleInLastHour { get; set; }

    public int MerchantsWithNoSaleToday { get; set; }

    public int MerchantsWithNoSaleInLast7Days { get; set; }
}