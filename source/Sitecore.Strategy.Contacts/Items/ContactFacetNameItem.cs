using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Strategy.Contacts.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Strategy.Contacts.Items
{
    public class ContactFacetNameItem : CustomItem
    {
        public ContactFacetNameItem(Item innerItem) : base(innerItem)
        {
        }

        public string FacetName
        {
            get
            {
                return this[Sitecore.Strategy.Contacts.DataProviders.FieldIDs.ContactFacetName];
            }
        }

        public Type ContractType
        {
            get
            {
                var typeName = this[Sitecore.Strategy.Contacts.DataProviders.FieldIDs.ContactFacetContract];
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }
                return Type.GetType(typeName);
            }
        }

        public Type ImplementationType
        {
            get
            {
                var typeName = this[Sitecore.Strategy.Contacts.DataProviders.FieldIDs.ContactFacetImplementation];
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }
                return Type.GetType(typeName);
            }
        }

        public static implicit operator Item(ContactFacetNameItem item)
        {
            if (item == null)
            {
                return null;
            }
            return item.InnerItem;
        }

        public static implicit operator ContactFacetNameItem(Item item)
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
            if (template.InheritsFrom(Sitecore.Strategy.Contacts.DataProviders.TemplateIDs.ContactFacetTemplate))
            {
                return new ContactFacetNameItem(item);
            }
            return null;
        }
    }
}