using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Pipelines;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Collections;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetItemDefinition
{
    public class GetItemDefinitionArgs : PipelineArgs
    {
        public GetItemDefinitionArgs(ID itemId, CallContext context)
        {
            this.ItemId = itemId;
            this.Context = context;
        }
        public ID ItemId { get; private set; }
        public CallContext Context { get; private set; }
        public ItemDefinition ItemDefinition { get; set; } 
    }
}