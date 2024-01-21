using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos.AdvertisementAppUserDtos;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation.AdvertisementAppUser
{
    public class AdvertisementAppUserCreateDtoValidator:AbstractValidator<AdvertisementAppUserCreateDto>
    {

        public AdvertisementAppUserCreateDtoValidator()
        {
            this.RuleFor(x=>x.AdvertisementId).NotEmpty();
            this.RuleFor(x=>x.AdvertisementAppUserStatusId).NotEmpty();
            this.RuleFor(x=>x.AppUserId).NotEmpty();
            this.RuleFor(x => x.CvPath).NotEmpty().WithMessage("Bir cv dosyasi seciniz.");
            this.RuleFor(x=>x.EndDate).NotEmpty().When(x=>x.MilitaryStatusId == (int)MilitaryStatusType.Tecilli).WithMessage("Tecil tarihi bos birakilamaz.");
           


        }
    }
}
