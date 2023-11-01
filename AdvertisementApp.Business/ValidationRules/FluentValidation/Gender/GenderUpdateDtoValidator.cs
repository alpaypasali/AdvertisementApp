using AdvertisementApp.Dtos.Gender;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation.Gender
{
    public class GenderUpdateDtoValidator : AbstractValidator<GenderUpdateDto>
    {
        public GenderUpdateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
