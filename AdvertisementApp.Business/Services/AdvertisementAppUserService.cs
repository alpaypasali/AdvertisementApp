using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos.AdvertisementAppUserDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class AdvertisementAppUserService: IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AdvertisementAppUserCreateDto> _createValidator;


        public AdvertisementAppUserService(IUow uow, IMapper mapper, IValidator<AdvertisementAppUserCreateDto> createValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {

            var result = _createValidator.Validate(dto);

            if (result.IsValid)
            {
                var control = await _uow.GetRepository<AdvertisementAppUser>().GetByFilter(x=>x.AppUserId == dto.AppUserId  && x.AdvertisementId == dto.AdvertisementId);
                if (control == null)
                {
                    var createdAdvertisementAppUser = _mapper.Map<AdvertisementAppUser>(dto);
                    await _uow.GetRepository<AdvertisementAppUser>().CreateAsync(createdAdvertisementAppUser);
                    await _uow.SaveChangesAsync();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);



                }
                List<CustomValidationError> errors = new List<CustomValidationError> {  new CustomValidationError
                {

                    ErrorMessage = "Daha önce başvurulan ilana tekrar başvurulamaz" ,
                    PropertyName = ""
                } };

                return new Response<AdvertisementAppUserCreateDto>(dto, errors);


            }

            return new Response<AdvertisementAppUserCreateDto>(dto, result.customValidationErrors());
        }

        public async Task<List<AdvertisementAppUserListDto>> GetList( AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();

           var list = await  query.Include(x=>x.Advertisement).Include(x=>x.AdvertisementAppUserStatus).Include(x=>x.MilitaryStatus).Include(x=>x.AppUser).ThenInclude(x=>x.Gender).Where
                (x=>x.AdvertisementAppUserStatusId == (int)type).ToListAsync();



            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);

        }

        public async Task SetStatusAsync(int advertisementAppUserId , AdvertisementAppUserStatusType type)
        {
            var unchanged = await _uow.GetRepository<AdvertisementAppUser>().Find(advertisementAppUserId);
            var changed = await _uow.GetRepository<AdvertisementAppUser>().GetByFilter(x => x.Id == advertisementAppUserId);
            changed.AdvertisementAppUserStatusId = (int)type;
            _uow.GetRepository<AdvertisementAppUser>().Uptade(changed, unchanged);
            await _uow.SaveChangesAsync();

        }
    }
}
