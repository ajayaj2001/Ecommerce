using Customer.Entities.Models;
using System;
using System.Collections.Generic;

namespace Customer.Contracts.Repositories
{
    public interface IUserRepository
    {

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="user"></param>
        void CreateUser(User user);

        ///<summary>
        /// get user credential by username
        ///</summary>
        ///<param name="email"></param>
        UserCredential GetUserCredentialByUserName(string email);

        ///<summary>
        /// get user credientials by user id
        ///</summary>
        ///<param name="userId"></param>
        UserCredential GetUserCredentialByUserId(Guid userId);

        ///<summary>
        /// get user crediential by user name for update
        ///</summary>
        ///<param name="email"></param>
        ///<param name="userId"></param>
        UserCredential GetUserCredentialByUserNameUpdate(string email,Guid userId);

        ///<summary>
        ///update user in db
        ///</summary>
        ///<param name="user"></param>
        void UpdateUser(User user);

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="id"></param>
        User GetUserById(Guid id);

        ///<summary>
        ///is email exist
        ///</summary>
        ///<param name="email"></param>
        bool IsEmailExist(string email);

        ///<summary>
        ///is email exist update
        ///</summary>
        ///<param name="email"></param>
        ///<param name="id"></param>
        bool IsEmailExistUpdate(string email,Guid id);

        ///<summary>
        ///get address ids of user
        ///</summary>
        ///<param name="id"></param>
        IEnumerable<Address> GetAddressIds(Guid id);

        ///<summary>
        ///get card ids of user
        ///</summary>
        ///<param name="id"></param>
        IEnumerable<CardDetail> GetCardIds(Guid id);

        ///<summary>
        ///save all changes
        ///</summary>
        bool Save();
    }
}
