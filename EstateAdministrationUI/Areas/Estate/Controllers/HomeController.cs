namespace EstateAdministrationUI.Areas.Estate.Controllers{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class HomeController : Controller{

        #region Methods
        
        [HttpGet]
        public IActionResult Index(){
            return this.View();
        }

        #endregion
    }
}