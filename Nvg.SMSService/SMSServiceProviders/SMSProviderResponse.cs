using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSServiceProviders
{
    public class SmsProviderResponse
    {
        public string StatusMessage { get; set; }
        public long? Unit { get; set; }
        public long? SmsCost { get; set; }
    }
}
