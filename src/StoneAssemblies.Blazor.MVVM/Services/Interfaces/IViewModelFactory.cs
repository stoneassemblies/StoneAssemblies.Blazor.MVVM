// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewModelFactory.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Services.Interfaces;

using StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces;

/// <summary>
/// The IViewModelFactory interface.
/// </summary>
public interface IViewModelFactory
{
    /// <summary>
    /// Creates a view model.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <typeparam name="TViewModel">
    /// The view model type.
    /// </typeparam>
    /// <returns>
    /// The view model.
    /// </returns>
    TViewModel Create<TViewModel>(params object[] parameters)
        where TViewModel : IViewModel;

    /// <summary>
    /// Creates a view model and initialize the view model.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <typeparam name="TViewModel">
    /// The view model type.
    /// </typeparam>
    /// <returns>
    /// The view model.
    /// </returns>
    Task<TViewModel> CreateAsync<TViewModel>(params object[] parameters)
        where TViewModel : IViewModel;
}