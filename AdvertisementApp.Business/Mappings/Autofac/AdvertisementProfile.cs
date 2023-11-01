using AdvertisementApp.Dtos.AdvertisementDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Mappings.Autofac
{
    public class AdvertisementProfile:Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<Advertisement , AdvertisementCreateDto>().ReverseMap();
            CreateMap<Advertisement , AdvertisementUpdateDto>().ReverseMap();
            CreateMap<Advertisement , AdvertisementListDto>().ReverseMap();
          
        }
    }
}
