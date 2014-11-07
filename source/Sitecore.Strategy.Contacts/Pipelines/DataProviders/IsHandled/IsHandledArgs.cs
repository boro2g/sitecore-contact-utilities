using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.DataProviders;
using Sitecore.Pipelines;
using Sitecore.Data;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.IsHandled
{
    public class IsHandledArgs : PipelineArgs
    {
        public IsHandledArgs(ID itemId, CallContext context)
        {
            this.ItemId = itemId;
            this.Context = context;
        }
        public ID ItemId { get; private set; }
        public CallContext Context { get; private set; }
        public bool IsHandled { get; set; }
        public bool IncludeAllIds { get; set; }
    }
}