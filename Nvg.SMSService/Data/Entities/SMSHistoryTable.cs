using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSHistory")]
    public class SMSHistoryTable
    {
        [Key]
        public string ID { get; set; }
        public string MessageSent { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public DateTime SentOn { get; set; }
        public string TemplateName { get; set; }
        public string TemplateVariant { get; set; }
        public string SMSChannelID { get; set; }
        [NotMapped]
        public string ChannelKey { get; set; }
        [ForeignKey("SMSChannelID")]
        public SMSChannelTable SMSChannel { get; set; }
        public string SMSProviderID { get; set; }
        [NotMapped]
        public string ProviderName { get; set; }
        [ForeignKey("SMSProviderID")]
        public SMSProviderSettingsTable SMSProvider { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
    }
}
