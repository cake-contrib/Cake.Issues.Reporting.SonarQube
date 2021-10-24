#load nuget:?package=Cake.Recipe&version=2.2.1
#addin nuget:?package=Cake.Git&version=1.1.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context, 
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Issues.Reporting.SonarQube",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Issues.Reporting.SonarQube",
    appVeyorAccountName: "cakecontrib",
    shouldRunDotNetCorePack: true,
    shouldRunDupFinder: false,
    shouldRunInspectCode: false,
    preferredBuildProviderType: BuildProviderType.GitHubActions);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Issues.Reporting.SonarQube.Tests/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.Issues]* -[Cake.Issues.Testing]* -[Cake.Issues.Reporting]* -[Shouldly]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
