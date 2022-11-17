using SMSService.Data.Entities;
using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.Data.SMSPool
{
    public interface ISMSPoolRepository
    {
        /// <summary>
        /// Add the SMS pool into the database.
        /// </summary>
        /// <param name="smsPoolInput"><see cref="SMSPoolTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSPoolTable> AddSMSPool(SMSPoolTable smsPoolInput);

        /// <summary>
        /// Gets the SMS pool by name.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
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

        /// <summary>
        /// Checks if the SMS Pool ID exists in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> CheckIfSmsPoolIDIsValid(string poolID);

        /// <summary>
        /// Checks if the SMS Pool ID and pool name combination exists in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> CheckIfSmsPoolIDNameValid(string poolID, string poolName);
    }
}
