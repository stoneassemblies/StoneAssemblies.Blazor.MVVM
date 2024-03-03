// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBase.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components
{
    using System.ComponentModel;

    using Blorc.Components;

    using Microsoft.AspNetCore.Components;

    using StoneAssemblies.Blazor.MVVM.Components.Extensions;
    using StoneAssemblies.Blazor.MVVM.Services.Interfaces;

    public partial class ComponentBase<TViewModel> : BlorcComponentBase where TViewModel : class, ViewModels.Interfaces.IViewModel
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
            get => this.GetPropertyValue<IViewModelFactory>(nameof(this.ViewModelFactory));
            set => this.SetPropertyValue(nameof(this.ViewModelFactory), value);
        }

        protected override bool ShouldRender()
        {
            return this.ViewModel is not null && base.ShouldRender();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.ViewModel) || e.PropertyName == nameof(this.ViewModelFactory))
            {
                this.InvokeAsync(async () =>
                {
                    await this.InitializeViewModelAsync();
                    this.StateHasChanged();
                });
            }
            else if (e.PropertyName != nameof(this.ViewModel) && this.ViewModel is not null)
            {
                this.MapViewToViewModelProperty(e.PropertyName);
            }
        }

        private async Task InitializeViewModelAsync()
        {
            if (this.ViewModelFactory is null)
            {
                return;
            }

            this.ViewModel ??= this.ViewModelFactory.Create<TViewModel>();
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