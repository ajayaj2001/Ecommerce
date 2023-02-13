using Microsoft.Extensions.Configuration;
using Customer.Contracts.Repositories;
using System;
using Customer.Entities.Models;
using Customer.Contracts.Services;

namespace Customer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository UserRepository)
        {
            _userRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
        }

        ///<summary>
        ///compare password
        ///</summary>
        ///<param name="dbPass"></param>
        ///<param name="userPass"></param>
        public bool ComparePassword(string userPass, string dbPass)
        {
            return userPass == dbPass;
        }

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        public UserCredential GetUserByUserName(string userName)
        {
            return _userRepository.GetUserCredentialByUserName(userName);
        }

    }
}
