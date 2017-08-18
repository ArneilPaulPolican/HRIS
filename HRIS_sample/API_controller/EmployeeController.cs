using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HRIS_sample.Entities;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace HRIS_sample.API_controller
{
    public class EmployeeController : ApiController
    {
        private Data.HRISDataContext db = new Data.HRISDataContext();
        [HttpGet, Route("api/employee/list")]
        public List<Employee> getEmployee()
        {
            var employeeList = from d in db.MstEmployees
                               select new Employee
                               {
                                   Id=d.ID,
                                   Name=d.Name
                               };
            return employeeList.ToList();
        }

    }
}
