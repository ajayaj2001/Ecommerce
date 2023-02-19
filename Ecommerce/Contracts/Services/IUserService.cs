using Customer.Entities.Dtos;
using System;
using Customer.Entities.Models;
using Customer.Entities.ResponseTypes;
using Order.Entities.Dtos;

namespace Customer.Contracts.Services
{
    public interface IUserService
    {
        ///<summary>
        ///create new user in db
        ///</summary>
        ///<param name="user"></param>
        Guid CreateUser(CreateUserDto user);

        ///<summary>
        ///update user in db
        ///</summary>
        ///<param name="userDetils"></param>
        ///<param name="userFromRepo"></param>
        ///<param name="userId"></param>
        
        void UpdateUser(Guid userId, UpdateUserDto userDetils);

        ///<summary>
        ///get single user detail 
        ///</summary>
        ///<param name="user"></param>
        UserDto FetchSingleCustomerDetail(Guid userId);

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="userId"></param>
        User GetUserById(Guid userId);

        ///<summary>
        ///validate user input for update  
        ///</summary>
        ///<param name="user"></param>
        ///<param name="id"></param>
        ValidateInputResponse ValidateUserInputUpdate(UpdateUserDto user, Guid id);

        ///<summary>
        ///validate user input for create
        ///</summary>
        ///<param name="user"></param>
        ValidateInputResponse ValidateUserInputCreate(CreateUserDto user);

        ///<summary>
        ///Get Order Details
        ///</summary>
        ///<param name="orderDetail"></param>
        ResultOrderDto GetOrderDetails(GetOrderDetailsDto orderDetail);

    }
}
