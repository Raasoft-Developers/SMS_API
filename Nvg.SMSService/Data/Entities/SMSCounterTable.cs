using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nvg.SMSService.Data.Entities
{
    public class SMSCounterTable
    {
        [Key]
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string Count { get; set; }
        public string FacilityID { get; set; }
    }
}
