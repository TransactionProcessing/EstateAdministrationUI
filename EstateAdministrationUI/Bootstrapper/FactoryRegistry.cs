namespace EstateAdministrationUI.Bootstrapper;

using BusinessLogic.Factories;
using Factories;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

public class FactoryRegistry : ServiceRegistry
{
    public FactoryRegistry()
    {
        this.AddSingleton<IModelFactory, ModelFactory>();
        this.AddSingleton<IViewModelFactory, ViewModelFactory>();
    }
}