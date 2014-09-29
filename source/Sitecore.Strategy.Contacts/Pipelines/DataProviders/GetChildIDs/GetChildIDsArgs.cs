using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Pipelines;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Collections;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetChildIDs
{
    public class GetChildIDsArgs : PipelineArgs
    {
        public GetChildIDsArgs(ItemDefinition itemDefinition, CallContext context)
        {
            this.ItemDefinition = itemDefinition;
            this.Context = context;
            this.IDList = new IDList();
        }
        public ItemDefinition ItemDefinition { get; private set; }
        public CallContext Context { get; private set; }
        public IDList IDList { get; private set; } 
    }
}