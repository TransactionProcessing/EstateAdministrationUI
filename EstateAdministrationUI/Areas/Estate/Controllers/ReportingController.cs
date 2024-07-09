using EstateAdministrationUI.BusinessLogic.Models;
using SimpleResults;

namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Azure.Core;
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

        [HttpGet]
        public async Task<IActionResult> GetSettlementAnalysis(CancellationToken cancellationToken)
        {
            return this.View("SettlementAnalysis");
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
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(Request.QueryString.Value, "comparisonDate");
            String comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(Request.QueryString.Value, "comparisonDateLabel");
            Int32? merchantReportingId = QueryStringHelper.GetIntegerValueFromQueryString(Request.QueryString.Value, "merchantId");
            Int32? operatorReportingId = QueryStringHelper.GetIntegerValueFromQueryString(Request.QueryString.Value, "operatorId");

            Result<TodaysSalesModel> response = await this.ApiClient.GetTodaysSales(accessToken, estateId, merchantReportingId, operatorReportingId, comparisonDate, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);
            
            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateFailedTransactionsDueToLowCreditAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            var comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(Request.QueryString.Value, "comparisonDate");
            var comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(Request.QueryString.Value, "comparisonDateLabel");

            Result<TodaysSalesModel> response =
                await this.ApiClient.GetTodaysFailedSales(accessToken, estateId, "1009", comparisonDate, cancellationToken);

            TodaysSalesViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetMerchantKpisAsJson(CancellationToken cancellationToken){

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            Result<MerchantKpiModel> response =
                await this.ApiClient.GetMerchantKpi(accessToken, estateId, cancellationToken);

            MerchantKpiViewModel viewModel = ViewModelFactory.ConvertFrom(response);
            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3MerchantsBySalesValueAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            List<BottomMerchantModel> merchants = new List<BottomMerchantModel>();
            
            Result<List<TopBottomMerchantDataModel>> response =
                await this.ApiClient.GetTopBottomMerchantData(accessToken, estateId
                                                              , BusinessLogic.Models.TopBottom.Bottom, 3, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response);

            var model = new { BottomMerchants = viewModel.Merchants };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3OperatorsBySalesValueAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            List<BottomOperatorModel> operators = new List<BottomOperatorModel>();

            Result<List<TopBottomOperatorDataModel>> response =
                await this.ApiClient.GetTopBottomOperatorData(accessToken, estateId
                                                              , BusinessLogic.Models.TopBottom.Bottom, 3, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response);

            var model = new { BottomOperators = viewModel.Operators };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3ProductsBySalesValueAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            List<BottomProductModel> products = new List<BottomProductModel>();

            Result<List<TopBottomProductDataModel>> response =
                await this.ApiClient.GetTopBottomProductData(accessToken, estateId, BusinessLogic.Models.TopBottom.Bottom, 3, cancellationToken);

            var viewModel = ViewModelFactory.ConvertFrom(response);

            var model = new { BottomProducts = viewModel.Products };

            return this.Json(model);
        }


        [HttpPost]
        public async Task<IActionResult> GetComparisonDatesAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            List<(String value, String text)> datesList = new List<(String, String)>();

            Result<List<ComparisonDateModel>> response = await this.ApiClient.GetComparisonDates(accessToken, estateId, cancellationToken);
            
            List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);
            return this.Json(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetMerchantListAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            Result<List<MerchantListModel>> response = await this.ApiClient.GetMerchantsForReporting(accessToken, estateId, cancellationToken);

            List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);
            viewModels = viewModels.OrderBy(v => v.value).ToList();
            return this.Json(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetOperatorListAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            Result<List<OperatorListModel>> response = await this.ApiClient.GetOperatorsForReporting(accessToken, estateId, cancellationToken);

            List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);
            viewModels = viewModels.OrderBy(v => v.value).ToList();
            return this.Json(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateSettlementAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            String comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(this.Request.QueryString.Value, "comparisonDateLabel");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            Result<TodaysSettlementModel> response =
                await this.ApiClient.GetTodaysSettlement(accessToken, estateId, merchantId, operatorId, comparisonDate, cancellationToken);

            TodaysSettlementViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesCountByHourAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            Result<List<TodaysSalesCountByHourModel>> response =
                await this.ApiClient.GetTodaysSalesCountByHour(accessToken, estateId, merchantId, operatorId, comparisonDate, cancellationToken);

            List<HourCountViewModel> viewModels = ViewModelFactory.ConvertFrom(response);
            viewModels = viewModels.OrderBy(r => r.Hour).ToList();

            var model = new
            {
                transactionHourViewModels = viewModels
            };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesValueByHourAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            Result<List<TodaysSalesValueByHourModel>> response =
                await this.ApiClient.GetTodaysSalesValueByHour(accessToken, estateId, merchantId, operatorId, comparisonDate, cancellationToken);

            List<HourValueViewModel> viewModels = ViewModelFactory.ConvertFrom(response);
            viewModels = viewModels.OrderBy(r => r.Hour).ToList();

            var model = new
            {
                transactionHourViewModels = viewModels
            };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetLastSettlementAsJson(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            Result<LastSettlementModel> response =
                await this.ApiClient.GetLastSettlement(accessToken, estateId, merchantId, operatorId, cancellationToken);

            LastSettlementViewModel viewModel = ViewModelFactory.ConvertFrom(response);
            
            return this.Json(viewModel);
        }

        #endregion
    }
}