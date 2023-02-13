using System;
using System.Collections.Generic;
using System.Linq;
using Customer.Contracts.Repositories;
using Customer.DbContexts;
using Customer.Entities.Models;

namespace Customer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CustomerContext _context;

        public UserRepository(CustomerContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///to create user in db
        ///</summary>
        ///<param name="user"></param>
        // user operation
        public void CreateUser(User user)
        {
            _context.Users.Add(user);

        }

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        public UserCredential GetUserCredentialByUserName(string userName)
        {
            UserCredential user = _context.credentials.Where(a => a.UserName == userName && a.IsActive).FirstOrDefault();
            return user;
        }

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        ///param name="userId"></param>
        public UserCredential GetUserCredentialByUserNameUpdate(string userName,Guid userId)
        {
            UserCredential user = _context.credentials.Where(a => a.UserName == userName &&a.UserId!=userId && a.IsActive).FirstOrDefault();
            return user;
        }

        ///<summary>
        ///get user credientials by user id
        ///</summary>
        ///<param name="userName"></param>
        ///param name="userId"></param>
        public UserCredential GetUserCredentialByUserId(Guid userId)
        {
            UserCredential user = _context.credentials.Where(a => a.UserId == userId && a.IsActive).FirstOrDefault();
            return user;
        }

        ///<summary>
        ///update user in db
        ///</summary>
        ///<param name="user"></param>
        public void UpdateUser(User user)
        {

            _context.Users.Update(user);
        }

        ///<summary>
        ///save all changes
        ///</summary>
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        ///<summary>
        ///is email exist
        ///</summary>
        ///<param name="email"></param>
        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(e => e.EmailAddress == email && e.IsActive);
        }

        ///<summary>
        ///is email exist
        ///</summary>
        ///<param name="email"></param>
        ///param name="id"></param>
        public bool IsEmailExistUpdate(string email,Guid id)
        {
            return _context.Users.Any(e => e.EmailAddress == email && e.Id!=id && e.IsActive);
        }

        ///<summary>
        ///get address ids of user
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<Address> GetAddressIds(Guid id)
        {
            return _context.Addresses.Where(a => a.UserId == id && a.IsActive );
        }

        ///<summary>
        ///get card ids of user
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<CardDetail> GetCardIds(Guid id)
        {
            return _context.Cards.Where(a => a.UserId == id && a.IsActive);
        }

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="id"></param>
        public User GetUserById(Guid id)
        {

            return _context.Users.FirstOrDefault(b => b.Id == id && b.IsActive);
        }

    }
}
