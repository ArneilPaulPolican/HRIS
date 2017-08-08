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
    public class ApplicationController : ApiController
    {
        private Data.HRISDataContext db = new Data.HRISDataContext();
        //List
        [HttpGet, Route("api/application/list")]
        public List<Application> getApplication()
        {
            var applicationList = from d in db.TrnApplications
                                  select new Application
                                  {
                                      ID = d.ID,
                                      ApplicantID = d.ApplicantID,
                                      ApplicationTypeID = d.ApplicationTypeId,
                                      TypeApplication = d.TrnApplicationType.ApplicationType,
                                      Body = d.Body,
                                      Reason=d.Reason,
                                      Remarks = d.Remarks,
                                      ApplicationDate = d.ApplicationDate,
                                      IsLocked=d.IsLocked,
                                      UserDateEntry = d.UserDateEntry,
                                      UserEntryID = d.UserEntryID,
                                      UpdatedUserID = d.UpdatedUserID,
                                      UpdatedDateEntry = d.UpdatedDateEntry
                                  };
            return applicationList.ToList();
        }
        //get
        [HttpGet, Route("api/application/get/{id}")]
        public Entities.Application listApplication(string id)
        {
            var applicationToList = from d in db.TrnApplications
                                    where d.ID == Convert.ToInt32(id)
                                    select new Entities.Application
                                    {
                                        ID = d.ID,
                                        ApplicantID = d.ApplicantID,
                                        ApplicationTypeID = d.ApplicationTypeId,
                                        TypeApplication = d.TrnApplicationType.ApplicationType,
                                        Body = d.Body,
                                        Remarks = d.Remarks,
                                        Reason = d.Reason,
                                        ApplicationDate = d.ApplicationDate,
                                        IsLocked = d.IsLocked,
                                        UserDateEntry = d.UserDateEntry,
                                        UserEntryID=d.UserEntryID,
                                        UpdatedUserID=d.UpdatedUserID,
                                        UpdatedDateEntry=d.UpdatedDateEntry
                                    };
            return (Entities.Application)applicationToList.FirstOrDefault();
            //return applicationToList.ToList();
        }
        //add
        [HttpPost, Route("api/application/add")]
        public HttpResponseMessage postApplication(Entities.Application addApplication)
        {
            try
            {
                var employee = (from d in db.MstEmployees where d.UserID == User.Identity.GetUserId() select d.ID).SingleOrDefault();
                Data.TrnApplication newApplication = new Data.TrnApplication();



                newApplication.ApplicantID = employee;
                newApplication.ApplicationTypeId = addApplication.ApplicationTypeID;
                newApplication.Body = "NA";/*!= null  addApplication.Body : "NA";*/
                newApplication.Reason = "NA";/*!= null ? addApplication.Reason : "NA";*/
                newApplication.Remarks ="NA";/*!= null ? addApplication.Remarks : "Na";*/
                newApplication.ApplicationDate = DateTime.Today;
                newApplication.UserEntryID = employee;
                newApplication.UpdatedUserID = employee;
                newApplication.UserDateEntry = DateTime.Today;
                newApplication.UpdatedDateEntry = DateTime.Today;
                newApplication.IsLocked = addApplication.IsLocked;
                db.TrnApplications.InsertOnSubmit(newApplication);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        //delete
        [HttpDelete,Route("api/application/delete/{id}")]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                var applications = from d in db.TrnApplications where d.ID == Convert.ToInt32(id) select d;
                if (applications.Any())
                {
                    db.TrnApplications.DeleteOnSubmit(applications.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        // LOCK
        [HttpPut]
        [Route("api/application/Lock/{id}")]
        public HttpResponseMessage LockApp(String id,Entities.Application lockapplication)
        {
            try
            {
                var appID = from d in db.TrnApplications where d.ID == Convert.ToInt32(id) select d;
                if (appID.Any())
                {
                    var updateApp = appID.FirstOrDefault();
                    var employee = (from d in db.MstEmployees where d.UserID == User.Identity.GetUserId() select d.ID).SingleOrDefault();


                    updateApp.ApplicantID = lockapplication.ApplicantID;
                    updateApp.ApplicationTypeId = lockapplication.ApplicationTypeID;
                    updateApp.Body = lockapplication.Body;
                    updateApp.Reason = lockapplication.Reason;
                    updateApp.Remarks = lockapplication.Remarks;
                    updateApp.ApplicationDate = Convert.ToDateTime(lockapplication.ApplicationDate);
                    updateApp.IsLocked = true;
                    updateApp.UserEntryID = lockapplication.UserEntryID;
                    updateApp.UserDateEntry = Convert.ToDateTime(lockapplication.UserDateEntry);
                    updateApp.UpdatedUserID = employee;
                    updateApp.UpdatedDateEntry = Convert.ToDateTime(lockapplication.UpdatedDateEntry);

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        //update
        [HttpPut, Route("api/application/Unlock/{id}")]
        public HttpResponseMessage update(String id, Entities.Application item)
        {
            try
            {
                var appID = from d in db.TrnApplications where d.ID == Convert.ToInt32(id) select d;
                if (appID.Any())
                {
                    var updateApp = appID.FirstOrDefault();
                    var employee = (from d in db.MstEmployees where d.UserID == User.Identity.GetUserId() select d.ID).SingleOrDefault();


                    updateApp.ApplicantID = item.ApplicantID;
                    updateApp.ApplicationTypeId = item.ApplicationTypeID;
                    updateApp.Body = item.Body;
                    updateApp.Reason = item.Reason;
                    updateApp.Remarks = item.Remarks;
                    updateApp.ApplicationDate = Convert.ToDateTime(item.ApplicationDate);
                    updateApp.IsLocked = false;
                    updateApp.UserEntryID = item.UserEntryID;
                    updateApp.UserDateEntry = Convert.ToDateTime(item.UserDateEntry);
                    updateApp.UpdatedUserID = employee;
                    updateApp.UpdatedDateEntry = Convert.ToDateTime(item.UpdatedDateEntry);

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }
}
