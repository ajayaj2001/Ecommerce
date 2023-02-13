using Customer.Entities.Dtos;
using System;
using Customer.Entities.Models;
using Customer.Entities.ResponseTypes;

namespace Customer.Contracts.Services
{
    public interface IAuthService
    {

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        UserCredential GetUserByUserName(string userName);


        ///<summary>
        ///compare password
        ///</summary>
        ///<param name="dbPass"></param>
        ///<param name="userPass"></param>
        bool ComparePassword(string userPass, string dbPass);
    }

}
