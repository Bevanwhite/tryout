﻿using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AuthController: Controller
    {
		private readonly SignInManager<IdentityUser> _signInManager;

		public AuthController(SignInManager<IdentityUser> signInManager)
		{
            _signInManager = signInManager;
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel login)
		{
			var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
