using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Threading;
using EstateAdministrationUI.Common;

namespace EstateAdministrationUI.Areas.Estate.Controllers
{
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Area("Estate")]
    public class DataReloadConfigurationController : Controller
    {
        private readonly IConfigurationService ConfigurationService;

        public DataReloadConfigurationController(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRetryTimeout([FromQuery] String sectionName, [FromQuery] String screenName, CancellationToken cancellationToken)
        {
            int retryTimeoutInSeconds = this.ConfigurationService.GetDataReloadConfigInSeconds(sectionName, screenName);
            
            return this.Ok(TimeSpan.FromSeconds(retryTimeoutInSeconds).TotalMilliseconds);
        }
    }
}
