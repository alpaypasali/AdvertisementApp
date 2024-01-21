using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Extensions;
using AdvertisementApp.UI.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
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
            
                //Lookup tablo = çok fazla değişiklik olmayan
                var dto = _mapper.Map<AppUserCreateDto>(model);
                var createResponse = await _appUserService.CreateWithRoleAsync(dto,(int)RoleType.Member);
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

        public IActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {
            var result = await _appUserService.CheckUserAsync(appUserLoginDto);
            if (result.ResponseType  == Common.ResponseType.Success)
            {
                var roleResult = await _appUserService.GetRoleByUserIdAsync(result.Data.Id);
                var claims = new List<Claim>();

                if(roleResult.ResponseType == Common.ResponseType.Success)
                {

                    foreach(var role in roleResult.Data)
                    {

                        claims.Add(new Claim(ClaimTypes.Role, role.Definition));
                    }


                }

                claims.Add(new Claim(ClaimTypes.NameIdentifier , result.Data.Id.ToString()));
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                  

                    IsPersistent = appUserLoginDto.RememberMe,
                   
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result.Message);
            return View(appUserLoginDto);
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index" , "Home");
        }
    }
}
