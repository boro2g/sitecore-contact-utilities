using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Xml;
using System.Reflection;
using Sitecore.Strategy.Contacts.Utils;

namespace Sitecore.Strategy.Contacts.DataProviders
{
    public static class ContactFacetHelper
    {
        private static Dictionary<Type, Type> _contactFacetImplementations = null;
        public static Dictionary<Type, Type> ContactFacetImplementations
        {
            get
            {
                if (_contactFacetImplementations == null)
                {
                    _contactFacetImplementations = new Dictionary<Type, Type>();
                    var configNode = Factory.GetConfigNode("model/elements");
                    //<element interface="Sitecore.Strategy.Contacts.Model.IEmployeeData, Sitecore.Strategy.Contacts" implementation="Sitecore.Strategy.Contacts.Model.EmployeeData, Sitecore.Strategy.Contacts" />
                    foreach (XmlNode node in configNode.ChildNodes)
                    {
                        var iname = node.Attributes["interface"];
                        var interfaceType = Type.GetType(iname.Value);
                        var iimpl = node.Attributes["implementation"];
                        var implType = Type.GetType(iimpl.Value);
                        _contactFacetImplementations.Add(interfaceType, implType);
                    }
                }
                return _contactFacetImplementations;
            }
        }

        private static XmlNode GetContractConfigNode()
        {
            var configNode = Factory.GetConfigNode("model/entities/contact/facets");
            return configNode;
        }

        public static List<string> GetFacetNames()
        {
            var configNode = GetContractConfigNode();
            var nameAttributes = configNode.SelectNodes("facet/@name");
            var names = new List<string>();
            foreach (XmlAttribute nameAttribute in nameAttributes)
            {
                names.Add(nameAttribute.Value);
            }
            return names;
        }

        private static XmlNode GetFacetNode(string facetName)
        {
            var configNode = GetContractConfigNode();
            var facetNode = configNode.SelectSingleNode(string.Format("facet[@name='{0}']", facetName));
            return facetNode;
        }

        public static Type GetContractTypeForFacet(string facetName)
        {
            var facetNode = GetFacetNode(facetName);
            if (facetNode == null)
            {
                return null;
            }
            var typeName = XmlUtil.GetAttribute("contract", facetNode);
            var type = Type.GetType(typeName);
            return type;
        }

        public static MemberInfo GetFacetMemberInfo(Type facetType, string memberName)
        {
            if (facetType == null)
            {
                return null;
            }

            string[] parts = memberName.Split(NestedFacets.Delimeter.ToCharArray());

            if (parts.Length > 1)
            {
                return GetNestedFacetMemberInfo(facetType, parts);
            }

            var member = facetType.GetMember(memberName).FirstOrDefault();

            return member;
        }

        public static Type GetFacetMemberType(Type facetType, string memberName)
        {
            if (facetType == null)
            {
                return null;
            }

            return FacetMemberType(GetFacetMemberInfo(facetType, memberName));
        }

        public static Type GetFacetMemberType(string facetName, string memberName)
        {
            return GetFacetMemberType(GetContractTypeForFacet(facetName), memberName);
        }

        private static Type FacetMemberType(MemberInfo member)
        {
            if (member?.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo) member).PropertyType;
            }

            if (member?.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo) member).FieldType;
            }

            if (member?.MemberType == MemberTypes.Method)
            {
                return ((MethodInfo) member).ReturnType;
            }

            return null;
        }

        private static MemberInfo GetNestedFacetMemberInfo(Type contractType, string[] parts)
        {
            foreach (string part in parts)
            {
                var member = contractType.GetMember(part).FirstOrDefault();

                if (parts.Length == 1)
                {
                    return member;
                }
                else
                {
                    return GetNestedFacetMemberInfo(FacetMemberType(member), parts.Skip(1).ToArray());
                }
            }

            return null;
        }
    }
}