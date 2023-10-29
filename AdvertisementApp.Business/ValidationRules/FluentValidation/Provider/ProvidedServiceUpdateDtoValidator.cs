using AdvertisementApp.Dtos.ProvidedServiceDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation.Provider
{
    public class ProvidedServiceUpdateDtoValidator:AbstractValidator<ProvidedServiceUpdateDto>
    {
        public ProvidedServiceUpdateDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ImagePath).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
