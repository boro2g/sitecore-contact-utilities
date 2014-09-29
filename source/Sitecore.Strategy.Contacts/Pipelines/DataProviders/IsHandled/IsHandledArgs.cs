using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Pipelines;
using Sitecore.Data;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.IsHandled
{
    public class IsHandledArgs : PipelineArgs
    {
        public IsHandledArgs(ID itemId)
        {
            this.ItemId = itemId;
        }
        public ID ItemId { get; private set; }
        public bool IsHandled { get; set; }
        public bool IncludeAllIds { get; set; }
    }
}