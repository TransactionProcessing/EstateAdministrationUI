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
    using Common;
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
        

        private readonly IConfigurationService ConfigurationService;

        #endregion

        #region Constructors
        
        public HomeController(IApiClient apiClient,
                              IConfigurationService configurationService)
        {
            this.ApiClient = apiClient;
            this.ConfigurationService = configurationService;
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
            var embedClass = JsonConvert.DeserializeObject<EmbedClass>(embedQuerString.ToString());

            var embedQuery = embedClass.embedQuerString;
            // User your user-email as embed_user_email
            embedQuery += "&embed_user_email=" + this.ConfigurationService.BoldBIUserEmail;
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
                var keyBytes = encoding.GetBytes(this.ConfigurationService.BoldBIEmbedSecret);
                var messageBytes = encoding.GetBytes(queryString);
                using (var hmacsha1 = new HMACSHA256(keyBytes))
                {
                    var hashMessage = hmacsha1.ComputeHash(messageBytes);
                    return Convert.ToBase64String(hashMessage);
                }
            }
            return string.Empty;
        }

        #endregion
    }

    [DataContract]
    [ExcludeFromCodeCoverage]
    public class EmbedClass
    {
        [DataMember]
        public string embedQuerString { get; set; }

        [DataMember]
        public string dashboardServerApiUrl { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TokenObject
    {
        public string Message { get; set; }

        public string Status { get; set; }

        public string Token { get; set; }
    }

    [ExcludeFromCodeCoverage]
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