using Ecom_core_prac.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Ecom_core_prac.Controllers
{
    public class AuthController : Controller
    {
        private readonly BaseAccount _baseAccount;

        public AuthController(IConfiguration configuration)
        {
            _baseAccount = new BaseAccount(configuration);
        }

      
        [HttpGet]
        public ActionResult ResetPassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DoReset(string txtUserName, string newPassword, string confirmPassword)
        {
            
            string Message = "Unauthorized";
            if (newPassword != confirmPassword)
            {
                ViewBag.Message = "Passwords do not match!";
                return View("ResetPassword");
            }
            
            if (_baseAccount.VerifyUser2(txtUserName, newPassword))
            {
                Message = "Authorized";
                HttpContext.Session.SetString("UserName", txtUserName);
                //return Redirect("www.google.com"); 
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = Message;
            //return RedirectToAction("Dashboard", "Inventory");
            return View("Login");
        }
        [HttpPost]
        public IActionResult SaveCustomer()
        {
            try
            {
                var formCollection = Request.Form;
                bool result = _baseAccount.SaveCustomer(formCollection);
                return Json(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in SaveCustomer: " + ex.Message);
                return Json(false);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");

            return View("Login");
        }
        [HttpPost]
        public IActionResult DoLogin(string txtUserName, string txtPassword)
        {
            string Message = "Unauthorized";

            if (_baseAccount.VerifyUser(txtUserName, txtPassword))
            {
                Message = "Authorized";
                HttpContext.Session.SetString("UserName", txtUserName);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = Message;
            return View("Login");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
       
        [HttpPost]
        
        public ActionResult DoSignUp(IFormCollection frmColl)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(frmColl["Password"]))
                {
                    ViewBag.Message = "Password is required.";
                    return View("SignUp");
                }

                // Password validation
                string password = frmColl["Password"].ToString().Trim();
                string confirmPassword = frmColl["ConfirmPassword"].ToString().Trim();

                if (password != confirmPassword)
                {
                    ViewBag.Message = "Password and Confirm Password do not match.";
                    return View("SignUp");
                }

                // Additional password strength validation (recommended)
               

                // Save user to database
                bool isSaved = _baseAccount.SaveUserToDatabase(frmColl);

                if (isSaved)
                {
                    // Set session and redirect
                    HttpContext.Session.SetString("UserName", frmColl["Name"]);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Message = "Registration failed. Please try again.";
                return View("Login");
            }
            catch (Exception ex)
            {
                // Log the error (you should implement proper logging)
                // _logger.LogError(ex, "Error during signup");

                ViewBag.Message = "An error occurred during registration. Please try again.";
                return View("Login");
            }
        }
    }
}
