using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Estate")]
    public class HomeController :Controller
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        public HomeController()
        {
        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
