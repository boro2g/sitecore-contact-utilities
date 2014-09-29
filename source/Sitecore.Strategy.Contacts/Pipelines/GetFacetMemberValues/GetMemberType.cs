using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.DataProviders;

namespace Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues
{
    public class GetMemberType
    {
        public virtual void Process(GetFacetMemberValuesArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.FacetName, "args.FacetName");
            Assert.ArgumentNotNullOrEmpty(args.MemberName, "args.MemberName");
            args.MemberType = ContactFacetHelper.GetFacetMemberType(args.FacetName, args.MemberName);
        }

    }
}