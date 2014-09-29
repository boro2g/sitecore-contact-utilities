using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.DataProviders;
using Sitecore.Rules;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.IsHandled
{
    public class TemplatesProcessor
    {
        public TemplatesProcessor()
        {
            if (Ids == null)
            {
                Ids = new List<ID>();
            }
            Ids.Add(Sitecore.ItemIDs.TemplateRoot);
            Ids.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactsTemplatesFolder);
            Ids.Add(Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetTemplate);
        }
        protected static List<ID> Ids { get; private set; }
        public virtual void Process(IsHandledArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.ItemId, "args.ItemId");
            if (! args.IsHandled && Ids.Contains(args.ItemId))
            {
                args.IsHandled = true;
            }
        }
    }
}