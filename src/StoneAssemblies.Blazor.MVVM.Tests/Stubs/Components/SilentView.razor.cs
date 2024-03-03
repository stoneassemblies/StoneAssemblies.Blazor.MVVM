namespace StoneAssemblies.Blazor.MVVM.Tests.Stubs.Components
{
    using Microsoft.AspNetCore.Components;

    using StoneAssemblies.Blazor.MVVM.Components.Attributes;
    using StoneAssemblies.Blazor.MVVM.Tests.Stubs.ViewModels;

    public partial class SilentView
    {
        public SilentView()
            : base(true)
        {
        }

        [Parameter]
        [ViewToViewModel]
        public int Id
        {
            get => this.GetPropertyValue<int>(nameof(this.Id));
            set => this.SetPropertyValue(nameof(this.Id), value);
        }

        [Parameter]
        [ViewToViewModel(propertyName: nameof(SilentViewViewModel.Name))]
        public string FullName
        {
            get => this.GetPropertyValue<string>(nameof(this.FullName));
            set => this.SetPropertyValue(nameof(this.FullName), value);
        }
    }
}
