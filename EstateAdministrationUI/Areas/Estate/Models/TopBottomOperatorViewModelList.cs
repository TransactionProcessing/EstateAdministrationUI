using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TopBottomOperatorViewModelList
{
    public TopBottomOperatorViewModelList(){
        this.Operators = new List<TopBottomOperatorViewModel>();
    }
    public List<TopBottomOperatorViewModel> Operators { get; set; }
}