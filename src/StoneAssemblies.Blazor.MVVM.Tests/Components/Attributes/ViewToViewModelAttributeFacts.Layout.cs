namespace StoneAssemblies.Blazor.MVVM.Tests.Components.Attributes;

using Blorc.Services;

using Bunit;

using FluentAssertions;

using StoneAssemblies.Blazor.MVVM.Extensions;
using StoneAssemblies.Blazor.MVVM.Tests.Stubs.Components;

using Xunit;

public partial class ViewToViewModelAttributeFacts
{
    public class Used_In_Layouts
    {
        [Fact]
        [Trait(Traits.Category, TestCategory.Unit)]
        public void Sets_Values_In_ViewModel_As_Expected()
        {
            // Arrange
            using var ctx = new TestContext();

            ctx.Services.AddBlorcCore();
            ctx.Services.AddStoneAssembliesMVVMServices();

            using var renderedComponent =
                ctx.RenderComponent<SilentLayout>(builder =>
                {
                    builder.Add(demo => demo.Id, 2);
                    builder.Add(demo => demo.FullName, "Jane Doe");
                });

            var renderedComponentInstance = renderedComponent.Instance;
            renderedComponentInstance.ViewModel?.Id.Should().Be(renderedComponent.Instance.Id);
            renderedComponentInstance.ViewModel?.Name.Should().Be(renderedComponent.Instance.FullName);
        }

        [Fact]
        [Trait(Traits.Category, TestCategory.Unit)]
        public void Updates_Values_In_ViewModel_As_Expected()
        {
            // Arrange
            using var ctx = new TestContext();

            ctx.Services.AddBlorcCore();
            ctx.Services.AddStoneAssembliesMVVMServices();

            using var renderedComponent =
                ctx.RenderComponent<SilentLayout>(builder =>
                {
                    builder.Add(demo => demo.Id, 2);
                    builder.Add(demo => demo.FullName, "Jane Doe");
                });

            var instance = renderedComponent.Instance;

#pragma warning disable BL0005
            instance.Id = 3;
            instance.FullName = "John Doe";
#pragma warning restore BL0005

            instance.ViewModel?.Id.Should().Be(instance.Id);
            instance.ViewModel?.Name.Should().Be(instance.FullName);
        }
    }
}