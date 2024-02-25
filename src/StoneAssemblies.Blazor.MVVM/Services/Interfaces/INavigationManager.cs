// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationManager.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Services.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

public interface INavigationManager
{
    void NavigateTo(string uri, bool forceLoad);

    void NavigateTo(string uri, bool forceLoad = false, bool replace = false);

    void NavigateTo(string uri, NavigationOptions options);

    Uri ToAbsoluteUri(string relativeUri);

    string ToBaseRelativePath(string uri);

    string BaseUri { get; }

    string Uri { get; }

    event EventHandler<LocationChangedEventArgs> LocationChanged;
}