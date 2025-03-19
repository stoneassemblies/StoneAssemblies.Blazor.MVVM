namespace StoneAssemblies.Blazor.MVVM.Services.Interfaces;

using Microsoft.AspNetCore.Components;

public interface IComponentService<out TComponent>
    where TComponent : ComponentBase
{
    /// <summary>
    /// Gets the typed component.
    /// </summary>
    TComponent? Component { get; }
}