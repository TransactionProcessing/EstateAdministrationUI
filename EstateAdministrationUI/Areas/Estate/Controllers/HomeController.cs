namespace EstateAdministrationUI.Areas.Estate.Controllers{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Common;
    using EstateReportingAPI.Client;
    using EstateReportingAPI.DataTransferObjects;
    using EstateReportingAPI.DataTrasferObjects;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.Editing;
    using Services;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class HomeController : Controller{
        #region Fields

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;

        private readonly IConfigurationService ConfigurationService;

        #endregion

        #region Constructors

        public HomeController(IApiClient apiClient,
                              IConfigurationService configurationService){
            this.ApiClient = apiClient;
            this.ConfigurationService = configurationService;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> GetComparisonDatesAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            List<ComparisonDateModel> response =
                await this.ApiClient.GetComparisonDates(accessToken, estateId, cancellationToken);

            List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);

            return this.Json(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetMerchantListAsJson(CancellationToken cancellationToken)
        {
            //String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            //Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            //List<ComparisonDateModel> response =
            //    await this.ApiClient.GetComparisonDates(accessToken, estateId, cancellationToken);

            //List<(String value, String text)> viewModels = ViewModelFactory.ConvertFrom(response);
            List<(String value, String text)> viewModels = new List<(String value, String text)>{
                                                                                                    (Guid.Empty.ToString(), "-- Select a Merchant --"),
                                                                                                    ("A5B55C9A-12D6-4DE3-BF51-B111B5C8792C", "S7 Merchant"),
                                                                                                    ("AB1C99FB-1C6C-4694-9A32-B71BE5D1DA33", "Test Merchant 1"),
                                                                                                    ("AF4D7C7C-9B8D-4E58-A12A-28D3E0B89DF6", "Test Merchant 2"),
                                                                                                    ("0D20A8B7-DA2E-421A-B591-53A08E694CEF", "Test Merchant 3"),
                                                                                                    ("BF1BFE5E-075C-49D0-A254-B9181899133A", "Test Merchant 4"),
                                                                                                    ("ADC939A8-315D-4019-9080-15259FA92324", "Test Merchant 5"),
                                                                                                    ("8BC8434D-41F9-4CC3-83BC-E73F20C02E1D", "v28 Emulator Merchant"),
                                                                                                    ("22549CD1-AFDD-44F9-882F-51C3EA71CCC0", "Xperia Merchant")
                                                                                                };
            return this.Json(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateSettlementAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            String comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(this.Request.QueryString.Value, "comparisonDateLabel");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            TodaysSettlementModel response =
                await this.ApiClient.GetTodaysSettlement(accessToken, estateId, merchantId, operatorId, comparisonDate, cancellationToken);

            TodaysSettlementViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateTransactionsAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            String comparisonDateLabel = QueryStringHelper.GetValueFromQueryString(this.Request.QueryString.Value, "comparisonDateLabel");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            TodaysSalesModel response =
                await this.ApiClient.GetTodaysSales(accessToken, estateId, merchantId, operatorId, comparisonDate, cancellationToken);

            TodaysSalesViewModel viewModel = ViewModelFactory.ConvertFrom(response, comparisonDateLabel);

            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesCountByHourAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            List<TodaysSalesCountByHourModel> response =
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
        public async Task<IActionResult> GetSalesValueByHourAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);

            DateTime comparisonDate = QueryStringHelper.GetDateTimeValueFromQueryString(this.Request.QueryString.Value, "comparisonDate", "yyyy-MM-dd");
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            List<TodaysSalesValueByHourModel> response =
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
        public async Task<IActionResult> GetYesterdaysSettlementAsJson(CancellationToken cancellationToken){
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            Guid estateId = Helpers.GetClaimValue<Guid>(this.User.Identity as ClaimsIdentity, Helpers.EstateIdClaimType);
            Guid? merchantId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "merchantId");
            Guid? operatorId = QueryStringHelper.GetGuidValueFromQueryString(Request.QueryString.Value, "operatorId");

            TodaysSettlementModel response = await this.ApiClient.GetTodaysSettlement(accessToken, estateId, merchantId, operatorId, DateTime.Now, cancellationToken);

            TodaysSettlementViewModel viewModel = ViewModelFactory.ConvertFrom(response);

            return this.Json(viewModel);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(){
            return this.View();
        }

        #endregion
    }
}