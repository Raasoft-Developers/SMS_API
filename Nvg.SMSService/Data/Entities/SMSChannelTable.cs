using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nvg.SMSService.Data.Entities
{
    [Table("SMSChannel")]
    public class SMSChannelTable
    {
        [Key]
        public string ID { get; set; }
        public string Key { get; set; }
        public string SMSPoolID { get; set; }
        [NotMapped]
        public string SMSPoolName { get; set; }
        [ForeignKey("SMSPoolID")]
        public SMSPoolTable SMSPool { get; set; }
        public string SMSProviderID { get; set; }
        [NotMapped]
        public string SMSProviderName { get; set; }
        [ForeignKey("SMSProviderID")]
        public SMSProviderSettingsTable SMSProvider { get; set; }
    }
}
