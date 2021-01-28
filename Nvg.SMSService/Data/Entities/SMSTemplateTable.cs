using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSTemplate")]
    public class SMSTemplateTable
    {
        [Key]
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Name { get; set; }
        public string MessageTemplate { get; set; }
    }
}
