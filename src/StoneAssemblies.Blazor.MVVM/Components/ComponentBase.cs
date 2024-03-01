// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBase.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components
{
    using System.ComponentModel;

    using Microsoft.AspNetCore.Components;

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
        private IViewModelFactory? ViewModelFactory { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await this.InitializeViewModelAsync();
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
        }

        private async Task InitializeViewModelAsync()
        {
            if (this.ViewModelFactory is null)
            {
                return;
            }

            this.ViewModel ??= this.ViewModelFactory.Create<TViewModel>();

            this.MapViewToViewModelProperties();
            this.ViewModel.PropertyChanged += this.OnViewModelPropertyChanged;
            this.ViewModel.InvokeAsync = this.InvokeAsync;

            await this.ViewModel.InitializeAsync();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.InvokeAsync(this.StateHasChanged);
        }
    }
}