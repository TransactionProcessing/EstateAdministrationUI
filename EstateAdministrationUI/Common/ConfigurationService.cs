using Microsoft.Extensions.Configuration;

namespace EstateAdministrationUI.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Shared.Exceptions;
    using Shared.General;

    [ExcludeFromCodeCoverage]
    public class ConfigurationService : IConfigurationService
    {
        public Int32 GetDataReloadConfigInSeconds(String section,String screenName)
        {
            var defaultValue = ConfigurationReader.GetValueFromSection<Int32>("AppSettings:DataReloadConfig", "DefaultInSeconds");

            String configScreenName = $"{screenName}InSeconds";

            IConfigurationSection configSection = Startup.Configuration.GetSection($"AppSettings:DataReloadConfig:{section}");
            Int32 overrideValue = configSection.GetValue<Int32>(configScreenName);

            return overrideValue switch
            {
                0 => defaultValue,
                _ => overrideValue
            };
        }
    }
}