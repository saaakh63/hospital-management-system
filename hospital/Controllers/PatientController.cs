using Azure.Core;
using hospital;
using hospital.connect;
using hospital.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PatientController(HospitalContext con, IWebHostEnvironment webHostEnvironment)
        {
            this.context = con;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult PrePatientIndex()
        {

            return View();
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var acc = context.Accounts.SingleOrDefault(account => account.Email == email);
            if (acc is not null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, acc.Password))
                {

                    HttpContext.Session.SetString("UserId", acc.Email.ToString());
                    HttpContext.Session.SetString("UserType", "Patient");

                    return RedirectToAction("PatientIndex", "PatientIndex");
                }
            }

            // Authentication failed, return to login page
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            ViewData["Message"] = "";
            return View(new { Message = "" });
        }

   

        [HttpPost]
        public IActionResult Signup(string email, string password, string name, string conpass, string gen, string phone )
        {
            //  gen = Request.Form["gender"];
            var found = context.Accounts.SingleOrDefault(account => account.Email == email) is not null;
            if (ModelState.IsValid)
             {

                if (!found)
                {
                    if (conpass == password)
                    {


                        context.Accounts.Add(new PatientAccount
                        {

                            Email = email,
                            Password = BCrypt.Net.BCrypt.HashPassword(password),
                            Name = name,
                            Gender = gen,
                            PhoneNumber = phone,
                            

                        });
                     
                        HttpContext.Session.SetString("UserId", email);
                        HttpContext.Session.SetString("UserType", "Patient");
                        context.SaveChanges();
                        return RedirectToAction("PatientIndex", "PatientIndex");

                    }

                }
              
            }
            ViewData["Message"] = "Error...";
            return View();
            
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}

