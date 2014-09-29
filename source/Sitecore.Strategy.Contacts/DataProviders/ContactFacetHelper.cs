using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Xml;
using System.Reflection;

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

        public static Type GetFacetMemberType(string facetName, string memberName)
        {
            var contractType = GetContractTypeForFacet(facetName);
            if (contractType == null)
            {
                return null;
            }
            var member = contractType.GetMember(memberName).FirstOrDefault();
            if (member == null)
            {
                return null;
            }
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).PropertyType;
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)member).FieldType;
            }
            else if (member.MemberType == MemberTypes.Method)
            {
                return ((MethodInfo)member).ReturnType;
            }
            return null;
        }
    }
}