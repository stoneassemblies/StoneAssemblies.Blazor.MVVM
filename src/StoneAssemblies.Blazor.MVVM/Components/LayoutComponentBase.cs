﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LayoutComponentBase.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components
{
    using System.ComponentModel;

    using Microsoft.AspNetCore.Components;

    using StoneAssemblies.Blazor.MVVM.Components.Extensions;
    using StoneAssemblies.Blazor.MVVM.Services.Interfaces;
    using StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces;

    public class LayoutComponentBase<TViewModel> : Blorc.Components.BlorcLayoutComponentBase
        where TViewModel : IViewModel
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
        private IViewModelFactory? ViewModelFactory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.InitializeViewModelAsync();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.ViewModel))
            {
                this.InvokeAsync(this.StateHasChanged);
            }
        }

        private async Task InitializeViewModelAsync()
        {
            if (this.ViewModelFactory is null)
            {
                return;
            }

            this.ViewModel = this.ViewModelFactory.Create<TViewModel>();
            this.MapViewToViewModelProperties();
            this.ViewModel.PropertyChanged += this.OnViewModelPropertyChanged;

            await this.ViewModel.InitializeAsync();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.InvokeAsync(this.StateHasChanged);
        }
    }
}