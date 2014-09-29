using Sitecore.Diagnostics;
using Sitecore.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Strategy.Contacts.DataProviders;
using Sitecore.Data;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetParentID
{
    public abstract class GetParentIDProcessorBase
    {
        public GetParentIDProcessorBase()
        {
            this.ParentIds = new Dictionary<ID, ID>();
        }

        protected Dictionary<ID, ID> ParentIds { get; private set; }
        
        public virtual void Process(GetParentIDArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.ItemDefinition, "args.ItemDefinition");
            Assert.ArgumentNotNull(args.Context, "args.Context");
            if (! ID.IsNullOrEmpty(args.ParentId))
            {
                return;
            }
            var itemId = args.ItemDefinition.ID;
            if (! ParentIds.ContainsKey(itemId))
            {
                return;
            }
            args.ParentId = ParentIds[itemId];
        }
    }
}