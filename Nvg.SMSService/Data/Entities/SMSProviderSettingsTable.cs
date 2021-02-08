using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSProviderSettings")]
    public class SMSProviderSettingsTable
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public string SMSPoolID { get; set; }
        [ForeignKey("SMSPoolID")]
        public SMSPoolTable SMSPool { get; set; }
    }
}
