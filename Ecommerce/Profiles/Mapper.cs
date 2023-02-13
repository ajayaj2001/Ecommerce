using AutoMapper;
using Customer.Entities.Models;
using Customer.Entities.Dtos;
using System;

namespace Customer.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //user
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>().ReverseMap();

            //card details
            CreateMap<CreateCardDto, CardDetail>().ReverseMap();
            CreateMap<UpdateCardDto, CardDetail>().ReverseMap();
            CreateMap<CardDto, CardDetail>().ReverseMap();

            //user creditentals
            CreateMap<CreateUserCredentialDto, UserCredential>().ReverseMap();
            CreateMap<UpdateUserCredentialDto, UserCredential>().ReverseMap();
            CreateMap<UserCredentialDto, UserCredential>().ReverseMap();

            //address
            CreateMap<CreateAddressDto, Address>().ReverseMap();
            CreateMap<UpdateAddressDto, Address>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();

        }
    }
}
