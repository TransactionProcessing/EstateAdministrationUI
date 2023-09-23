namespace EstateAdministrationUI.Areas.Estate.Controllers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class HourValueViewModel
{
    public Int32 Hour { get; set; }
    public Decimal TodaysValue { get; set; }
    public Decimal ComparisonValue { get; set; }
}