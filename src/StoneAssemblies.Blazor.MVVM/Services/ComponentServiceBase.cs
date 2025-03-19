namespace StoneAssemblies.Blazor.MVVM.Services;

using Blorc.Services;

using Microsoft.AspNetCore.Components;

using StoneAssemblies.Blazor.MVVM.Services.Interfaces;

public class ComponentServiceBase<TComponent> : IComponentService, IComponentService<TComponent>
    where TComponent : ComponentBase
{
    /// <summary>
    /// Gets or sets the component.
    /// </summary>
    ComponentBase? IComponentService.Component { get; set; }

    /// <summary>
    /// Gets the typed component.
    /// </summary>
    public TComponent? Component => (this as IComponentService).Component as TComponent;
}