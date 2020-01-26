namespace Cake.Issues.Reporting.SonarQube.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Cake.Testing;
    using Shouldly;

    internal class SonarQubeIssueReportFixture
    {
        public const string RepositoryRootPath = @"c:\Source\Cake.Issues.Reporting.SonarQube";

        public SonarQubeIssueReportFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.SonarQubeIssueReportFormatSettings = new SonarQubeIssueReportFormatSettings();
        }

        public FakeLog Log { get; set; }

        public SonarQubeIssueReportFormatSettings SonarQubeIssueReportFormatSettings { get; set; }

        public string CreateReport(IEnumerable<IIssue> issues)
        {
            var generator =
                new SonarQubeIssueReportGenerator(this.Log, this.SonarQubeIssueReportFormatSettings);

            var reportFile = Path.GetTempFileName();
            try
            {
                var createIssueReportSettings =
                    new CreateIssueReportSettings(RepositoryRootPath, reportFile);
                generator.Initialize(createIssueReportSettings);
                generator.CreateReport(issues);

                using (var stream = new FileStream(reportFile, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (File.Exists(reportFile))
                {
                    File.Delete(reportFile);
                }
            }
        }

        public void TestReportCreation(Action<SonarQubeIssueReportFormatSettings> settings)
        {
            // Given
            settings(this.SonarQubeIssueReportFormatSettings);

            // When
            var result =
                this.CreateReport(
                    new List<IIssue>
                    {
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Reporting.SonarQube.Tests\Foo.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                    });

            // Then
            // Currently only checks if genertions failed or not without checking actual output.
            result.ShouldNotBeNull();
        }
    }
}
