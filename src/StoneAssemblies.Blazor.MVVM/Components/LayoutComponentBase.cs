// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LayoutComponentBase.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components;

using System.ComponentModel;
using System.Reflection;

using Microsoft.AspNetCore.Components;

using StoneAssemblies.Blazor.MVVM.Components.Attributes;
using StoneAssemblies.Blazor.MVVM.Components.Extensions;
using StoneAssemblies.Blazor.MVVM.Services.Interfaces;
using StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces;

public class LayoutComponentBase<TViewModel> : Blorc.Components.BlorcLayoutComponentBase
    where TViewModel : class, IViewModel
{
    public LayoutComponentBase()
    {
        this.PropertyChanged += this.OnPropertyChanged;
    }

    [Parameter]
    public TViewModel? ViewModel
    {
        get => this.GetPropertyValue<TViewModel>(nameof(this.ViewModel));
        set => this.SetPropertyValue(nameof(this.ViewModel), value);
    }

    [Inject]
    private IViewModelFactory? ViewModelFactory
    {
        get => this.GetPropertyValue<IViewModelFactory?>(nameof(this.ViewModelFactory));
        set => this.SetPropertyValue(nameof(this.ViewModelFactory), value);
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.ViewModel))
        {
            this.InvokeAsync(
                async () =>
                {
                    await this.InitializeViewModelAsync();
                    this.StateHasChanged();
                });
        }
        else if (e.PropertyName == nameof(this.ViewModelFactory))
        {
            if (this.ViewModelFactory is not null)
            {
                this.ViewModel ??= this.ViewModelFactory.Create<TViewModel>();
            }
        }
        else
        {
            var viewModel = this.ViewModel;
            if (viewModel is not null)
            {
                var (propertyInfo, viewToViewModelAttribute) = this.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                    .Select(
                        (PropertyInfo info) => (PropertyInfo: info,
                                                   ViewToViewModelAttribute: info.GetCustomAttribute<ViewToViewModelAttribute>()))
                    .FirstOrDefault(((PropertyInfo PropertyInfo, ViewToViewModelAttribute ViewToViewModelAttribute) tuple) => tuple.PropertyInfo.Name == e.PropertyName);

                if (propertyInfo is not null && viewToViewModelAttribute is not null)
                {
                    var propertyName = viewToViewModelAttribute.PropertyName;
                    if (string.IsNullOrWhiteSpace(propertyName))
                    {
                        propertyName = propertyInfo.Name;
                    }

                    var viewModelProperty = viewModel.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .FirstOrDefault((PropertyInfo info) => info.Name == propertyName);

                    if (viewModelProperty is not null)
                    {
                        viewModelProperty.SetValue(viewModel, propertyInfo.GetValue(this));
                    }
                }
            }
        }
    }

    private async Task InitializeViewModelAsync()
    {
        if (this.ViewModel is null)
        {
            return;
        }

        this.ViewModel.InvokeAsync = this.InvokeAsync;
        this.ViewModel.PropertyChanged += this.OnViewModelPropertyChanged;

        this.MapViewToViewModelProperties();

        await this.ViewModel.InitializeAsync();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        this.InvokeAsync(this.StateHasChanged);
    }
}