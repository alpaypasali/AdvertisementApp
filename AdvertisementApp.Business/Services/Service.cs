﻿using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos.Interfaces;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class Service<CreateDto, UpdateDto, ListDto , T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class , IDto , new()
        where UpdateDto : class ,IUpdateDto , new()
        where ListDto : class , IDto , new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto createDto)
        {
           var result = _createDtoValidator.Validate(createDto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(createDto);
                await _uow.GetRepository<T>().CreateAsync(createdEntity);
                return new Response<CreateDto>(ResponseType.Success, createDto);
            }
            return new Response<CreateDto>(createDto , result.customValidationErrors());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);

            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilter(x=> x.Id == id);
            if(data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye sahip data bulunamadı");

            }
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success,dto);
        }

        public async Task<IResponse> Remove(int id)
        {
            var data = await _uow.GetRepository<T>().Find(id);
            if (data == null)
            {
                return new Response(ResponseType.NotFound, $"{id} ye sahip data bulunamadı");

            }
            _uow.GetRepository<T>().Remove(data);
            return new Response(ResponseType.Success);
         
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto updateDto)
        {
            var result =_updateDtoValidator.Validate(updateDto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<T>().Find(updateDto.Id);
                if (unchangedData == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{updateDto.Id} ye sahip data bulunamadı");
                }
                var data = _mapper.Map<T>(updateDto);
                _uow.GetRepository<T>().Uptade(data, unchangedData);
                return new Response<UpdateDto>(ResponseType.Success, updateDto);
            }
            return new Response<UpdateDto>(updateDto, result.customValidationErrors());
        }
    }
}