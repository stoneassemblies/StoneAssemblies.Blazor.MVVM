// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelFactory.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Services;

using Microsoft.Extensions.DependencyInjection;

using StoneAssemblies.Blazor.MVVM.Services.Interfaces;
using StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces;

/// <summary>
/// The ViewModelFactory class.
/// </summary>
public class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">
    /// The service provider.
    /// </param>
    public ViewModelFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public TViewModel Create<TViewModel>(params object[] parameters)
        where TViewModel : IViewModel
    {
        return ActivatorUtilities.CreateInstance<TViewModel>(this.serviceProvider, parameters);
    }

    /// <inheritdoc/>
    public async Task<TViewModel> CreateAsync<TViewModel>(params object[] parameters)
        where TViewModel : IViewModel
    {
        var viewModel = ActivatorUtilities.CreateInstance<TViewModel>(this.serviceProvider, parameters);
        await viewModel.InitializeAsync();
        return viewModel;
    }
}