﻿using AdvertisementApp.Common;
using AdvertisementApp.Dtos.Interfaces;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IService<CreateDto , UpdateDto , ListDto , T>
        where CreateDto : class,IDto , new()
        where UpdateDto : class,IUpdateDto, new()
        where ListDto : class,IDto , new()
        where T : BaseEntity
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto createDto);  
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto updateDto);

        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IResponse> RemoveAsync(int id);
        Task<IResponse<List<ListDto>>> GetAllAsync(); 


    }
}
