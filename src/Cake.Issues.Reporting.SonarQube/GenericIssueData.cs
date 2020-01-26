namespace Cake.Issues.Reporting.SonarQube
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

#pragma warning disable SA1401 // Fields must be private
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1402 // File may only contain a single class
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable SA1649 // File name must match first type name

    [DataContract]
    internal class GenericIssueData
    {
        [DataMember]
        public IEnumerable<GenericIssueDataIssue> issues;

        public GenericIssueData(IEnumerable<IIssue> issues)
        {
            this.issues = issues.Select(x => x.ToGenericIssueDataIssue());
        }
    }

    [DataContract]
    internal class GenericIssueDataIssue
    {
        [DataMember]
        public string engineId;

        [DataMember]
        public string ruleId;

        [DataMember]
        public GenericIssueDataLocation primaryLocation;

        [DataMember]
        public string type;

        [DataMember]
        public string severity;
    }

    [DataContract]
    internal class GenericIssueDataLocation
    {
        [DataMember]
        public string message;

        [DataMember]
        public string filePath;

        [DataMember]
        public GenericIssueDataTextRange textRange;
    }

    [DataContract]
    internal class GenericIssueDataTextRange
    {
        [DataMember]
        public int startLine;
    }

#pragma warning restore SA1401 // Fields must be private
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore SA1649 // File name must match first type name
}
