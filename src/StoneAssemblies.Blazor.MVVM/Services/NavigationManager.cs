// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationManager.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

using StoneAssemblies.Blazor.MVVM.Services.Interfaces;

public class NavigationManager : INavigationManager
{
    private readonly Microsoft.AspNetCore.Components.NavigationManager navigationManager;

    public NavigationManager(Microsoft.AspNetCore.Components.NavigationManager navigationManager)
    {
        ArgumentNullException.ThrowIfNull(navigationManager);

        this.navigationManager = navigationManager;
        this.navigationManager.LocationChanged += (sender, args) =>
        {
            this.OnLocationChanged(args);
        };
    }

    /// <inheritdoc/>
    public void NavigateTo(string uri, bool forceLoad)
    {
        this.navigationManager.NavigateTo(uri, forceLoad);
    }

    /// <inheritdoc/>
    public void NavigateTo(string uri, bool forceLoad = false, bool replace = false)
    {
        this.navigationManager.NavigateTo(uri, forceLoad, replace);
    }

    /// <inheritdoc/>
    public void NavigateTo(string uri, NavigationOptions options)
    {
        this.navigationManager.NavigateTo(uri, options);
    }

    /// <inheritdoc/>
    public Uri ToAbsoluteUri(string relativeUri)
    {
        return this.navigationManager.ToAbsoluteUri(relativeUri);
    }

    /// <inheritdoc/>
    public string ToBaseRelativePath(string uri)
    {
        return this.navigationManager.ToBaseRelativePath(uri);
    }

    /// <inheritdoc/>
    public string BaseUri => this.navigationManager.BaseUri;

    /// <inheritdoc/>
    public string Uri => this.navigationManager.Uri;

    /// <inheritdoc/>
    public event EventHandler<LocationChangedEventArgs> LocationChanged;

    protected virtual void OnLocationChanged(LocationChangedEventArgs e)
    {
        this.LocationChanged?.Invoke(this, e);
    }
}