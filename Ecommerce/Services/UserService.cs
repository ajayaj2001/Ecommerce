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
using Order.Entities.Dtos;

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
            _userRepository.CreateUser(user);
            _userRepository.Save(user.Id);
            return user.Id;
        }

        ///<summary>
        ///get single user detais
        ///</summary>
        ///<param name="user"></param>
        public UserDto FetchSingleCustomerDetail(Guid userId)
        {
            User user=_userRepository.GetUserById(userId);
            UserCredential userCredential = _userRepository.GetUserCredentialByUserId(user.Id);
            user.Credentials.Id = userCredential.Id;
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
        public void UpdateUser(Guid userId, UpdateUserDto userDetails)
        {
            User userFromRepo = _userRepository.GetUserById(userId);
            List<CardDetail> cardCollection = _userRepository.GetCardIds(userId).ToList();
            List<Address> addressCollection = _userRepository.GetAddressIds(userId).ToList();
            userFromRepo.Credentials = _userRepository.GetUserCredentialByUserId(userFromRepo.Id);
            userDetails.Credentials.Id = userFromRepo.Credentials.Id;
            //User userInput = _mapper.Map<User>(userDetails);
            userDetails.Id = userFromRepo.Id;

            cardCollection.Select((value, i) =>
            {
                userDetails.CardDetails.ElementAt(i).Id = value.Id;
                return value;
            });
            addressCollection.Select((value, i) =>
            {
                userDetails.Addresses.ElementAt(i).Id = value.Id;
                return value;
            });
            _mapper.Map(userDetails, userFromRepo);
            _userRepository.UpdateUser(userFromRepo);
            _userRepository.Save(userId);
        }

        ///<summary>
        ///validate user input in create user 
        ///</summary>
        ///<param name="user"></param>
        public ValidateInputResponse ValidateUserInputCreate(CreateUserDto user)
        {
            //check email 
            if (_userRepository.IsEmailExist(user.Credentials.EmailAddress))
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
            if (_userRepository.IsEmailExistUpdate(user.Credentials.EmailAddress,id))
            {
                _logger.LogError("Email already exist");
                return new ValidateInputResponse() { errorMessage = "Email already exist", errorCode = 409 };
            }
            return new ValidateInputResponse() { errorMessage = "no error", errorCode = 200 };
        }


        ///<summary>
        ///Get Order Details
        ///</summary>
        ///<param name="orderDetail"></param>
        public ResultOrderDto GetOrderDetails(GetOrderDetailsDto orderDetail)
        {
           ResultOrderDto resultOrder = new ResultOrderDto();

            CardDetail card = _userRepository.GetCardDetailById(orderDetail.CardId);
            if (card != null)
                resultOrder.CardDetail = _mapper.Map<CardDto>(card);
            else
                resultOrder.CardDetail = null;

            Address address= _userRepository.GetAddressById(orderDetail.AddressId);
            if (address != null)
                resultOrder.address = _mapper.Map<AddressDto>(address);
            else
                resultOrder.address = null;

            return resultOrder;
        }
    }
}