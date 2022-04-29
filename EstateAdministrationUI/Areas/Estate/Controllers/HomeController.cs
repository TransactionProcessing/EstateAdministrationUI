namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;
    using Factories;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("GetDetails")]
        public string GetDetails([FromBody] object embedQuerString)
        {
            var embedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<EmbedClass>(embedQuerString.ToString());

            var embedQuery = embedClass.embedQuerString;
            // User your user-email as embed_user_email
            embedQuery += "&embed_user_email=" + EmbedProperties.UserEmail;
            var embedDetailsUrl = "/embed/authorize?" + embedQuery.ToLower() + "&embed_signature=" + GetSignatureUrl(embedQuery.ToLower());

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(embedClass.dashboardServerApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                var result = client.GetAsync(embedClass.dashboardServerApiUrl + embedDetailsUrl).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                return resultContent;
            }

        }

        public string GetSignatureUrl(string queryString)
        {
            if (queryString != null)
            {
                var encoding = new System.Text.UTF8Encoding();
                var keyBytes = encoding.GetBytes(EmbedProperties.EmbedSecret);
                var messageBytes = encoding.GetBytes(queryString);
                using (var hmacsha1 = new HMACSHA256(keyBytes))
                {
                    var hashMessage = hmacsha1.ComputeHash(messageBytes);
                    return Convert.ToBase64String(hashMessage);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the todays transactions as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTodaysTransactionsAsJson")]
        public async Task<IActionResult> GetTodaysTransactionsAsJson(CancellationToken cancellationToken)
        {
            return await GetTransactionsForPeriod(DatePeriod.Today, cancellationToken);
        }

        /// <summary>
        /// Gets the this weeks transactions as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetThisWeeksTransactionsAsJson")]
        public async Task<IActionResult> GetThisWeeksTransactionsAsJson(CancellationToken cancellationToken)
        {
            return await GetTransactionsForPeriod(DatePeriod.ThisWeek, cancellationToken);
        }

        /// <summary>
        /// Gets the number of merchants as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNumberOfMerchantsAsJson")]
        public async Task<IActionResult> GetNumberOfMerchantsAsJson(CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                List<MerchantModel> merchants = await this.ApiClient.GetMerchants(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

                MerchantCountViewModel viewModel = this.ViewModelFactory.ConvertFrom(merchants.ToArray());

                return this.Json(viewModel);
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the this months transactions as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetThisMonthsTransactionsAsJson")]
        public async Task<IActionResult> GetThisMonthsTransactionsAsJson(CancellationToken cancellationToken)
        {
            return await GetTransactionsForPeriod(DatePeriod.ThisMonth, cancellationToken);
        }

        private async Task<IActionResult> GetTransactionsForPeriod(DatePeriod period, CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                TransactionForPeriodModel transactionModel =
                    await this.ApiClient.GetTransactionsForDatePeriod(accessToken, this.User.Identity as ClaimsIdentity, period, cancellationToken);

                TransactionPeriodViewModel viewModel = this.ViewModelFactory.ConvertFrom(transactionModel);

                return this.Json(viewModel);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the transactions by merchant as json.
        /// </summary>
        /// <param name="merchantCount">The merchant count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTransactionsByMerchantAsJson")]
        public async Task<IActionResult> GetTransactionsByMerchantAsJson([FromQuery] Int32 merchantCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                  [FromQuery] DateTime startDate,
                                                                  [FromQuery] DateTime endDate,
                                                                  CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                DataByMerchantModel model = await this.ApiClient.GetTransactionsByMerchant(accessToken,
                                                                                           this.User.Identity as ClaimsIdentity,
                                                                                           startDate,
                                                                                           endDate,
                                                                                           merchantCount,
                                                                                           sortDirection,
                                                                                           sortField,
                                                                                           cancellationToken);

                DataByMerchantViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);

                return this.Json(viewModel);
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the transactions by operator as json.
        /// </summary>
        /// <param name="operatorCount">The operator count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTransactionsByOperatorAsJson")]
        public async Task<IActionResult> GetTransactionsByOperatorAsJson([FromQuery] Int32 operatorCount,
                                                                  [FromQuery] SortDirection sortDirection,
                                                                  [FromQuery] SortField sortField,
                                                                  [FromQuery] DateTime startDate,
                                                                  [FromQuery] DateTime endDate,
                                                                  CancellationToken cancellationToken)
        {
            try
            {
                String accessToken = await this.HttpContext.GetTokenAsync("access_token");

                DataByOperatorModel model = await this.ApiClient.GetTransactionsByOperator(accessToken,
                                                                                           this.User.Identity as ClaimsIdentity,
                                                                                           startDate,
                                                                                           endDate,
                                                                                           operatorCount,
                                                                                           sortDirection,
                                                                                           sortField,
                                                                                           cancellationToken);

                DataByOperatorViewModel viewModel = this.ViewModelFactory.ConvertFrom(model);
                return this.Json(viewModel);
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        #endregion
    }

    public class EmbedProperties
    {
        //Dashboard Server BI URL (ex: http://localhost:5000/bi, http://demo.boldbi.com/bi)
        public static string RootUrl = "http://localhost/bi";

        //For Bold BI Enterprise edition, it should be like `site/site1`. For Bold BI Cloud, it should be empty string.
        public static string SiteIdentifier = "site/site1";

        //Your Bold BI application environment. (If Cloud, you should use `cloud`, if Enterprise, you should use `enterprise`)
        public static string Environment = "enterprise";

        //Enter your BoldBI credentials here.
        public static string UserEmail = "stuart_ferguson1development@outlook.com";

        // Get the embedSecret key from Bold BI.Please refer this link(https://help.boldbi.com/embedded-bi/site-administration/embed-settings/)
        public static string EmbedSecret = "5cmhwwl6ff4Qr7kQva9eTGbYxPpAyJXX";
    }

    [DataContract]
    public class EmbedClass
    {
        [DataMember]
        public string embedQuerString { get; set; }
        [DataMember]
        public string dashboardServerApiUrl { get; set; }
    }

    public class TokenObject
    {
        public string Message { get; set; }

        public string Status { get; set; }

        public string Token { get; set; }
    }

    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken
        {
            get;
            set;
        }

        [JsonProperty("token_type")]
        public string TokenType
        {
            get;
            set;
        }

        [JsonProperty("expires_in")]
        public string ExpiresIn
        {
            get;
            set;
        }

        [JsonProperty("email")]
        public string Email
        {
            get;
            set;
        }

        public string LoginResult
        {
            get;
            set;
        }

        public string LoginStatusInfo
        {
            get;
            set;
        }

        [JsonProperty(".issued")]
        public string Issued { get; set; }

        [JsonProperty(".expires")]
        public string Expires { get; set; }
    }
}