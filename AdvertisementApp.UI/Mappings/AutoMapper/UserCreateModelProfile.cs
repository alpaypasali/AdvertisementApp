using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AdvertisementApp.UI.Mappings.AutoMapper
{
    public class UserCreateModelProfile:Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel , AppUserCreateDto> ();   
        }
    }
}
