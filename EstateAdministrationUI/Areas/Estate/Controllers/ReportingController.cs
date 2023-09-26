using EstateAdministrationUI.BusinessLogic.Models;

namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using BusinessLogic.Models;
    using Common;
    using EstateReportingAPI.Client;
    using EstateReportingAPI.DataTransferObjects;
    using EstateReportingAPI.DataTrasferObjects;
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

        private readonly IApiClient ApiClient;


        #endregion

        #region Constructors
        
        public ReportingController(IApiClient apiClient){
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

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateTransactionsAsJson(CancellationToken cancellationToken)
        {
            var comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(Request.QueryString.Value, "comparisonDate");
            var comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(Request.QueryString.Value, "comparisonDateLabel");
            
            TodaysSalesModel response = await this.ApiClient.GetTodaysSales(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), comparisonDate, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);
            
            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateFailedTransactionsDueToLowCreditAsJson(CancellationToken cancellationToken)
        {
            var comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(Request.QueryString.Value, "comparisonDate");
            var comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(Request.QueryString.Value, "comparisonDateLabel");

            TodaysSalesModel response =
                await this.ApiClient.GetTodaysFailedSales(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"),"1009", comparisonDate, cancellationToken);

            TodaysSalesViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetMerchantKpisAsJson(CancellationToken cancellationToken){

            MerchantKpiModel response =
                await this.ApiClient.GetMerchantKpi(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), cancellationToken);

            MerchantKpiViewModel viewModel = ViewModelFactory.ConvertFrom(response);
            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3MerchantsBySalesValueAsJson(CancellationToken cancellationToken){
            List<BottomMerchantModel> merchants = new List<BottomMerchantModel>();
            
            List<TopBottomMerchantDataModel> response =
                await this.ApiClient.GetTopBottomMerchantData(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22")
                                                                             , BusinessLogic.Models.TopBottom.Bottom, 3, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response);

            var model = new { BottomMerchants = viewModel.Merchants };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3OperatorsBySalesValueAsJson(CancellationToken cancellationToken)
        {
            List<BottomOperatorModel> operators = new List<BottomOperatorModel>();

            var response =
                await this.ApiClient.GetTopBottomOperatorData(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22")
                                                                             , BusinessLogic.Models.TopBottom.Bottom, 3, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response);

            var model = new { BottomOperators = viewModel.Operators };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3ProductsBySalesValueAsJson(CancellationToken cancellationToken)
        {
            List<BottomProductModel> products = new List<BottomProductModel>();

            var response =
                await this.ApiClient.GetTopBottomProductData(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22")
                                                                             , BusinessLogic.Models.TopBottom.Bottom, 3, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response);

            var model = new { BottomProducts = viewModel.Products };

            return this.Json(model);
        }


        [HttpPost]
        public async Task<IActionResult> GetComparisonDatesAsJson(CancellationToken cancellationToken)
        {
            List<(String value, String text)> datesList = new List<(String, String)>();

            List<ComparisonDateModel> response =
                await this.ApiClient.GetComparisonDates(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), cancellationToken);

            List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);
            return this.Json(viewModels);
        }
        #endregion
    }
}