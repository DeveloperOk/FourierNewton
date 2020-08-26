using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourierNewton.Common.Cryptography;
using FourierNewton.Common.Database.AccountInformation;
using FourierNewton.Common.DateFN;
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

            if (string.IsNullOrEmpty(email) == true) {

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            } else if (string.IsNullOrEmpty(email.Trim()) == true) {

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            } else if (AccountInformationManager.IsThereAnyRecordWithGivenEmail(email.Trim())) {

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailExistsMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            } else {

                view = View("/Views/Sign/SignUpProcess.cshtml");
                
                string password = CryptographyManager.GeneratePassword().Trim();
                string encryptedPassword = CryptographyManager.EncryptStringWithAes(password);

                var accountInformation = new AccountInformation();
                accountInformation.Email = email.Trim();
                accountInformation.Password = encryptedPassword;
                accountInformation.Date = DateFN.GetCurrentDate();

                AccountInformationManager.InsertData(accountInformation);

                EmailManager.SendEmail(email.Trim(), password);
            }

            return view;
        }


    }
}
