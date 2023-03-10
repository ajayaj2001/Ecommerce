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
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        ///<summary>
        ///get user credientials by user name
        ///</summary>
        ///<param name="email"></param>
        public UserCredential GetUserCredentialByEmail(string email)
        {
            return _context.credentials.Where(a => a.EmailAddress == email && a.IsActive).FirstOrDefault();
        }

        ///<summary>
        ///get user credientials by user id
        ///</summary>
        ///<param name="userId"></param>
        public UserCredential GetUserCredentialByUserId(Guid userId)
        {
            return _context.credentials.Where(a => a.UserId == userId && a.IsActive).FirstOrDefault();
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
        public bool Save(Guid creatorId)
        {
            _context.OnBeforeSaving(creatorId);
            return _context.SaveChanges() >= 0;
        }

        ///<summary>
        ///is email exist
        ///</summary>
        ///<param name="email"></param>
        public bool IsEmailExist(string email)
        {
            return _context.credentials.Any(e => e.EmailAddress == email && e.IsActive);
        }

        ///<summary>
        ///is email exist update
        ///</summary>
        ///<param name="email"></param>
        ///<param name="id"></param>
        public bool IsEmailExistUpdate(string email,Guid id)
        {
            return _context.credentials.Any(e => e.EmailAddress == email && e.UserId!=id && e.IsActive);
        }


        ///<summary>
        ///get address ids of user
        ///</summary>
        ///<param name="id"></param>
        public IEnumerable<Address> GetAddressIds(Guid id)
        {
            return _context.Addresses.Where(a => a.UserId == id && a.IsActive);
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

        ///<summary>
        ///get address by address id
        ///</summary>
        ///<param name="id"></param>
        public Address GetAddressById(Guid id)
        {
            return _context.Addresses.FirstOrDefault(b => b.Id == id && b.IsActive);
        }

        ///<summary>
        ///get card by card id
        ///</summary>
        ///<param name="id"></param>
        public CardDetail GetCardDetailById(Guid id)
        {
            return _context.Cards.FirstOrDefault(b => b.Id == id && b.IsActive);
        }



    }
}
