namespace Cake.Issues.Reporting.SonarQube
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.Reporting;

    /// <summary>
    /// Contains functionality to generate files compatible with SonarQubes generic issue data format.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class SonarQubeIssueReportFormatAliases
    {
        /// <summary>
        /// Gets an instance of the SonarQube report format using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for generating the report.</param>
        /// <returns>Instance of a SonarQube report format.</returns>
        /// <example>
        /// <para>Create file in SonarQube generic issue data format:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         SonarQubeIssueReportFormatSettings();
        ///
        ///     CreateIssueReport(
        ///         issues,
        ///         SonarQubeIssueReportFormat(settings),
        ///         @"c:\repo",
        ///         @"c:\export.json");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
        public static IIssueReportFormat SonarQubeIssueReportFormat(
            this ICakeContext context,
            SonarQubeIssueReportFormatSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new SonarQubeIssueReportGenerator(context.Log, settings);
        }
    }
}
