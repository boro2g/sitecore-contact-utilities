using System;
using System.Collections.Generic;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues;
using Sitecore.Strategy.Contacts.DataProviders;
using Sitecore.Strategy.Contacts.Sample.Model;

namespace Sitecore.Strategy.Contacts.Sample.Pipelines.GetFacetMemberValues
{
    public class GetEmployeeLocationValues
    {
        public virtual void Process(GetFacetMemberValuesArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.FacetName, "args.FacetName");
            var type = ContactFacetHelper.GetContractTypeForFacet(args.FacetName);
            if (type == null)
            {
                return;
            }
            if (! typeof(IEmployeeData).IsAssignableFrom(type))
            {
                return;
            }
            if (args.MemberName == "Location")
            {
                args.Values.Add("NA", "North America");
                args.Values.Add("SA", "South America");
                args.Values.Add("EU", "Europe");
                args.Values.Add("APAC", "Asia-Pacific");
            }
        }
    }
}