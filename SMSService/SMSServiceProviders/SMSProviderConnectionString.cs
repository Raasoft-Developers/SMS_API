using System;
using System.Collections.Generic;
using System.Text;

namespace SMSService.SMSServiceProviders
{
    public class SMSProviderConnectionString
    {
        public Dictionary<string, string> Fields { get; set; }

        public SMSProviderConnectionString(string connectionString)
        {
            Fields = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(connectionString))
            {
                LoadConnectionStringParts(connectionString);
            }
            else
            {
                throw new ArgumentException(nameof(connectionString));
            }
        }

        private void LoadConnectionStringParts(string connectionString)
        {
            var connString = connectionString.Split(";");
            foreach (var part in connString)
            {
                if (part != "")
                {
                    var splitString = part.Split("=");
                    var key = splitString[0].ToLower();
                    var value = splitString[1];
                    Fields[key] = value;
                }
            }
        }
    }
}
