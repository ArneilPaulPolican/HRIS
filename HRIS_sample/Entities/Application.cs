using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRIS_sample.Entities
{
    public class ApplicationType
    {
        public Int32 ID { get; set; }
        public String TypeApplication { get; set; }
    }

    public class Application
    {
        public Int32 ID { get; set; }
        public Int32 ApplicantID { get; set; }
        public String Name { get; set; }
        public Int32 ApplicationTypeID { get; set; }
        public String TypeApplication { get; set; }
        public String Body { get; set; }
        public String Reason { get; set; }
        public String Remarks { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public Boolean IsLocked { get; set; }
        public Int32? UserEntryID { get; set; }
        public DateTime? UserDateEntry { get; set; }
        public Int32? UpdatedUserID { get; set; }
        public DateTime? UpdatedDateEntry { get; set; }
        
    }
}