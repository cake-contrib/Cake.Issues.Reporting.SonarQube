﻿namespace Cake.Issues.Reporting.SonarQube
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Generator for creating reports compatible with SonarQubes generic issue data format.
    /// </summary>
    internal class SonarQubeIssueReportGenerator : IssueReportFormat
    {
        private readonly SonarQubeIssueReportFormatSettings sonarqubeIssueReportFormatSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SonarQubeIssueReportGenerator"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public SonarQubeIssueReportGenerator(ICakeLog log, SonarQubeIssueReportFormatSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.sonarqubeIssueReportFormatSettings = settings;
        }

        /// <inheritdoc />
        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            this.Log.Information("Creating report '{0}'", this.Settings.OutputFilePath.FullPath);

            // TODO Implement
            return null;
        }
    }
}