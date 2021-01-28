using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSHistory")]
    public class SMSHistoryTable
    {
        [Key]
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string ToPhNumbers { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SentOn { get; set; }
        public string Status { get; set; }

    }
}
