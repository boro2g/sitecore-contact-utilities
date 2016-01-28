﻿using Sitecore.CodeDom.Scripts;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Rules.RuleMacros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sitecore.Strategy.Contacts.DataProviders;

namespace Sitecore.Strategy.Contacts.Items
{
    public class ContactFacetMemberItem : CustomItem
    {
        public ContactFacetMemberItem(Item innerItem)
            : base(innerItem)
        {
        }

        public Type ContactFacetContractType
        {
            get
            {
                ContactFacetNameItem item = this.InnerItem.Parent;
                if (item == null)
                {
                    return null;
                }
                return item.ContractType;
            }
        }
        public MemberInfo Member
        {
            get
            {
                var name = this.InnerItem[Sitecore.Strategy.Contacts.DataProviders.FieldIDs.ContactFacetMemberName];
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                var type = this.ContactFacetContractType;
                if (type == null)
                {
                    return null;
                }

                return ContactFacetHelper.GetFacetMemberInfo(type, name);
            }
        }

        public static implicit operator Item(ContactFacetMemberItem item)
        {
            if (item == null)
            {
                return null;
            }
            return item.InnerItem;
        }

        public static implicit operator ContactFacetMemberItem(Item item)
        {
            if (item == null)
            {
                return null;
            }
            var template = TemplateManager.GetTemplate(item);
            if (template == null)
            {
                return null;
            }
            if (template.InheritsFrom(Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetMemberTemplate))
            {
                return new ContactFacetMemberItem(item);
            }
            return null;
        }
    }
}