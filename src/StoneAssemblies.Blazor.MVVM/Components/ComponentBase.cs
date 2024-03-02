// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBase.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components
{
    using System.ComponentModel;
    using System.Reflection;

    using Microsoft.AspNetCore.Components;

    using StoneAssemblies.Blazor.MVVM.Components.Attributes;
    using StoneAssemblies.Blazor.MVVM.Components.Extensions;
    using StoneAssemblies.Blazor.MVVM.Services.Interfaces;

    public class ComponentBase<TViewModel> : Blorc.Components.BlorcComponentBase
        where TViewModel : class, ViewModels.Interfaces.IViewModel
    {
        public ComponentBase(bool injectComponentServices)
            : base(injectComponentServices)
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
                this.InvokeAsync(async () =>
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
                        .Select(info => (PropertyInfo: info, ViewToViewModelAttribute: info.GetCustomAttribute<ViewToViewModelAttribute>()))
                        .FirstOrDefault(tuple => tuple.PropertyInfo.Name == e.PropertyName);

                    if (propertyInfo is not null && viewToViewModelAttribute is not null)
                    {
                        var viewModelProperty = viewModel.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .FirstOrDefault(info => info.Name == viewToViewModelAttribute.PropertyName);

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
}