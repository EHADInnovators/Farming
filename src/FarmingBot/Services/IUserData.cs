using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmingBot.Services
{
    public interface IUserData
    {
        // Should return basic user profile info e.g., Loyalty Level, etc.
        Task<UserProfile> GetUserInformation(string name, string email);
        // lot more we can do
        // edit, delete, etc.
    }
}
