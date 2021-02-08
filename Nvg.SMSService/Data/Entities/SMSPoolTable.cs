using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSPool")]
    public class SMSPoolTable
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
