namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Security.Claims;
    using System.Security.Cryptography;
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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Newtonsoft.Json;
    using Services;
    using Shared.Logger;

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
        

        private readonly IConfigurationService ConfigurationService;

        private readonly IEstateReportingApiClient EstateReportingApiClient;

        #endregion

        #region Constructors
        
        public HomeController(IApiClient apiClient,
                              IConfigurationService configurationService,
                              IEstateReportingApiClient estateReportingApiClient)
        {
            this.ApiClient = apiClient;
            this.ConfigurationService = configurationService;
            this.EstateReportingApiClient = estateReportingApiClient;
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
        
        [HttpPost]
        public async Task<IActionResult> GetComparisonDateTransactionsAsJson(CancellationToken cancellationToken)
        {
            var qs = HttpUtility.ParseQueryString(Request.QueryString.Value);
            var comparisonDate = qs["comparisonDate"];
            var comparisonDateLabel = qs["comparisonDateLabel"];
            DateTime d = DateTime.ParseExact(comparisonDate, "yyyy-MM-dd", null);

            var response = 
                await this.EstateReportingApiClient.GetTodaysSales(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), d, cancellationToken);

            var todaysModel = new
                        {
                ValueOfTransactions = response.TodaysSalesValue
            };

            var comparisonModel = new
                                  {
                                      ValueOfTransactions = response.ComparisonSalesValue
                                  };
            
            var variance = (todaysModel.ValueOfTransactions - comparisonModel.ValueOfTransactions).SafeDivision(todaysModel.ValueOfTransactions);

            var model = new
            {
                TodaysValueOfTransactions = todaysModel.ValueOfTransactions,
                ComparisonValueOfTransactions = comparisonModel.ValueOfTransactions,
                Label = $"{comparisonDateLabel} Sales",
                Variance = variance
            };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetYesterdaysSettlementAsJson(CancellationToken cancellationToken)
        {
            var response = await this.EstateReportingApiClient.GetTodaysSettlement(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), DateTime.Now, cancellationToken);

            var model = new
                        {
                            ValueOfSettlement = response.TodaysSettlementValue
                        };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDateSettlementAsJson(CancellationToken cancellationToken)
        {
            var qs = HttpUtility.ParseQueryString(Request.QueryString.Value);
            var comparisonDate = qs["comparisonDate"];
            var comparisonDateLabel = qs["comparisonDateLabel"];
            DateTime d = DateTime.ParseExact(comparisonDate, "yyyy-MM-dd", null);

            var response =
                await this.EstateReportingApiClient.GetTodaysSettlement(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), d, cancellationToken);

            var todaysModel = new
                              {
                                  ValueOfSettlement = response.TodaysSettlementValue
                              };

            var comparisonModel = new
            {
                ValueOfSettlement = response.ComparisonSettlementValue,

            };

            var variance = (todaysModel.ValueOfSettlement - comparisonModel.ValueOfSettlement).SafeDivision(todaysModel.ValueOfSettlement);

            var model = new
                        {
                TodaysValueOfSettlement = todaysModel.ValueOfSettlement,
                ComparisonValueOfSettlement = comparisonModel.ValueOfSettlement,
                Label = $"{comparisonDateLabel} Settlement",
                            Variance = variance
                        };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetComparisonDatesAsJson(CancellationToken cancellationToken){
            List<(String value, String text)> datesList = new List<(String, String)>();

            List<ComparisonDate> response =
                await this.EstateReportingApiClient.GetComparisonDates(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), cancellationToken);

            foreach (ComparisonDate comparisonDate in response){
                datesList.Add((comparisonDate.Date.ToString("yyyy-MM-dd"), comparisonDate.Description));
            }

            return this.Json(datesList);
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesValueByHourAsJson(CancellationToken cancellationToken){

            var qs = HttpUtility.ParseQueryString(Request.QueryString.Value);
            var comparisonDate = qs["comparisonDate"];
            DateTime d = DateTime.ParseExact(comparisonDate, "yyyy-MM-dd", null);
            
            var model = new
            {
                transactionHourViewModels = new List<HourValueModel>()
            };

            List<TodaysSalesValueByHour> response =
                await this.EstateReportingApiClient.GetTodaysSalesValueByHour(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), d, cancellationToken);

            response = response.OrderBy(r => r.Hour).ToList();

            foreach (TodaysSalesValueByHour todaysSalesValueByHour in response){
                model.transactionHourViewModels.Add(new HourValueModel(){
                                                                            ComparisonValue = todaysSalesValueByHour.ComparisonSalesValue,
                                                                            Hour = todaysSalesValueByHour.Hour,
                                                                            TodaysValue = todaysSalesValueByHour.TodaysSalesValue
                                                                        });
            }

            return this.Json(model);

        }

        [HttpPost]
        public async Task<IActionResult> GetSalesCountByHourAsJson(CancellationToken cancellationToken)
        {
            var qs = HttpUtility.ParseQueryString(Request.QueryString.Value);
            var comparisonDate = qs["comparisonDate"];
            DateTime d = DateTime.ParseExact(comparisonDate, "yyyy-MM-dd", null);

            var model = new
            {
                transactionHourViewModels = new List<HourCountModel>()
            };

            var response =
                await this.EstateReportingApiClient.GetTodaysSalesCountByHour(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), d, cancellationToken);

            response = response.OrderBy(r => r.Hour).ToList();

            foreach (var todaysSalesCountByHour in response)
            {
                model.transactionHourViewModels.Add(new HourCountModel()
                                                    {
                                                        ComparisonCount = todaysSalesCountByHour.ComparisonSalesCount,
                                                        Hour = todaysSalesCountByHour.Hour,
                                                        TodaysCount = todaysSalesCountByHour.TodaysSalesCount
                                                    });
            }

            return this.Json(model);

        }

        public class HourValueModel{
            public Int32 Hour{ get; set; }
            public Decimal TodaysValue{ get; set; }
            public Decimal ComparisonValue { get; set; }
        }

        public class HourCountModel
        {
            public Int32 Hour { get; set; }
            public Decimal TodaysCount { get; set; }
            public Decimal ComparisonCount { get; set; }
        }

        #endregion
    }
}
