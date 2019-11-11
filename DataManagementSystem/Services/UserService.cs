using DataManagementSystem.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementSystem.Services
{
    public interface IUserService
    {
        Task<bool> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private Config _config;

        public UserService(IOptions<Config> configAccessor)
        {
            _config = configAccessor.Value;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            return await Task.Run(() =>
            {
                return (username.Equals(_config.Authentication.Username) && password.Equals(_config.Authentication.Password));
            });
        }
    }
}
