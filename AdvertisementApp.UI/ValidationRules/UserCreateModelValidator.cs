using AdvertisementApp.UI.Models;
using FluentValidation;
using System;

namespace AdvertisementApp.UI.ValidationRules
{
    public class UserCreateModelValidator:AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x=>x.Password).NotEmpty();
            RuleFor(x=>x.Password).MinimumLength(3);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Password ot match");
            RuleFor(x => new { x.UserName, x.FirstName }

            ).Must(x =>  CannotFirstName(x.UserName , x.FirstName)).WithMessage("UserName contains firstname");
                 
            RuleFor(x=>x.GenderId).NotEmpty();
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.SurName).NotEmpty();
          

        }

        private bool CannotFirstName(string userName , string firstName)
        {
            return !userName.Contains(firstName);    
        }

       
    }
}
