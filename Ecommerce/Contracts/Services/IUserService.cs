using Customer.Entities.Dtos;
using System;
using Customer.Entities.Models;
using Customer.Entities.ResponseTypes;

namespace Customer.Contracts.Services
{
    public interface IUserService
    {
        ///<summary>
        ///create new user in db
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="user"></param>
        Guid CreateUser(CreateUserDto user);

        ///<summary>
        ///update user in db
        ///</summary>
        ///<param name="userDetils"></param>
        ///<param name="userFromRepo"></param>
        
        void UpdateUser(Guid userId, UpdateUserDto userDetils, User userFromRepo);

        ///<summary>
        ///get user by user name
        ///</summary>
        ///<param name="userName"></param>
        UserCredential GetUserByUserName(string userName);

        ///<summary>
        ///get all address book based on filter
        ///</summary>
        ///<param name="pageSortParam"></param>
        ///List<User> GetAllCustomer(PageSortParam pageSortParam);

        ///<summary>
        ///update address book in database
        ///</summary>
        ///<param name="users"></param>
        ///IEnumerable<UserDto> FetchAddressBookDetail(List<User> users);

        ///<summary>
        ///get single address book detais
        ///</summary>
        ///<param name="user"></param>
        UserDto FetchSingleCustomerDetail(User user);

        ///<summary>
        ///delete address book in database
        ///</summary>
        ///<param name="user"></param>
        ///void DeleteAddressBook(Guid userId);

        ///<summary>
        ///get user by user id
        ///</summary>
        ///<param name="userId"></param>
        User GetUserById(Guid userId);

        ///<summary>
        ///update address book details
        ///</summary>
        ///<param name="authId"></param>
        ///<param name="userId"></param>
        ///<param name="userFromRepo"></param>
        ///<param name="userInput"></param>
        ///void UpdateAddressBook(Guid userId, User userInput, User userFromRepo, Guid authId);

        ///<summary>
        ///validate user input in update user 
        ///</summary>
        ///<param name="user"></param>
        ///<param name="id"></param>
        ValidateInputResponse ValidateUserInputUpdate(UpdateUserDto user, Guid id);

        ///<summary>
        ///fetch user details for create 
        ///</summary>
        ///<param name="user"></param>
        ValidateInputResponse ValidateUserInputCreate(CreateUserDto user);

    }
}
