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

        /// <summary>
        /// The API client
        /// </summary>
        private readonly IApiClient ApiClient;

        private readonly IEstateReportingApiClient EstateReportingApiClient;

        #endregion

        #region Constructors
        
        public ReportingController(IApiClient apiClient,
            IEstateReportingApiClient estateReportingApiClient){
            this.ApiClient = apiClient;
            this.EstateReportingApiClient = estateReportingApiClient;
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
        public async Task<IActionResult> GetComparisonDateFailedTransactionsDueToLowCreditAsJson(CancellationToken cancellationToken)
        {
            var qs = HttpUtility.ParseQueryString(Request.QueryString.Value);
            var comparisonDate = qs["comparisonDate"];
            var comparisonDateLabel = qs["comparisonDateLabel"];
            DateTime d = DateTime.ParseExact(comparisonDate, "yyyy-MM-dd", null);

            var response =
                await this.EstateReportingApiClient.GetTodaysFailedSales(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"),"1009", d, cancellationToken);
            
            Decimal variance = (response.TodaysSalesValue - response.ComparisonSalesValue).SafeDivision(response.TodaysSalesValue);

            var model = new
            {
                TodaysCountOfTransactions = response.TodaysSalesValue,
                ComparisonCountOfTransactions = response.ComparisonSalesValue,
                Label = $"{comparisonDateLabel} Sales",
                Variance = variance
            };

            return this.Json(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetMerchantKpisAsJson(CancellationToken cancellationToken){

            var response =
                await this.EstateReportingApiClient.GetMerchantKpi(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), cancellationToken);
            var model = new
            {
                ActiveMerchantCount = response.MerchantsWithSaleInLastHour,
                NoSalesInLastHourCount = response.MerchantsWithNoSaleToday,
                NoSalesInLast7DaysCount = response.MerchantsWithNoSaleInLast7Days
            };

            //var model = new{
            //                   ActiveMerchantCount = 1,
            //                   NoSalesInLastHourCount = 2,
            //                   NoSalesInLast7DaysCount = 3
            //};
            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3MerchantsBySalesValueAsJson(CancellationToken cancellationToken){
            List<BottomMerchant> merchants = new List<BottomMerchant>();
            
            var response =
                await this.EstateReportingApiClient.GetTopBottomMerchantData(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22")
                                                                             , TopBottom.Bottom, 3, cancellationToken);

            response.ForEach(m => merchants.Add(new BottomMerchant{
                                                        SalesValue = m.SalesValue,
                                                        MerchantName = m.MerchantName,
                                                    }));

            var model = new{ BottomMerchants = merchants };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3OperatorsBySalesValueAsJson(CancellationToken cancellationToken)
        {
            List<BottomOperator> operators = new List<BottomOperator>();

            var response =
                await this.EstateReportingApiClient.GetTopBottomOperatorData(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22")
                                                                             , TopBottom.Bottom, 3, cancellationToken);

            response.ForEach(o => operators.Add(new BottomOperator
                                            {
                                                SalesValue = o.SalesValue,
                                                OperatorName = o.OperatorName,
                                            }));

            var model = new { BottomOperators = operators };

            return this.Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetBottom3ProductsBySalesValueAsJson(CancellationToken cancellationToken)
        {
            List<BottomProduct> products = new List<BottomProduct>();

            var response =
                await this.EstateReportingApiClient.GetTopBottomProductData(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22")
                                                                             , TopBottom.Bottom, 3, cancellationToken);

            response.ForEach(p => products.Add(new BottomProduct
            {
                                      SalesValue = p.SalesValue,
                                      ProductName = p.ProductName,
                                  }));

            var model = new { BottomProducts = products };

            return this.Json(model);
        }


        [HttpPost]
        public async Task<IActionResult> GetComparisonDatesAsJson(CancellationToken cancellationToken)
        {
            List<(String value, String text)> datesList = new List<(String, String)>();

            List<ComparisonDate> response =
                await this.EstateReportingApiClient.GetComparisonDates(null, Guid.Parse("435613AC-A468-47A3-AC4F-649D89764C22"), cancellationToken);

            foreach (ComparisonDate comparisonDate in response)
            {
                datesList.Add((comparisonDate.Date.ToString("yyyy-MM-dd"), comparisonDate.Description));
            }

            return this.Json(datesList);
        }
        #endregion
    }

    public class BottomMerchant{
        public String MerchantName { get; set; }

        public Decimal SalesValue { get; set; }
    }

    public class BottomProduct
    {
        public String ProductName { get; set; }

        public Decimal SalesValue { get; set; }
    }

    public class BottomOperator
    {
        public String OperatorName { get; set; }

        public Decimal SalesValue { get; set; }
    }
}