using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TopBottomMerchantViewModelList
{
    public TopBottomMerchantViewModelList(){
        this.Merchants = new List<TopBottomMerchantViewModel>();
    }
    public List<TopBottomMerchantViewModel> Merchants { get; set; }
}