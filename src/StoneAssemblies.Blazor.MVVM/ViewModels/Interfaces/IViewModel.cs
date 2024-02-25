// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewModel.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.ViewModels.Interfaces
{
    public interface IViewModel : Blorc.MVVM.IViewModel
    {
        Func<Action, Task> InvokeAsync { get; set; }

        Task InitializeAsync();
    }
}