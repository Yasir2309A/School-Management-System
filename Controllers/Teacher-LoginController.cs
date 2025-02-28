using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    public class Teacher_LoginController : Controller
    {
        private readonly Mydbcontext AuthContexts;




        public IWebHostEnvironment Env { get; }

        public Teacher_LoginController(Mydbcontext AuthContext, IWebHostEnvironment env)
        {
            this.AuthContexts = AuthContext;
            Env = env;
        }
        public IActionResult TeacherRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TeacherRegister(Teacher Registered)
        {
            AuthContexts.Add(Registered);
            AuthContexts.SaveChanges();
            return RedirectToAction("Register");
        }
        public IActionResult ViewTeacher()
        {
            var users = AuthContexts.teachers.ToList();
            return View(users);
        }
        public IActionResult TeacherDelete(int id)
        {
            var data = AuthContexts.teachers.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult TeacherDeleteConfrim(int id)
        {
            var data = AuthContexts.teachers.Find(id);
            AuthContexts.teachers.Remove(data);
            AuthContexts.SaveChanges();
            return RedirectToAction("ViewTeacher");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Teacher reg)
        {
            var data = AuthContexts.teachers.Where(Z => Z.Email == reg.Email && Z.Password == reg.Password).FirstOrDefault();
            if (data != null)
            {
                HttpContext.Session.SetString("mysessionteacher", data.FullName);
                return RedirectToAction("MyAccount");
            }
   
            return View();
        }

        public IActionResult MyAccount()
        {
            if (HttpContext.Session.GetString("mysessionteacher") != null)
            {
                ViewData["UserNamed"] = HttpContext.Session.GetString("mysessionteacher");
                return View();
            }
            return RedirectToAction("Login");
        }
        //public IActionResult Dashboard()
        //{
        //    if (HttpContext.Session.GetString("mysession") != null)
        //    {
        //        ViewData["UserNamed"] = HttpContext.Session.GetString("mysession");
        //        return View();
        //    }
        //    return RedirectToAction("Login");
        //}

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("mysessionteacher") != null)
            {
                HttpContext.Session.Remove("mysessionteacher");
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}
