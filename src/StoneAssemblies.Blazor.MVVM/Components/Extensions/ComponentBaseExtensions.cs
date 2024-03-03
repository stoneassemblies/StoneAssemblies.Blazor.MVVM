// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBaseExtensions.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components.Extensions;

using System.Reflection;

using StoneAssemblies.Blazor.MVVM.Components.Attributes;
using StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces;

public static class ComponentBaseExtensions
{
    public static void MapViewToViewModelProperties<TViewModel>(this ComponentBase<TViewModel> component)
        where TViewModel : class, IViewModel
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

    public static void MapViewToViewModelProperty<TViewModel>(this ComponentBase<TViewModel> component, string? viewPropertyName)
        where TViewModel : class, IViewModel
    {
        var (propertyInfo, viewToViewModelAttribute) = component.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).Select(
                info => (PropertyInfo: info,
                            ViewToViewModelAttribute: info.GetCustomAttribute<ViewToViewModelAttribute>()))
            .FirstOrDefault(tuple => tuple.PropertyInfo.Name == viewPropertyName);

        if (propertyInfo is not null && viewToViewModelAttribute is not null)
        {
            var propertyName = viewToViewModelAttribute.PropertyName;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                propertyName = propertyInfo.Name;
            }

            var componentViewModel = component.ViewModel;
            if (componentViewModel is null)
            {
                return;
            }

            var viewModelProperty = componentViewModel.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(info => info.Name == propertyName);

            if (viewModelProperty is not null)
            {
                viewModelProperty.SetValue(componentViewModel, propertyInfo.GetValue(component));
            }
        }
    }
}