using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Pipelines;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Collections;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetParentID
{
    public class GetParentIDArgs : PipelineArgs
    {
        public GetParentIDArgs(ItemDefinition itemDefinition, CallContext context)
        {
            this.ItemDefinition = itemDefinition;
            this.Context = context;
        }
        public ItemDefinition ItemDefinition { get; set; }
        public CallContext Context { get; private set; }
        public ID ParentId { get; set; }
    }
}