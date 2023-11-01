using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.ValidationRules.FluentValidation.AppUser;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class AppUserService:Service<AppUserCreateDto, AppUserUpdateDto , AppUserListDto , AppUser>,IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<AppUserCreateDto> _validator;
        public AppUserService(IMapper mapper , IValidator<AppUserCreateDto> createDtoValidator,IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow ):base(mapper, createDtoValidator ,updateDtoValidator,uow)
        {
            _mapper = mapper;
            _uow = uow;
            _validator = createDtoValidator;
        }

        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto , int roleId)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user = _mapper.Map<AppUser>(dto);
                user.AppUserRoles = new List<AppUserRole>();
                user.AppUserRoles.Add(new AppUserRole
                {
                AppUser = user,
                AppRoleId = roleId


                });
               await _uow.GetRepository<AppUser>().CreateAsync(user);
               
                await _uow.SaveChangesAsync();
                return new Response<AppUserCreateDto>(ResponseType.Success, dto);
            }
            return new Response<AppUserCreateDto>(dto, validationResult.customValidationErrors());


        }
    }
}
