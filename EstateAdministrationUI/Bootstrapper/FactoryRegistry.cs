namespace EstateAdministrationUI.Bootstrapper;

using BusinessLogic.Factories;
using Factories;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class FactoryRegistry : ServiceRegistry
{
    public FactoryRegistry()
    {
        this.AddSingleton<IModelFactory, ModelFactory>();
        this.AddSingleton<IViewModelFactory, ViewModelFactory>();
    }
}