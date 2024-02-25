// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    using StoneAssemblies.Blazor.MVVM.Services.Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static void AddStoneAssembliesMVVMServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IViewModelFactory, IViewModelFactory>();
        }
    }
}