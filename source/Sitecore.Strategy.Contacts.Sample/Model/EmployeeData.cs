using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sitecore.Strategy.Contacts.Sample.Model
{
    [Serializable]
    public class EmployeeData : Facet, IEmployeeData
    {
        public EmployeeData()
        {
            base.EnsureAttribute<string>(FIELD_EMPLOYEE_ID);
        }
        
        private const string FIELD_EMPLOYEE_ID = "EmployeeId";
        private const string FIELD_EMPLOYEE_NAME = "EmployeeName";
        private const string FIELD_EMPLOYEE_YEAR_OF_SERVICE = "EmployeeYearsOfService";
        private const string FIELD_EMPLOYEE_LOCATION = "EmployeeLocation";

        public string EmployeeId
        {
            get { return base.GetAttribute<string>(FIELD_EMPLOYEE_ID); }
            set { base.SetAttribute<string>(FIELD_EMPLOYEE_ID, value); }
        }
        public string Name
        {
            get { return base.GetAttribute<string>(FIELD_EMPLOYEE_NAME); }
            set { base.SetAttribute<string>(FIELD_EMPLOYEE_NAME, value); }
        }
        public int YearsOfService
        {
            get { return base.GetAttribute<int>(FIELD_EMPLOYEE_YEAR_OF_SERVICE); }
            set { base.SetAttribute<int>(FIELD_EMPLOYEE_YEAR_OF_SERVICE, value); }
        }
        public string Location
        {
            get { return base.GetAttribute<string>(FIELD_EMPLOYEE_LOCATION); }
            set { base.SetAttribute<string>(FIELD_EMPLOYEE_LOCATION, value); }
        }
    }
}