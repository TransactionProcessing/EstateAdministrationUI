namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class ReportingController : Controller
    {
        #region Fields

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;
        

        #endregion

        #region Constructors
        
        public ReportingController(IApiClient apiClient)
        {
            this.ApiClient = apiClient;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the merchant analysis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMerchantAnalysis(CancellationToken cancellationToken)
        {
            return this.View("MerchantAnalysis");
        }

        /// <summary>
        /// Gets the operator analysis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOperatorAnalysis(CancellationToken cancellationToken)
        {
            return this.View("OperatorAnalysis");
        }
        
        /// <summary>
        /// Gets the transaction analysis.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTransactionAnalysis(CancellationToken cancellationToken)
        {
            return this.View("TransactionAnalysis");
        }

        #endregion
    }
}