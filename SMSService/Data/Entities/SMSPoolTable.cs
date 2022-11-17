using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMSService.Data.Entities
{
    [Table("SMSPool")]
    public class SMSPoolTable
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
