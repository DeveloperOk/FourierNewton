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

            if (string.IsNullOrEmpty(email) == true)
            {

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            }
            else if (string.IsNullOrEmpty(email.Trim()) == true)
            {

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            }
            else if (AccountInformationManager.IsThereAnyRecordWithGivenEmail(email.Trim()))
            {

                SignUpViewModel signUpViewModel = new SignUpViewModel();
                signUpViewModel.IsError = true;
                signUpViewModel.ErrorMessage = SignConstants.EmailExistsMessage;

                view = View("/Views/Sign/SignUp.cshtml", signUpViewModel);

            }
            else
            {

                view = View("/Views/Sign/SignUpProcess.cshtml");

                string password = CryptographyManager.GeneratePassword().Trim();
                string encryptedPassword = CryptographyManager.EncryptStringWithAes(password);

                var accountInformation = new AccountInformation();
                accountInformation.Email = email.Trim();
                accountInformation.Password = encryptedPassword;
                accountInformation.Date = DateFN.GetCurrentDate();

                AccountInformationManager.InsertData(accountInformation);

                EmailManager.SendEmailForAccountGeneration(email.Trim(), password);
            }

            return view;
        }

        public IActionResult ResetPassword()
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
            resetPasswordViewModel.IsError = false;

            return View(resetPasswordViewModel);
        }

        public IActionResult ResetPasswordProcess(string email)
        {

            ViewResult view;

            if (string.IsNullOrEmpty(email) == true)
            {

                ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
                resetPasswordViewModel.IsError = true;
                resetPasswordViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/ResetPassword.cshtml", resetPasswordViewModel);

            }
            else if (string.IsNullOrEmpty(email.Trim()) == true)
            {

                ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
                resetPasswordViewModel.IsError = true;
                resetPasswordViewModel.ErrorMessage = SignConstants.EmailIsEmptyOrNullMessage;

                view = View("/Views/Sign/ResetPassword.cshtml", resetPasswordViewModel);

            }
            else if (AccountInformationManager.IsThereAnyRecordWithGivenEmail(email.Trim()) == false)
            {

                ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
                resetPasswordViewModel.IsError = true;
                resetPasswordViewModel.ErrorMessage = SignConstants.EmailDoesNotExistMessage;

                view = View("/Views/Sign/ResetPassword.cshtml", resetPasswordViewModel);

            }
            else
            {

                view = View("/Views/Sign/ResetPasswordProcess.cshtml");

                string password = CryptographyManager.GeneratePassword().Trim();
                string encryptedPassword = CryptographyManager.EncryptStringWithAes(password);

                var accountInformation = new AccountInformation();
                accountInformation.Email = email.Trim();
                accountInformation.Password = encryptedPassword;
                accountInformation.Date = DateFN.GetCurrentDate();

                AccountInformationManager.UpdatePassword(accountInformation);

                EmailManager.SendEmailForResettingPassword(email.Trim(), password);
            }

            return view;
        }

        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel();
            changePasswordViewModel.IsError = false;

            return View(changePasswordViewModel);
        }

        public IActionResult ChangePasswordProcess(string email, string currentPassword, string newPassword, string repeatedNewPassword)
        {

            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel();
            changePasswordViewModel.IsError = false;
            changePasswordViewModel.ErrorMessage = "";

            if (string.IsNullOrEmpty(email) == true)
            {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.EmailIsEmptyOrNullMessage + SignConstants.Newline;

            }
            else if (string.IsNullOrEmpty(email.Trim()) == true)
            {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.EmailIsEmptyOrNullMessage + SignConstants.Newline;

            }
            else if (AccountInformationManager.IsThereAnyRecordWithGivenEmail(email.Trim()) == false)
            {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.EmailDoesNotExistMessage + SignConstants.Newline;

            }

            
            if (string.IsNullOrEmpty(currentPassword) == true)
            {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.CurrentPasswordCanNotBeNullOrEmpty + SignConstants.Newline;

            }
            else if (string.IsNullOrEmpty(currentPassword.Trim()) == true)
            {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.CurrentPasswordCanNotBeNullOrEmpty + SignConstants.Newline;
            }
            else if(string.IsNullOrEmpty(email) == false){

                if (isCurrentPasswordCorrect(currentPassword.Trim(), email.Trim()) == false)
                {

                    changePasswordViewModel.IsError = true;
                    changePasswordViewModel.ErrorMessage += SignConstants.CurrentPasswordIsNotCorrect + SignConstants.Newline;

                }

            }


            if (string.IsNullOrEmpty(newPassword) == true || string.IsNullOrEmpty(repeatedNewPassword) == true) {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.NewPasswordCanNotBeNullOrEmpty + SignConstants.Newline;

            } else if (string.IsNullOrEmpty(newPassword.Trim()) == true || string.IsNullOrEmpty(repeatedNewPassword.Trim()) == true) {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.NewPasswordCanNotBeNullOrEmpty + SignConstants.Newline;

            } else if (string.Equals(newPassword.Trim(), repeatedNewPassword.Trim()) == false) {

                changePasswordViewModel.IsError = true;
                changePasswordViewModel.ErrorMessage += SignConstants.NewPasswordsDoNotMatch + SignConstants.Newline;

            }


            ViewResult view;

            if (changePasswordViewModel.IsError == true) { 

                view = View("/Views/Sign/ChangePassword.cshtml", changePasswordViewModel);

            }
            else
            {

                view = View("/Views/Sign/ChangePasswordProcess.cshtml");

                string encryptedPassword = CryptographyManager.EncryptStringWithAes(newPassword.Trim());

                var accountInformation = new AccountInformation();
                accountInformation.Email = email.Trim();
                accountInformation.Password = encryptedPassword;
                accountInformation.Date = DateFN.GetCurrentDate();

                AccountInformationManager.UpdatePassword(accountInformation);

            }


            return view;
        }


        private bool isCurrentPasswordCorrect(string currentPassword, string email) {

            bool currentPasswordCorrectFlag = false;

            var listOfAccountInformation = AccountInformationManager.GetDataWithGivenEmail(email);
            if (listOfAccountInformation != null) {

                if (listOfAccountInformation.Any() == true)
                {
                    var accountInformation = listOfAccountInformation.First();

                    if (accountInformation != null)
                    {

                        var decryptedPassword = CryptographyManager.DecryptStringWithAes(accountInformation.Password);
                        if (string.Equals(currentPassword, decryptedPassword) == true)
                        {

                            currentPasswordCorrectFlag = true;
                        }

                    }
                }

            }


            return currentPasswordCorrectFlag;
                    
        }




    }

}
