using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Strategy.Contacts.Sample.Model
{
    public interface IEmployeeData : IFacet
    {
        string EmployeeId { get; set; }
        string Name { get; set; }
        int YearsOfService { get; set; }
        string Location { get; set; }
    }
}
