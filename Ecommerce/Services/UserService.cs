﻿using Customer.Entities.Dtos;
using Customer.Entities.Models;
using System;
using AutoMapper;
using System.Linq;
using Customer.Entities.ResponseTypes;
using Microsoft.Extensions.Logging;
using Customer.Contracts.Services;
using Customer.Contracts.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Customer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IMapper mapper, IUserRepository UserRepository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary>
        ///create new user in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="user"></param>
        public Guid CreateUser(CreateUserDto userDetails)
        {
            User user = _mapper.Map<User>(userDetails);
            user.CreatedAt = DateTime.Now.ToString();
            user.Credentials.CreatedAt = DateTime.Now.ToString();
            user.Credentials.CreatedBy = Guid.NewGuid();

            foreach (CardDetail item in user.CardDetails)
            {
                item.CreatedAt = DateTime.Now.ToString();
            }
            foreach (Address item in user.Addresses)
            {
                item.CreatedAt = DateTime.Now.ToString();
            }
            _userRepository.CreateUser(user);
            _userRepository.Save();
            // UserDto userDetail = _mapper.Map<UserDto>(user);
            return user.Id;
        }

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        public UserCredential GetUserByUserName(string userName)
        {
            return _userRepository.GetUserCredentialByUserName(userName);

        }

        ///<summary>
        ///get all address book based on filter
        ///</summary>
        ///<param name="pageSortParam"></param>
        //public List<User> GetAllCustomer(PageSortParam pageSortParam)
        //{
        //    IEnumerable<User> foundedUserList = _userRepository.GetAllUsers();
        //    //PaginationHandler<User> list = new PaginationHandler<User>(pageSortParam);
        //    //return list.GetData(foundedUserList);
        //    return foundedUserList;
        //}

        ///<summary>
        ///update address book in database
        ///</summary>
        ///<param name="users"></param>
        /*public IEnumerable<UserDto> FetchCustomerDetail(List<User> users)
        {
            foreach (User user in users)
            {
                user.Emails = _userRepository.GetEmailIds(user.Id).ToList();
                user.Addresses = _userRepository.GetAddressIds(user.Id).ToList();
                user.Phones = _userRepository.GetPhoneIds(user.Id).ToList();
                user.Assets = _userRepository.GetAssetIds(user.Id).ToList();
            }
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }*/

        ///<summary>
        ///get single address book detais
        ///</summary>
        ///<param name="user"></param>
        public UserDto FetchSingleCustomerDetail(User user)
        {
            UserCredential temp = _userRepository.GetUserCredentialByUserId(user.Id);
            user.Credentials.Id = temp.Id;
            user.Addresses = _userRepository.GetAddressIds(user.Id).ToList();
            user.CardDetails = _userRepository.GetCardIds(user.Id).ToList();
            return _mapper.Map<UserDto>(user);
        }

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="userId"></param>
        public User GetUserById(Guid userId)
        {
            return _userRepository.GetUserById(userId);
        }

        ///<summary>
        ///delete address book in database
        ///</summary>
        ///<param name="user"></param>
        public void DeleteCustomer(Guid userId)
        {
            User userFromRepo = _userRepository.GetUserById(userId);
            userFromRepo.IsActive = false;
            _userRepository.Save();
        }

        ///<summary>
        ///convert user to userDto
        ///</summary>
        ///<param name="user"></param>
        //public void UserToUserDto(Guid userId)
        //{
        //    User userFromRepo = _userRepository.GetUserById(userId);
        //    userFromRepo.IsActive = false;
        //    _userRepository.Save();
        //}

        ///<summary>
        ///update address book details
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="userFromRepo"></param>
        ///<param name="userDetails"></param>
        public void UpdateUser(Guid userId, UpdateUserDto userDetils, User userFromRepo)
        {
            List<CardDetail> cardCollection = _userRepository.GetCardIds(userId).ToList();
            List<Address> addressCollection = _userRepository.GetAddressIds(userId).ToList();
            User userInput = _mapper.Map<User>(userDetils);
            userInput.Id = userFromRepo.Id;
            //userInput.Credentials.Id = userFromRepo.Credentials.Id;
            userInput.Credentials.UpdatedAt= DateTime.Now.ToString();

            foreach (CardDetail item in userInput.CardDetails)
            {
                item.UpdatedAt = DateTime.Now.ToString();
            }
            foreach (Address item in userInput.Addresses)
            {
                item.UpdatedAt = DateTime.Now.ToString();
            }
            cardCollection.Select((value, i) =>
            {
                userInput.CardDetails.ElementAt(i).Id = value.Id;
                return value;
            });
            addressCollection.Select((value, i) =>
            {
                userInput.Addresses.ElementAt(i).Id = value.Id;
                return value;
            });
            _mapper.Map(userInput,userFromRepo);
            userFromRepo.UpdatedAt = DateTime.Now.ToString();
            _userRepository.UpdateUser(userFromRepo);
            _userRepository.Save();
        }

        ///<summary>
        ///validate user input in create user 
        ///</summary>
        ///<param name="user"></param>
        public ValidateInputResponse ValidateUserInputCreate(CreateUserDto user)
        {
            //check email 
            UserCredential userCredientials = _userRepository.GetUserCredentialByUserName(user.Credentials.UserName);
            if (userCredientials != null)
            {
                _logger.LogError("user name already exist");
                return new ValidateInputResponse() { errorMessage = "user name already exist", errorCode = 409 };
            }
            //check email 
            if (_userRepository.IsEmailExist(user.EmailAddress))
            {
                _logger.LogError("Email already exist");
                return new ValidateInputResponse() { errorMessage = "Email already exist", errorCode = 409 };
            }
            if (user.CardDetails.GroupBy(x => x.CardNumber).Any(g => g.Count() > 1))
            {
                _logger.LogError("Card already exist");
                return new ValidateInputResponse() { errorMessage = "Card already exist", errorCode = 409 };
            }
            return new ValidateInputResponse() { errorMessage = "no error", errorCode = 200 };
        }

        ///<summary>
        ///validate user input in update user 
        ///</summary>
        ///<param name="user"></param>
        ///<param name="id"></param>
        public ValidateInputResponse ValidateUserInputUpdate(UpdateUserDto user, Guid id)
        {
            //check username exist 
            UserCredential userCredientials = _userRepository.GetUserCredentialByUserNameUpdate(user.Credentials.UserName, id);
            if (userCredientials != null)
            {
                _logger.LogError("user name already exist");
                return new ValidateInputResponse() { errorMessage = "user name already exist", errorCode = 409 };
            }
            //check email 
            if (_userRepository.IsEmailExistUpdate(user.EmailAddress, id))
            {
                _logger.LogError("Email already exist");
                return new ValidateInputResponse() { errorMessage = "Email already exist", errorCode = 409 };
            }

            if (user.Addresses.GroupBy(x => x.Type).Any(g => g.Count() > 1))
            {
                _logger.LogError("Address Type already exist");
                return new ValidateInputResponse() { errorMessage = "Address Type already exist", errorCode = 409 };
            }
            if (user.CardDetails.GroupBy(x => x.Type).Any(g => g.Count() > 1))
            {
                _logger.LogError("Card Type already exist");
                return new ValidateInputResponse() { errorMessage = "Card Type already exist", errorCode = 409 };
            }
            return new ValidateInputResponse() { errorMessage = "no error", errorCode = 200 };
        }
    }
}