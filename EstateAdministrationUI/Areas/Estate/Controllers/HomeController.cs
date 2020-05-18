namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Common;
    using BusinessLogic.Models;
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
        /// Gets the merchant list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMerchantList(CancellationToken cancellationToken)
        {
            return this.View("MerchantList");
        }

        /// <summary>
        /// Gets the merchant list as json.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMerchantListAsJson(CancellationToken cancellationToken)
        {
            Logger.LogDebug("In method GetMerchantListAsJson");

            // Search Value from (Search box)  
            String searchValue = this.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            Logger.LogDebug($"searchvalue is {searchValue}");

            String accessToken = await this.HttpContext.GetTokenAsync("access_token");
            Logger.LogDebug("got access token");

            List<MerchantModel> merchantList = await this.ApiClient.GetMerchants(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

            List<MerchantListViewModel> merchantViewModels = this.ViewModelFactory.ConvertFrom(merchantList);

            Logger.LogDebug($"merchant list count is {merchantViewModels.Count}");

            Expression<Func<MerchantListViewModel, Boolean>> whereClause = m => m.MerchantName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                                                                m.Town.Contains(searchValue, StringComparison.OrdinalIgnoreCase);

            DataTablesResult<MerchantListViewModel> dataTableResult = this.GetDataForDataTable(merchantViewModels, whereClause);

            String jsonResult = JsonConvert.SerializeObject(dataTableResult);
            Logger.LogDebug(jsonResult);

            return this.Json(this.GetDataForDataTable(merchantViewModels, whereClause));
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Manages the estate.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ManageEstate(CancellationToken cancellationToken)
        {
            String accessToken = await this.HttpContext.GetTokenAsync("access_token");

            EstateModel estateDetails = await this.ApiClient.GetEstate(accessToken, this.User.Identity as ClaimsIdentity, cancellationToken);

            return this.View(this.ViewModelFactory.ConvertFrom(estateDetails));
        }

        /// <summary>
        /// Manages the estate.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ManageEstate(EstateViewModel viewModel,
                                                      CancellationToken cancellationToken)
        {
            return this.View(viewModel);
        }

        /// <summary>
        /// Gets the data for data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryData">The query data.</param>
        /// <param name="whereClause">The where clause.</param>
        /// <returns></returns>
        private DataTablesResult<T> GetDataForDataTable<T>(IEnumerable<T> queryData,
                                                           Expression<Func<T, Boolean>> whereClause = null)
        {
            DataTablesResult<T> result;

            IFormCollection formData = this.HttpContext.Request.Form;

            if (formData == null)
            {
                result = null;
            }
            else
            {
                Logger.LogInformation("got form");
                // Extract the data tables fields
                String draw = formData["draw"].FirstOrDefault();
                Logger.LogInformation($"draw is {draw}");
                // Skiping number of Rows count  
                String start = formData["start"].FirstOrDefault();
                Logger.LogInformation($"start is {start}");
                // Paging Length 10,20  
                String length = formData["length"].FirstOrDefault();
                Logger.LogInformation($"length is {length}");
                // Sort Column Name  
                String sortColumn = formData["columns[" + formData["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                Logger.LogInformation($"sortcol is {sortColumn}");
                // Sort Column Direction ( asc ,desc)  
                String sortColumnDirection = formData["order[0][dir]"].FirstOrDefault();
                Logger.LogInformation($"sortcoldirection is {sortColumnDirection}");
                // Search Value from (Search box)  
                String searchValue = formData["search[value]"].FirstOrDefault();
                Logger.LogInformation($"searchvalue is {searchValue}");
                //Paging Size (10,20,50,100)  
                Int32 pageSize = length != null ? Convert.ToInt32(length) : 0;
                Int32 skip = start != null ? Convert.ToInt32(start) : 0;
                Int32 recordsTotal = 0;
                recordsTotal = queryData.Count();

                // Filtering
                if (whereClause != null)
                {
                    queryData = queryData.AsQueryable().Where(whereClause);
                }

                // Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    queryData = queryData.OrderBy(sortColumn, sortColumnDirection);
                }

                //Paging   
                queryData = queryData.Skip(skip).Take(pageSize).ToList();

                Logger.LogInformation($"querydata count is {queryData.Count()}");
                // Build the result 
                result = new DataTablesResult<T>
                         {
                             Data = queryData,
                             Draw = int.Parse(draw),
                             RecordsTotal = recordsTotal,
                             RecordsFiltered = queryData.Count()
                         };
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTablesResult<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        [JsonProperty("draw")]
        public Int32 Draw { get; set; }

        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        /// <value>
        /// The records filtered.
        /// </value>
        [JsonProperty("recordsFiltered")]
        public Int32 RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the records total.
        /// </summary>
        /// <value>
        /// The records total.
        /// </value>
        [JsonProperty("recordsTotal")]
        public Int32 RecordsTotal { get; set; }

        #endregion
    }
}