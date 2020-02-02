namespace Cake.Issues.Reporting.SonarQube.Tests
{
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IIssueExtensionsTests
    {
        public sealed class TheToGenericIssueDataIssueMethod
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                IIssue issue = null;

                // When
                var result = Record.Exception(() => issue.ToGenericIssueDataIssue());

                // Then
                result.IsArgumentNullException("issue");
            }

            [Theory]
            [InlineData("providerName")]
            public void Should_Set_EngineId(string providerName)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", providerName)
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.engineId.ShouldBe(providerName);
            }

            [Theory]
            [InlineData("ruleId")]
            [InlineData("")]
            [InlineData(null)]
            public void Should_Set_RuleId(string ruleName)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .OfRule(ruleName)
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.ruleId.ShouldBe(ruleName);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue(message, "providerType", "providerName")
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.primaryLocation.message.ShouldBe(message);
            }

            [Theory]
            [InlineData("src/foo/bar.cs", "src/foo/bar.cs")]
            [InlineData("", null)]
            [InlineData(null, null)]
            public void Should_Set_FilePath(string filePath, string expectedFilePath)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile(filePath)
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.primaryLocation.filePath.ShouldBe(expectedFilePath);
            }

            [Theory]
            [InlineData(IssuePriority.Undefined, "CODE_SMELL")]
            [InlineData(IssuePriority.Hint, "CODE_SMELL")]
            [InlineData(IssuePriority.Suggestion, "CODE_SMELL")]
            [InlineData(IssuePriority.Warning, "CODE_SMELL")]
            [InlineData(IssuePriority.Error, "CODE_SMELL")]
            public void Should_Set_Type(IssuePriority priority, string expectedType)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority)
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.type.ShouldBe(expectedType);
            }

            [Theory]
            [InlineData(IssuePriority.Undefined, "INFO")]
            [InlineData(IssuePriority.Hint, "INFO")]
            [InlineData(IssuePriority.Suggestion, "MINOR")]
            [InlineData(IssuePriority.Warning, "MAJOR")]
            [InlineData(IssuePriority.Error, "CRITICAL")]
            public void Should_Set_Severity(IssuePriority priority, string expectedSeverity)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .WithPriority(priority)
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.severity.ShouldBe(expectedSeverity);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_StartLine(int? line)
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("message", "providerType", "providerName")
                        .InFile("src/foo/bar.cs", line)
                        .Create();

                // When
                var result = issue.ToGenericIssueDataIssue();

                // Then
                result.primaryLocation.textRange.startLine.ShouldBe(line.Value);
            }
        }
    }
}