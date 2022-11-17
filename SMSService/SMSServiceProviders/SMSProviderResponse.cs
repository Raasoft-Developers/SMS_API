using System;
using System.Collections.Generic;
using System.Text;

namespace SMSService.SMSServiceProviders
{
    public class SmsProviderResponse
    {
        public string StatusMessage { get; set; }
        public long? Unit { get; set; }
        public long? SmsCost { get; set; }
        public string Status { get; set; }
    }
}
