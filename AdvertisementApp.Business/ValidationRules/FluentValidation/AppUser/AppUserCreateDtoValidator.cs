﻿using AdvertisementApp.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation.AppUser
{
    public class AppUserCreateDtoValidator:AbstractValidator<AppUserCreateDto>
    {
        public AppUserCreateDtoValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.GenderId).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
            RuleFor(x=>x.PhoneNumber).NotEmpty();
            RuleFor(x=>x.SurName).NotEmpty();
            RuleFor(x=>x.UserName).NotEmpty();
        }
    }
   
}
