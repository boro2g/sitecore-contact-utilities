using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.IDTables;
using System.Reflection;

namespace Sitecore.Strategy.Contacts.DataProviders
{
    public static class IDTableHelper
    {
        public static ID GenerateIdForValue(string prefix, string value)
        {
            if (string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(value))
            {
                return null;
            }
            var input = string.Format("{0}-{1}", prefix, value);
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.Default.GetBytes(input));
                var guid = new Guid(hash);
                return ID.Parse(guid);
            }
        }

        private static IDTableEntry UpdateOrCreateIDTableEntry(string prefix, string key, ID id, ID parentId, string customData)
        {
            var keys = IDTable.GetKeys(prefix, id);
            if (keys != null && keys.Length > 0)
            {
                var entry = keys[0];
                if (entry.ParentID == parentId && string.Equals(entry.CustomData, customData))
                {
                    return entry;
                }
                IDTable.RemoveID(prefix, id);
            }
            return IDTable.Add(prefix, key, id, parentId, customData);
        }

        public static ID GenerateIdForFacet(string facetName, ID parentId, ID templateId)
        {
            var prefix = "facet";
            var id = GenerateIdForValue(prefix, facetName);
            var customData = string.Format("templateId={0}", templateId.ToString());
            UpdateOrCreateIDTableEntry(prefix, facetName, id, parentId, customData);
            return id;
        }

        public static ID GenerateIdForFacetMember(string memberInfoName, ID parentId, ID templateId)
        {
            var prefix = "facet-member";
            var id = GenerateIdForValue(prefix, string.Format("{0}-{1}", memberInfoName, parentId.ToString()));
            var customData = string.Format("templateId={0}", templateId.ToString());
            UpdateOrCreateIDTableEntry(prefix, memberInfoName, id, parentId, customData);
            return id;
        }

        public static ID GenerateIdForFacetMemberValue(string memberValue, string memberDescription, ID parentId, ID templateId)
        {
            var prefix = "facet-member-value";
            var id = GenerateIdForValue(prefix, memberValue);
            var customData = string.Format("templateId={0}|description={1}", templateId.ToString(), memberDescription);
            UpdateOrCreateIDTableEntry(prefix, memberValue, id, parentId, customData);
            return id;
        }

        public static bool IsItem(string prefix, ID itemId)
        {
            var keys = IDTable.GetKeys(prefix, itemId);
            return (keys != null && keys.Length > 0);
        }
        private static IDTableEntry GetEntry(string prefix, ID itemId)
        {
            var keys = IDTable.GetKeys(prefix, itemId);
            if (keys == null || keys.Length == 0)
            {
                return null;
            }
            return keys[0];
        }
        public static string GetCustomDataValue(string dataKey, string prefix, ID itemId)
        {
            var entry = GetEntry(prefix, itemId);
            if (entry == null || string.IsNullOrEmpty(entry.CustomData))
            {
                return null;
            }
            var dictionary = entry.CustomData.Split('|').Select(x => x.Split('=')).ToDictionary(y => y[0], y => y[1]);
            if (! dictionary.ContainsKey(dataKey))
            {
                return null;
            }
            return dictionary[dataKey];
        }
        public static string GetKey(string prefix, ID itemId)
        {
            var entry = GetEntry(prefix, itemId);
            if (entry == null)
            {
                return null;
            }
            return entry.Key;
        }
        public static ID GetParentId(string prefix, ID itemId) 
        {
            var entry = GetEntry(prefix, itemId);
            if (entry == null)
            {
                return null;
            }
            return entry.ParentID;
        }
        
        public static bool IsFacetItem(ID itemId)
        {
            return IsItem("facet", itemId);
        }
        public static string GetFacetName(ID itemId)
        {
            return GetKey("facet", itemId);
        }
        public static ID GetFacetParentId(ID itemId)
        {
            return GetParentId("facet", itemId);
        }

        public static bool IsFacetMemberItem(ID itemId)
        {
            return IsItem("facet-member", itemId);
        }
        public static string GetFacetMemberName(ID itemId) 
        {
            return GetKey("facet-member", itemId);
        }
        public static ID GetFacetMemberParentId(ID itemId)
        {
            return GetParentId("facet-member", itemId);
        }

        public static bool IsFacetMemberValueItem(ID itemId)
        {
            return IsItem("facet-member-value", itemId);
        }
        public static string GetFacetMemberValue(ID itemId)
        {
            return GetKey("facet-member-value", itemId);
        }
        public static string GetFacetMemberValueDescription(ID itemId)
        {
            var entry = GetEntry("facet-member-value", itemId);
            if (entry == null)
            {
                return null;
            }
            return GetCustomDataValue("description", "facet-member-value", itemId);
        }

        public static ID GetFacetMemberValueParentId(ID itemId)
        {
            return GetParentId("facet-member-value", itemId);
        }

        public static string GetFacetMemberFacetName(ID itemId)
        {
            var parentId = GetFacetMemberParentId(itemId);
            var facetName = GetFacetName(parentId);
            return facetName;
        }
    }
}