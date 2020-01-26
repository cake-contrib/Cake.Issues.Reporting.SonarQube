namespace Cake.Issues.Reporting.SonarQube.Tests
{
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class SonarQubeIssueReportGeneratorTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SonarQubeIssueReportGenerator(
                        null,
                        new SonarQubeIssueReportFormatSettings()));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new SonarQubeIssueReportGenerator(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheInternalCreateReportMethod
        {
            [Fact]
            public void Should_Generate_Report()
            {
                // Given
                var fixture = new SonarQubeIssueReportFixture();
                var issues =
                    new List<IIssue>
                    {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.SonarQube.Tests\SonarQubeIssueReportGeneratorTests.cs", 10)
                            .InProjectFile(@"src\Cake.Issues.Reporting.SonarQube.Tests\Cake.Issues.Reporting.SonarQube.Tests.csproj")
                            .OfRule("Rule Foo")
                            .WithPriority(IssuePriority.Error)
                            .Create(),
                    };

                // When
                var reportContents = fixture.CreateReport(issues);

                // Then
                reportContents.ShouldNotBeEmpty();
            }
        }
    }
}
