using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Collections;
using Sitecore.Strategy.Contacts.DataProviders;
using Sitecore.Rules;
using Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues;
using Sitecore.Pipelines;
using Sitecore.Data;
using Sitecore.Data.DataProviders;
using System.Reflection;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Strategy.Contacts.Utils;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.GetChildIDs
{
    public class DefaultProcessor
    {
        public virtual void Process(GetChildIDsArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.ItemDefinition, "args.ItemDefinition");
            Assert.ArgumentNotNull(args.Context, "args.Context");
            //TODO: finish
            var ids = args.IDList;
            var itemId = args.ItemDefinition.ID;
            if (itemId == Sitecore.Strategy.Contacts.DataProviders.ItemIDs.SettingsRoot)
            {
                ids.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactsFolder);
            }
            else if (itemId == Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactsFolder)
            {
                ids.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactFacetsFolder);
            }
            else if (itemId == Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactFacetsFolder)
            {
                AddChildIDsForContactFacetsRootItem(ids, args.ItemDefinition, args.Context);
            }
            else if (IDTableHelper.IsFacetItem(itemId))
            {
                AddChildIDsForContactFacetItem(ids, args.ItemDefinition, args.Context);
            }
            else if (IDTableHelper.IsFacetMemberItem(itemId))
            {
                AddChildIDsForContactFacetMemberItem(ids, args.ItemDefinition, args.Context);
            }
        }
        protected virtual void AddChildIDsForContactFacetsRootItem(IDList ids, ItemDefinition itemDefinition, CallContext context)
        {
            var facetNames = ContactFacetHelper.GetFacetNames();
            foreach (var name in facetNames)
            {
                var id = IDTableHelper.GenerateIdForFacet(name, itemDefinition.ID, Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetTemplate);
                ids.Add(id);
            }
        }
        protected virtual void AddChildIDsForContactFacetItem(IDList ids, ItemDefinition itemDefinition, CallContext context)
        {
            var facetName = IDTableHelper.GetFacetName(itemDefinition.ID);
            var contractType = ContactFacetHelper.GetContractTypeForFacet(facetName);
            
            foreach (string memberName in FacetReflectionUtil.NonFacetMemberNames(contractType))
            {
                var id = IDTableHelper.GenerateIdForFacetMember(memberName, itemDefinition.ID,
                            Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberTemplate);

                ids.Add(id);
            }

            foreach (string memberName in FacetReflectionUtil.FacetMemberNames(contractType))
            {
                foreach (
                    string subMemberName in
                        FacetReflectionUtil.NonFacetMemberNames(contractType.GetProperty(memberName).PropertyType))
                {
                    string key = $"{memberName}{NestedFacets.Delimeter}{subMemberName}";

                    var id = IDTableHelper.GenerateIdForFacetMember(key, itemDefinition.ID,
                        Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberTemplate);

                    ids.Add(id);
                }
            }
        }

        protected virtual void AddChildIDsForContactFacetMemberItem(IDList ids, ItemDefinition itemDefinition, CallContext context)
        {
            var itemId = itemDefinition.ID;
            var facetName = IDTableHelper.GetFacetMemberFacetName(itemId);
            var memberName = IDTableHelper.GetFacetMemberName(itemId);
            var args = new GetFacetMemberValuesArgs(facetName, memberName);
            CorePipeline.Run("getFacetMemberValues", args);
            if (!args.Values.Any())
            {
                return;
            }
            foreach (var pair in args.Values)
            {
                var id = IDTableHelper.GenerateIdForFacetMemberValue($"{facetName}-{memberName}-{pair.Key}", pair.Value, itemId, Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberValueTemplate);
                ids.Add(id);
            }
        }


    }
}