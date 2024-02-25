string NuGetVersionV2 = "";
string SolutionFileName = "src/StoneAssemblies.Blazor.MVVM.sln";

string[] DockerFiles = System.Array.Empty<string>();

string[] OutputImages = System.Array.Empty<string>();

string[] ComponentProjects  = new [] {
	"src/StoneAssemblies.Blazor.MVVM/StoneAssemblies.Blazor.MVVM.csproj"
};

string TestProject = "src/StoneAssemblies.Blazor.MVVM.Tests/StoneAssemblies.Blazor.MVVM.Tests.csproj";

string SonarProjectKey = "stoneassemblies_StoneAssemblies.Blazor.MVVM";
string SonarOrganization = "stoneassemblies";