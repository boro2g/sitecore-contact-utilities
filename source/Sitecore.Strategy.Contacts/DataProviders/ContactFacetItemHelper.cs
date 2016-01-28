using Sitecore.Analytics.Model.Framework;
using Sitecore.Data;
using Sitecore.Strategy.Contacts.Items;
using System;
using System.Linq;
using System.Reflection;
using Sitecore.Strategy.Contacts.Utils;

namespace Sitecore.Strategy.Contacts.DataProviders
{
    public static class ContactFacetItemHelper
    {
        private static IFacet GetFacet(Database database, ID contactFacetId)
        {
            ContactFacetNameItem cfnItem = database.GetItem(contactFacetId);
            if (cfnItem == null)
            {
                return null;
            }
            var facetName = cfnItem.FacetName;
            var contact = Sitecore.Analytics.Tracker.Current.Contact;
            if (contact == null)
            {
                return null;
            }
            var facet = contact.Facets[facetName];
            return facet;
        }
        private static ContactFacetMemberItem GetFacetMember(Database database, ID contactFacetId, ID contactFacetMemberId)
        {
            var facet = GetFacet(database, contactFacetId);

            if (facet == null)
            {
                return null;
            }

            ContactFacetMemberItem cfmItem = database.GetItem(contactFacetMemberId);

            return cfmItem;
        }
        public static Type GetFacetMemberValueType(Database database, ID contactFacetId, ID contactFacetMemberId)
        {
            var member = GetFacetMember(database, contactFacetId, contactFacetMemberId).Member;
            if (member == null)
            {
                return null;
            }
            if (member is PropertyInfo)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    return property.PropertyType;
                }
            }
            else if (member is FieldInfo)
            {
                var field = member as FieldInfo;
                if (field != null)
                {
                    return field.FieldType;
                }
            }
            else if (member is MethodInfo)
            {
                var method = member as MethodInfo;
                if (method != null)
                {
                    return method.ReturnType;
                }
            }
            return null;
        }
        public static object GetFacetMemberValue(Database database, ID contactFacetId, ID contactFacetMemberId)
        {
            var facet = GetFacet(database, contactFacetId);
            if (facet == null)
            {
                return null;
            }
            var member = GetFacetMember(database, contactFacetId, contactFacetMemberId);

            string[] parts = member.DisplayName.Split(NestedFacets.Delimeter.ToCharArray());

            if (parts.Length > 1)
            {
                return FindNestedPropertyValue(facet, parts);
            }

            if (member.Member.MemberType == MemberTypes.Property)
            {
                var property = member.Member as PropertyInfo;
                return property?.GetValue(facet);
            }

            return null;
        }

        private static object FindNestedPropertyValue(object entity, string[] parts)
        {
            foreach (string part in parts)
            {
                PropertyInfo property = entity.GetType().GetProperty(part);

                if (property != null)
                {
                    if (parts.Length == 1)
                    {
                        return property.GetValue(entity);
                    }
                    else
                    {
                        return FindNestedPropertyValue(property.GetValue(entity), parts.Skip(1).ToArray());
                    }
                }
            }

            return null;
        }
    }
}