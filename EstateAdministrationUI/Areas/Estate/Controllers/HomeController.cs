namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using Factories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class HomeController : Controller
    {
        #region Fields

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;

        /// <summary>
        /// The view model factory
        /// </summary>
        private readonly IViewModelFactory ViewModelFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="viewModelFactory">The view model factory.</param>
        public HomeController(IApiClient apiClient,
                              IViewModelFactory viewModelFactory)
        {
            this.ApiClient = apiClient;
            this.ViewModelFactory = viewModelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        #endregion
    }
}