using System;
using System.Collections.Generic;
using System.Reflection;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.Strategy.Contacts.Utils
{
    public class FacetReflectionUtil
    {
        public static IEnumerable<string> NonFacetMemberNames(Type type)
        {
            return MemberNames(type, false);
        }

        public static IEnumerable<string> FacetMemberNames(Type type)
        {
            return MemberNames(type, true);
        }

        private static IEnumerable<string> MemberNames(Type type, bool getFacets)
        {
            if (type == null)
            {
                yield break;
            }

            var members = type.GetMembers();

            foreach (var member in members)
            {
                if (member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
                {
                    var propertyType = type.GetProperty(member.Name).PropertyType;

                    if (typeof(IFacet).IsAssignableFrom(propertyType))
                    {
                        if (getFacets)
                        {
                            yield return member.Name;
                        }
                    }
                    else
                    {
                        if (!getFacets)
                        {
                            yield return member.Name;
                        }
                    }
                }
            }
        }
    }
}