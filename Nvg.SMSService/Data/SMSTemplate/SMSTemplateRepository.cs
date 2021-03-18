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

        public SMSResponseDto<SMSTemplateTable> AddUpdateSMSTemplate(SMSTemplateTable templateInput)
        {
            var response = new SMSResponseDto<SMSTemplateTable>();
            try
            {
                var template = _context.SMSTemplates.FirstOrDefault(st => st.Name.ToLower().Equals(templateInput.Name.ToLower()) && st.SMSPoolID.Equals(templateInput.SMSPoolID) && st.Variant==templateInput.Variant);
                if (template != null)
                {
                    template.MessageTemplate = templateInput.MessageTemplate;
                    template.Sender = templateInput.Sender;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = templateInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed To Update";
                        response.Result = templateInput;
                    }
                }
                else
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
                          (t.Name.Equals(templateName) && string.IsNullOrEmpty(variant) ||
                           !string.IsNullOrEmpty(variant) && t.Name.Equals(templateName) && t.Variant.ToLower().Equals(variant.ToLower()))
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

        public SMSResponseDto<List<SMSTemplateTable>> GetSMSTemplatesByPool(string poolID)
        {
            var response = new SMSResponseDto<List<SMSTemplateTable>>();
            try
            {
                var smsTemplates = (from t in _context.SMSTemplates
                                      join p in _context.SMSPools on t.SMSPoolID equals p.ID
                                      where p.ID.ToLower().Equals(poolID.ToLower())
                                      select t).ToList();
                if (smsTemplates.Count > 0)
                {
                    response.Status = true;
                    response.Message = $"Obtained {smsTemplates.Count} records";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Found no record";
                }
                response.Result = smsTemplates;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<string> DeleteSMSTemplate(string templateID)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsTemplate = _context.SMSTemplates.Where(o => o.ID.ToLower().Equals(templateID.ToLower())).FirstOrDefault();
                if (smsTemplate != null)
                {
                    _context.SMSTemplates.Remove(smsTemplate);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = $"Deleted Template";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = $"Failed to delete Template";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Found no record";
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
    }
}
