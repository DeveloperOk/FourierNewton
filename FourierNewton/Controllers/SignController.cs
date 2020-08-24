using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourierNewton.Common.Email;
using FourierNewton.Common.Sign;
using FourierNewton.Models.Sign;
using Microsoft.AspNetCore.Mvc;

namespace FourierNewton.Controllers
{
    public class SignController : Controller
    {

        public IActionResult SignUp()
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            signUpViewModel.IsError = false;

            return View(signUpViewModel);
        }

       
        public IActionResult SignUpProcess(string email)
        {

            ViewResult view;

            if (string.IsNullOrEmpty(email) == true){

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            } else {

                view = View("/Views/Sign/SignUpProcess.cshtml");
                EmailManager.SendEmail(email);
            }

            return view;
        }


    }
}
