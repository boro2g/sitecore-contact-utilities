using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Pipelines;

namespace Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues
{
    public class GetFacetMemberValuesArgs : PipelineArgs
    {
        public GetFacetMemberValuesArgs(string facetName, string memberName)
        {
            this.FacetName = facetName;
            this.MemberName = memberName;
            this.Values = new Dictionary<string, string>();
        }
        public string FacetName { get; private set; }
        public string MemberName { get; private set; }
        public Type MemberType { get; set; }
        public Dictionary<string, string> Values { get; private set; }
    }
}