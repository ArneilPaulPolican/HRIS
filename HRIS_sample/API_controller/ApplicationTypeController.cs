using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using HRIS_sample.Entities;
using System.Diagnostics;

namespace HRIS_sample.API_controller
{
    public class ApplicationTypeController : ApiController
    {
        private Data.HRISDataContext db = new Data.HRISDataContext();

        [HttpGet, Route("api/applicationtype/list")]
        public List<ApplicationType> getType()
        {
            var typeList = from d in db.TrnApplicationTypes
                           select new ApplicationType
                           {
                               ID = d.ID,
                               TypeApplication = d.ApplicationType
                           };
            return typeList.ToList();
        }
        //get
        [HttpGet, Route("api/applicationtype/list/{id}")]
        public List<ApplicationType> listApplication(string id)
        {
            var applicationToList = from d in db.TrnApplicationTypes
                                    where d.ID == Convert.ToInt32(id)
                                    select new Entities.ApplicationType
                                    {
                                        ID = d.ID,
                                        TypeApplication=d.ApplicationType
                                    };
            return applicationToList.ToList();
        }

    }
}
