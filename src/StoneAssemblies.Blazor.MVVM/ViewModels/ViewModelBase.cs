// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Blorc.Data;
    using Blorc.MVVM;

    /// <summary>
    /// The view model base.
    /// </summary>
    public class ViewModelBase : IViewModel
    {
        /// <summary>
        /// The property bag.
        /// </summary>
        private readonly PropertyBag propertyBag = new PropertyBag();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase()
        {
            this.propertyBag.PropertyChanged += this.OnPropertyBagPropertyChanged;
        }

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the notify property change notification is enabled.
        /// </summary>
        public bool IsPropertyNotificationEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the invoke async.
        /// </summary>
        /// <remarks>TODO: Create a dispatcher service for this.</remarks>
        public Func<Action, Task>? InvokeAsync { get; set; }

        /// <summary>
        /// The initialize async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets property value.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="TValue">
        /// The value type.
        /// </typeparam>
        /// <returns>
        /// The value.
        /// </returns>
        protected TValue GetPropertyValue<TValue>(string propertyName)
        {
            return this.propertyBag.GetValue<TValue>(propertyName);
        }

        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="TValue">
        /// The value type.
        /// </typeparam>
        protected void SetPropertyValue<TValue>(string propertyName, TValue value)
        {
            this.propertyBag.SetValue<TValue?>(propertyName, value);
        }

        /// <summary>
        /// Called on property bag property changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnPropertyBagPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (this.IsPropertyNotificationEnabled)
            {
                this.OnPropertyChanged(e.PropertyName);
            }
        }
    }
}