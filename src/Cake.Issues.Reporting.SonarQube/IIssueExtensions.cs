namespace Cake.Issues.Reporting.SonarQube
{
    /// <summary>
    /// Extensions for <see cref="IIssue"/>.
    /// </summary>
    internal static class IIssueExtensions
    {
        /// <summary>
        /// Converts an <see cref="IIssue"/> to an <see cref="GenericIssueDataIssue"/>.
        /// </summary>
        /// <param name="issue">Issue to convert.</param>
        /// <returns>Converted issue.</returns>
        public static GenericIssueDataIssue ToGenericIssueDataIssue(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            var result =
                new GenericIssueDataIssue
                {
                    engineId = issue.ProviderName,
                    ruleId = issue.Rule,
                    primaryLocation =
                        new GenericIssueDataLocation
                        {
                            message = issue.MessageText,
                            filePath = issue.AffectedFileRelativePath?.FullPath,
                        },
                    type = issue.Priority.ToGenericIssueDataType(),
                    severity = issue.Priority.ToGenericIssueDataSeverity(),
                };

            if (issue.Line.HasValue)
            {
                result.primaryLocation.textRange =
                    new GenericIssueDataTextRange
                    {
                        startLine = issue.Line.Value,
                    };
            }

            return result;
        }

        private static string ToGenericIssueDataType(this int? priority)
        {
            if (priority == null)
            {
                return "CODE_SMELL";
            }

            switch (priority)
            {
                case (int)IssuePriority.Hint:
                case (int)IssuePriority.Suggestion:
                case (int)IssuePriority.Warning:
                case (int)IssuePriority.Error:
                case (int)IssuePriority.Undefined:
                default:
                    return "CODE_SMELL";
            }
        }

        private static string ToGenericIssueDataSeverity(this int? priority)
        {
            if (priority == null)
            {
                return "INFO";
            }

            switch (priority)
            {
                case (int)IssuePriority.Hint:
                    return "INFO";
                case (int)IssuePriority.Suggestion:
                    return "MINOR";
                case (int)IssuePriority.Warning:
                    return "MAJOR";
                case (int)IssuePriority.Error:
                    return "CRITICAL";
                case (int)IssuePriority.Undefined:
                default:
                    return "INFO";
            }
        }
    }
}
