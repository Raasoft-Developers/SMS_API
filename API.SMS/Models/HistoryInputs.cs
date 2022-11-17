using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SMS.Models
{
    public class HistoryInput
    {
        public string ChannelKey { get; set; }
        public string Tag { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
