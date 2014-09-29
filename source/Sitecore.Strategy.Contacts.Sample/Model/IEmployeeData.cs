using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Strategy.Contacts.Sample.Model
{
    public interface IEmployeeData : IFacet
    {
        string EmployeeId { get; }
        string Name { get; }
        int YearsOfService { get; }
        string Location { get; }
    }
}
