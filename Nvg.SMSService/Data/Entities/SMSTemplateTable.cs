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
        public string ID { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string SMSPoolID { get; set; }
        [ForeignKey("SMSPoolID")]
        public SMSPoolTable SMSPool { get; set; }
        public string MessageTemplate { get; set; }
    }
}
