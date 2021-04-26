using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSQuota")]
    public class SMSQuotaTable
    {
        [Key]
        public long ID { get; set; }
        public string SMSChannelID { get; set; }
        [ForeignKey("SMSChannelID")]
        public SMSChannelTable SMSChannel { get; set; }
        public int MonthlyConsumption { get; set; }
        public int TotalConsumption { get; set; }
        public string CurrentMonth { get; set; }
        public int MonthlyQuota { get; set; }
        public int TotalQuota { get; set; }
    }
}
