using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.Pipelines.DataProviders.IsHandled;
using Sitecore.Pipelines;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetItemDefinition
{
    public class CheckIfHandled
    {
        public virtual void Process(GetItemDefinitionArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.ItemId, "args.ItemId");
            var args2 = new IsHandledArgs(args.ItemId);
            CorePipeline.Run("contactFacetDataProvider.isHandled", args2);
            if (!args2.IsHandled)
            {
                args.AbortPipeline();
            }
        }
    }
}