// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewToViewModelAttribute.cs" company="Stone Assemblies">
// Copyright © 2023 - 2024 Stone Assemblies development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StoneAssemblies.Blazor.MVVM.Components.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ViewToViewModelAttribute : Attribute
{
    public ViewToViewModelAttribute(string propertyName = "")
    {
        this.PropertyName = propertyName;
    }

    public string PropertyName { get; }
}