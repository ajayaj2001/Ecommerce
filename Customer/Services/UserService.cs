using Customer.Entities.Dtos;
using Customer.Entities.Models;
using System;
using AutoMapper;
using System.Linq;
using Customer.Entities.ResponseTypes;
using Microsoft.Extensions.Logging;
using Customer.Contracts.Services;
using Customer.Contracts.Repositories;
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
        ///<param name="userDetails"></param>
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
        ///get single user detais
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
        ///update user details
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="userFromRepo"></param>
        ///<param name="userDetails"></param>
        public void UpdateUser(Guid userId, UpdateUserDto userDetails, User userFromRepo)
        {
            List<CardDetail> cardCollection = _userRepository.GetCardIds(userId).ToList();
            List<Address> addressCollection = _userRepository.GetAddressIds(userId).ToList();
            User userInput = _mapper.Map<User>(userDetails);
            userInput.Id = userFromRepo.Id;
            userInput.Credentials.UpdatedAt = DateTime.Now.ToString();

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
            _mapper.Map(userInput, userFromRepo);
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
            return new ValidateInputResponse() { errorMessage = "no error", errorCode = 200 };
        }

        ///<summary>
        ///validate user input in update user 
        ///</summary>
        ///<param name="user"></param>
        ///<param name="id"></param>
        public ValidateInputResponse ValidateUserInputUpdate(UpdateUserDto user, Guid id)
        {
            //check email 
            if (_userRepository.IsEmailExistUpdate(user.EmailAddress, id))
            {
                _logger.LogError("Email already exist");
                return new ValidateInputResponse() { errorMessage = "Email already exist", errorCode = 409 };
            }
            return new ValidateInputResponse() { errorMessage = "no error", errorCode = 200 };
        }
    }
}