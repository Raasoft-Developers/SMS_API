using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsDefault { get; set; }
    }
}
