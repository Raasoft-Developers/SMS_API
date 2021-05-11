using System.Threading.Tasks;

namespace Nvg.SMSBackgroundTask.SMSProvider
{
    public interface ISMSProvider
    {
        public Task<string> SendSMS(string recipients, string message, string sender = "");
    }
}
