using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.Models
{
    public class SMSDBInfo
    {
        public SMSDBInfo(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
