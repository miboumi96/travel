﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Data.Static;
using TravelAgency.Data.ViewModels;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<TravelUser> _userManager;
        private readonly SignInManager<TravelUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<TravelUser> userManager, SignInManager<TravelUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login() => View(new LoginUser());

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }


        public IActionResult Register() => View(new RegisterUser());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new TravelUser()
            {
                FName = registerVM.FName,
                LName = registerVM.LName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
