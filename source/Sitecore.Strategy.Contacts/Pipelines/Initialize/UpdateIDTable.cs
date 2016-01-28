using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Pipelines;
using Sitecore.Strategy.Contacts.DataProviders;
using System.Reflection;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues;
using Sitecore.Strategy.Contacts.Utils;

namespace Sitecore.Strategy.Contacts.Pipelines.Initialize
{
    public class UpdateIDTable
    {
        public virtual void Process(PipelineArgs args)
        {
            AddFacets(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactFacetsFolder);
        }
        protected virtual void AddFacets(ID parentId)
        {
            var facetNames = ContactFacetHelper.GetFacetNames();
            foreach (var name in facetNames)
            {
                var facetId = IDTableHelper.GenerateIdForFacet(name, parentId, Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetTemplate);
                AddContactFacetMember(name, facetId);
            }
        }

        protected virtual void AddContactFacetMember(string facetName, ID parentId)
        {
            var contractType = ContactFacetHelper.GetContractTypeForFacet(facetName);

            foreach (string memberName in FacetReflectionUtil.NonFacetMemberNames(contractType))
            {
                var memberId = IDTableHelper.GenerateIdForFacetMember(memberName, parentId,
                    Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberTemplate);
                AddContactFacetMemberValues(facetName, memberName, memberId);
            }

            foreach (string memberName in FacetReflectionUtil.FacetMemberNames(contractType))
            {
                foreach (
                    string subMemberName in
                        FacetReflectionUtil.NonFacetMemberNames(contractType.GetProperty(memberName).PropertyType))
                {
                    string key = $"{memberName}{NestedFacets.Delimeter}{subMemberName}";

                    var memberId = IDTableHelper.GenerateIdForFacetMember(key, parentId,
                        Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberTemplate);
                    AddContactFacetMemberValues(facetName, key, memberId);
                }
            }
        }

        protected virtual
            void AddContactFacetMemberValues(string facetName, string memberName, ID parentId)
        {
            var args = new GetFacetMemberValuesArgs(facetName, memberName);
            CorePipeline.Run("getFacetMemberValues", args);
            if (!args.Values.Any())
            {
                return;
            }
            foreach (var pair in args.Values)
            {
                IDTableHelper.GenerateIdForFacetMemberValue($"{facetName}-{memberName}-{pair.Key}", pair.Value, parentId, Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberValueTemplate);
            }
        }

    }
}