﻿using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSPool
{
    public interface ISMSPoolRepository
    {
        SMSResponseDto<SMSPoolTable> AddSMSPool(SMSPoolTable smsPoolInput);
        SMSResponseDto<SMSPoolTable> GetSMSPoolByName(string poolName);

        /// <summary>
        /// Updates the SMS pool into the database.
        /// </summary>
        /// <param name="SMSPoolInput"><see cref="SMSPoolTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSPoolTable> UpdateSMSPool(SMSPoolTable SMSPoolInput);

        /// <summary>
        /// Adds the SMS pool into the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSPool(string poolID);

        /// <summary>
        /// Gets all the SMS pools.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSPoolTable>> GetSMSPools();

        /// <summary>
        /// Gets all the SMS pool names
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSPoolTable>> GetSMSPoolNames();
    }
}
