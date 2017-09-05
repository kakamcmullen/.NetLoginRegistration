using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Registration.Models;  //allows access to Models inside Registration namespace
using System.Linq;

namespace Registration.Controllers
{
    public class RegistrationController : Controller
    {
//=======================LOGIN-REGISTRATION====================================//
        private RegistrationContext _context;

        public RegistrationController(RegistrationContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
//=======================CREATE NEW USER======================================//
        [HttpPost]
        [Route("/create")]
        public IActionResult Create(RegisterViewModel model)
        {
            if(ModelState.IsValid){
            //Add the User to the database
            User NewUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                DOB = model.DOB,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            
//==========CHECK FOR USERNAME IN DB=========//
            if(model.UserName != null)
            {
                User CheckUserName = _context.Users.SingleOrDefault(user => user.UserName == model.UserName);
                if(CheckUserName != null)
                {
                ViewBag.UNErrors = "UserName is already registered";
                return View("Index");
                }
            };
            
//==========CHECK FOR EMAIL IN DB============//            
            if(model.Email != null)
            {
                User CheckEmail = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                if(CheckEmail != null)
                {
                ViewBag.EMErrors = "Email is already registered";
                return View("Index");  
                }                 
            };

//==========SAVE NEW USER====================//
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                User RegUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                HttpContext.Session.SetInt32("CurrentUser", RegUser.UserId);
            return RedirectToAction("Dashboard");
            }
            else
            { 
            return View("Index");
            }
        }
//=======================LOAD DASHBOARD========================================//
        [HttpGet]
        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            int? ValidUser = HttpContext.Session.GetInt32("CurrentUser");
            if(ValidUser != null)
            {
                ViewBag.Users = _context.Users.ToList();
            return View();
            }
            return RedirectToAction("Index");
        }
//==========================LOGIN===============================================//
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string UserName, string Email, string Password)
        {
            if(UserName != null && Email != null && Password != null)
            {
                User VerifyUser = _context.Users.SingleOrDefault(user => user.UserName == UserName && user.Email == Email && user.Password == Password);
                if(VerifyUser != null)
                {
                    HttpContext.Session.SetInt32("CurrentUser", VerifyUser.UserId);
                        return RedirectToAction("Dashboard");
                }
            }
            ViewBag.Errors = "Invalid Login Combination";
                      
            return View("Index");
        }
//==========================Logout===============================================//
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
//========================DELETE USER===========================================//
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete()
        {
            int? DeleteUserId = HttpContext.Session.GetInt32("CurrentUser");
            User DeleteUser = _context.Users.SingleOrDefault(user => user.UserId == DeleteUserId);
                _context.Users.Remove(DeleteUser);
                _context.SaveChanges();
                HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
//========================UPDATE USER PAGE======================================//
        [HttpGet]
        [Route("/UpdateAcct")]
        public IActionResult UpdateAcct()
        {
            int? ValidUser = HttpContext.Session.GetInt32("CurrentUser");
            if(ValidUser != null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
//========================UPDATE USER EMAIL, PASSWORD, & USERNAME===============//
        [HttpPost]
        [Route("/Update")]
        public IActionResult Update(UpdateViewModel model)
        {
            if(ModelState.IsValid){
            //update the User in the database
            int? UpdateUserId = HttpContext.Session.GetInt32("CurrentUser");
            User UpdateUser = _context.Users.SingleOrDefault(user => user.UserId == UpdateUserId);
            UpdateUser.UserName = model.UserName;
            UpdateUser.Email = model.Email;
            UpdateUser.Password = model.Password;
            UpdateUser.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}
