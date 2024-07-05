namespace EstateAdministrationUI.Common
{
    using System;

    public interface IConfigurationService
    {
        Int32 GetDataReloadConfigInSeconds(String section, String screenName);
    }
}