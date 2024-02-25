// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LayoutComponentBaseExtensions.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components.Extensions;

using System.Reflection;

using StoneAssemblies.Blazor.MVVM.Components;
using StoneAssemblies.Blazor.MVVM.Components.Attributes;
using StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces;

public static class LayoutComponentBaseExtensions
{
    public static void MapViewToViewModelProperties<TViewModel>(this LayoutComponentBase<TViewModel> component)
        where TViewModel : IViewModel
    {
        var viewToViewModelProperties = component.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).Select(
                info => (PropertyInfo: info,
                            ViewToViewModelAttribute: info.GetCustomAttribute<ViewToViewModelAttribute>()));

        foreach (var tuple in viewToViewModelProperties)
        {
            var viewProperty = tuple.PropertyInfo;
            if (tuple.ViewToViewModelAttribute is not null)
            {
                var propertyName = tuple.ViewToViewModelAttribute.PropertyName;
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    propertyName = viewProperty.Name;
                }

                var viewModelProperty = component.ViewModel?.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(info => info.Name == propertyName);

                if (viewModelProperty is not null)
                {
                    viewModelProperty.SetValue(component.ViewModel, viewProperty.GetValue(component));
                }
            }
        }
    }
}