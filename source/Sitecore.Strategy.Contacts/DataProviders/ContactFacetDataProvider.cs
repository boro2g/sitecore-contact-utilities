using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetChildIDs;
using Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetItemDefinition;
using Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetItemFields;
using Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetItemVersions;
using Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetParentID;

namespace Sitecore.Strategy.Contacts.DataProviders
{
    public class ContactFacetDataProvider : DataProvider
    {
        public override IDList GetChildIDs(ItemDefinition itemDefinition, CallContext context)
        {
            var args = new GetChildIDsArgs(itemDefinition, context);
            CorePipeline.Run("contactFacetDataProvider.getChildIDs", args);
            return args.IDList;
        }

        public override VersionUriList GetItemVersions(ItemDefinition itemDefinition, CallContext context)
        {
            var args = new GetItemVersionsArgs(itemDefinition, context);
            CorePipeline.Run("contactFacetDataProvider.getItemVersions", args);
            return args.VersionUriList;
        }

        public override LanguageCollection GetLanguages(CallContext context)
        {
            return null;
        }

        public override ItemDefinition GetItemDefinition(ID itemId, CallContext context)
        {
            var args = new GetItemDefinitionArgs(itemId, context);
            CorePipeline.Run("contactFacetDataProvider.getItemDefinition", args);
            return args.ItemDefinition;
        }

        public override FieldList GetItemFields(ItemDefinition itemDefinition, VersionUri version, CallContext context)
        {
            var args = new GetItemFieldsArgs(itemDefinition, context);
            CorePipeline.Run("contactFacetDataProvider.getItemFields", args);
            return args.FieldList;
        }
        
        public override ID GetParentID(ItemDefinition itemDefinition, CallContext context)
        {
            var args = new GetParentIDArgs(itemDefinition, context);
            CorePipeline.Run("contactFacetDataProvider.getParentID", args);
            return args.ParentId;
        }
    }
}