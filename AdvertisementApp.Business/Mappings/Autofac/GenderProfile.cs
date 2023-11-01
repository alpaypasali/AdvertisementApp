using AdvertisementApp.Dtos.Gender;
using AdvertisementApp.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Mappings.Autofac
{
    public class GenderProfile:Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, GenderCreateDto>().ReverseMap();
            CreateMap<Gender, GenderListDto>().ReverseMap();
            CreateMap<Gender, GenderUpdateDto>().ReverseMap();

        }
    }
}
