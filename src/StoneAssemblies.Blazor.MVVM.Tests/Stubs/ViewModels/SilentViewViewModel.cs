namespace StoneAssemblies.Blazor.MVVM.Tests.Stubs.ViewModels;

using StoneAssemblies.Blazor.MVVM.ViewModels;

public class SilentViewViewModel : ViewModelBase
{
    public string Name
    {
        get => this.GetPropertyValue<string>(nameof(this.Name));
        set => this.SetPropertyValue(nameof(this.Name), value);
    }

    public int Id
    {
        get => this.GetPropertyValue<int>(nameof(this.Id));
        set => this.SetPropertyValue(nameof(this.Id), value);
    }
}