using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TopBottomProductViewModelList
{
    public TopBottomProductViewModelList(){
        this.Products = new List<TopBottomProductViewModel>();
    }
    public List<TopBottomProductViewModel> Products { get; set; }
}