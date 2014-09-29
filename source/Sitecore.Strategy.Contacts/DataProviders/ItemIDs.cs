using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;

namespace Sitecore.Strategy.Contacts.DataProviders
{
    public static class ItemIDs
    {
        public static readonly ID ContactsTemplatesFolder = new ID("{A518DF02-A12F-410B-ABC5-656888C8257A}");
        public static readonly ID ContactFacetTemplateDataSection = new ID("{5A5D1E53-26E8-4029-AFA1-1DF79217D677}");
        
        public static readonly ID ContactsFolder = new ID("{83A409BE-5D6C-46CD-99D8-23C98C45D575}");
        public static readonly ID ContactFacetsFolder = new ID("{C630AE5C-9EA7-4F22-9EBA-2B385425F78B}");

        public static readonly ID SettingsRoot = new ID("{087E1EA5-6280-4575-9E70-85B588DB91B2}");
    }
}