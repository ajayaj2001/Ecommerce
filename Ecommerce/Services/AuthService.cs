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
        ///get user by email
        ///</summary>
        ///<param name="email"></param>
        public UserCredential GetUserByEmail(string email)
        {
            return _userRepository.GetUserCredentialByEmail(email);
        }

    }
}
