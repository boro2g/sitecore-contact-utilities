using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;

namespace Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues
{
    public class GetBooleanValues
    {
        public virtual void Process(GetFacetMemberValuesArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.MemberType, "args.MemberType");
            if (args.MemberType == typeof(bool))
            {
                args.Values.Add("true", "true");
                args.Values.Add("false", "false");
            }
        }
    }
}