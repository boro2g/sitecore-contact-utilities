using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Pipelines;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Collections;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetItemVersions
{
    public class GetItemVersionsArgs : PipelineArgs
    {
        public GetItemVersionsArgs(ItemDefinition itemDefinition, CallContext context)
        {
            this.ItemDefinition = itemDefinition;
            this.Context = context;
        }
        public ItemDefinition ItemDefinition { get; private set; }
        public CallContext Context { get; private set; }
        public VersionUriList VersionUriList { get; set; } 
    }
}