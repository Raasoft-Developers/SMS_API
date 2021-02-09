using Microsoft.Extensions.Logging;
using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.SMSService.Data.SMSTemplate
{
    public class SMSTemplateRepository : ISMSTemplateRepository
    {
        private readonly SMSDbContext _context;
        private readonly ILogger<SMSTemplateRepository> _logger;

        public SMSTemplateRepository(SMSDbContext context, ILogger<SMSTemplateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public SMSResponseDto<SMSTemplateTable> AddSMSTemplate(SMSTemplateTable templateInput)
        {
            var response = new SMSResponseDto<SMSTemplateTable>();
            try
            {
                templateInput.ID = Guid.NewGuid().ToString();
                _context.SMSTemplates.Add(templateInput);
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Added";
                    response.Result = templateInput;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Not Added";
                    response.Result = templateInput;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName)
        {
            var response = new SMSResponseDto<bool>();
            try
            {
                var templateExist = (from t in _context.SMSTemplates
                                   join c in _context.SMSChannels on t.SMSPoolID equals c.SMSPoolID
                                   where t.Name.Equals(templateName) && c.Key.ToLower().Equals(channelKey.ToLower())
                                   select t).Any();
                response.Status = templateExist;
                response.Message = $"Is template existing : {templateExist}";
                response.Result = templateExist;
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public SMSTemplateTable GetSMSTemplate(string templateID)
        {
            return _context.SMSTemplates.FirstOrDefault(x => x.ID == templateID);
        }

        public SMSTemplateTable GetSMSTemplate(string templateName, string channelKey, string variant = null)
        {
            var smsTemplate = new SMSTemplateTable();
            var smsQry = (from t in _context.SMSTemplates
                           join c in _context.SMSChannels on t.SMSPoolID equals c.SMSPoolID
                           where c.Key.ToLower().Equals(channelKey.ToLower()) &&
                           (t.Name.Equals(templateName) && string.IsNullOrEmpty(t.Variant) ||
                            t.Name.Equals(templateName) && t.Variant.ToLower().Equals(variant.ToLower()))
                           select t);

            if (!string.IsNullOrEmpty(variant))
                smsTemplate = smsQry.ToList().FirstOrDefault(st => !string.IsNullOrEmpty(st.Variant));
            else
                smsTemplate = smsQry.FirstOrDefault();
            
            /*if (smsTemplate == null)
                smsTemplate = _context.SMSTemplate.FirstOrDefault(t => t.Name == defaultTemplate);*/

            _logger.LogDebug($"Template used : {smsTemplate?.Name}");

            return smsTemplate;
        }

    }
}
