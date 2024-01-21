using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.ValidationRules.FluentValidation.AppUser;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos.AppRoleDtos;
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
        private readonly IValidator<AppUserLoginDto> _loginDtoValidator;

        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow, IValidator<AppUserLoginDto> loginDtoValidator) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _mapper = mapper;
            _uow = uow;
            _validator = createDtoValidator;
            _loginDtoValidator = loginDtoValidator;
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
        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto loginDto)
        {
            var validationResult = _loginDtoValidator.Validate(loginDto);
            if (validationResult.IsValid)
            {
              var user =  await _uow.GetRepository<AppUser>().GetByFilter(x=>x.UserName == loginDto.UserName && x.Password == loginDto.Password);
                if(user != null)
                {
                    var appUserDto = _mapper.Map<AppUserListDto>(user);
                    return new Response<AppUserListDto>(ResponseType.Success, appUserDto);

                }
                return new Response<AppUserListDto>(ResponseType.NotFound, "Kullanıcı adı veya şifre hatalı!");

            }
            return new Response<AppUserListDto>(ResponseType.ValidationError, "Kullanıcı Adı veya şifre bol olamaz!");


        }

        public async Task<IResponse<List<AppRoleListDto>>> GetRoleByUserIdAsync(int userId)
        {
        var result =  await  _uow.GetRepository<AppRole>().GetAllAsync(x=>x.AppUserRoles.Any(x=>x.AppUserId == userId));
            if(result == null)
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "İlgili rol bulunamadı.");

            }
            var dto = _mapper.Map<List<AppRoleListDto>>(result);
            return new Response<List<AppRoleListDto>>(ResponseType.Success, dto);
        }
    }
}
