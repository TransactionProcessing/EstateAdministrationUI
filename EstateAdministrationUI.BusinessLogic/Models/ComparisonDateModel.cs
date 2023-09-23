namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ComparisonDateModel
{
    public int OrderValue { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }
}