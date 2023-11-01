using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Extensions;
using AdvertisementApp.UI.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {

        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _validator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> validator, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _validator = validator;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        public async Task<IActionResult> SignUp()
        {
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateModel();
            model.Genders = new SelectList(response.Data,"Id","Definition");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            var result = _validator.Validate(model);
            if(result.IsValid) { 
            
                var dto = _mapper.Map<AppUserCreateDto>(model);
                var createResponse = await _appUserService.CreateWithRoleAsync(dto,2);
                return this.ResponseRedirectAction(createResponse, "SignIn");
            
            
            }
            foreach(var error in result.Errors)
            {


                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            var response =await _genderService.GetAllAsync();
            model.Genders = new SelectList(response.Data, "Id", "Definition",model.GenderId);
            return View(model);
        }
    }
}
