using Microsoft.AspNetCore.Mvc;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    public class StudentRegisterController : Controller
    {
        private readonly Mydbcontext AuthContexts;




        public IWebHostEnvironment Env { get; }

        public StudentRegisterController(Mydbcontext AuthContext, IWebHostEnvironment env)
        {
            this.AuthContexts = AuthContext;
            Env = env;
        }
        public IActionResult StudentRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentRegister(Student Registered, IFormFile ProfilePicture)
        {
            string file_Location = Path.Combine(Env.WebRootPath, "I", ProfilePicture.FileName);
            FileStream fs = new FileStream(file_Location, FileMode.Create);


            ProfilePicture.CopyTo(fs);
            Registered.ProfilePicture = ProfilePicture.FileName;
            AuthContexts.students.Add(Registered);
            AuthContexts.SaveChanges();
            return RedirectToAction("StudentLogin");
        }
        public IActionResult ViewStudent()
        {
            var users = AuthContexts.students.ToList();
            return View(users);
        }
        public IActionResult StudentDelete(int id)
        {
            var data = AuthContexts.students.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult StudentDeleteConfrim(int id)
        {
            var data = AuthContexts.students.Find(id);
            AuthContexts.students.Remove(data);
            AuthContexts.SaveChanges();
            return RedirectToAction("ViewStudent");
        }
        public IActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentLogin(Teacher reg)
        {
            var data = AuthContexts.students.Where(Z => Z.Email == reg.Email && Z.Password == reg.Password).FirstOrDefault();
            if (data != null)
            {
                HttpContext.Session.SetString("mysession", data.FullName);
                return RedirectToAction("MyAccount");
            }

            return View();
        }

        public IActionResult MyAccount()
        {
            if (HttpContext.Session.GetString("mysession") != null)
            {
                ViewData["UserNamed"] = HttpContext.Session.GetString("mysession");
                return View();
            }
            return RedirectToAction("StudentLogin");
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
        public IActionResult Edit(int id)
        {
            var data = AuthContexts.students.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Student student, IFormFile ProfilePicture)
        {
            var data = AuthContexts.students.Where(Z => Z.Id == student.Id).FirstOrDefault();
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                string file_Location = Path.Combine(Env.WebRootPath, "I", ProfilePicture.FileName);
                FileStream fs = new FileStream(file_Location, FileMode.Create);
                ProfilePicture.CopyTo(fs);

                data.ProfilePicture = ProfilePicture.FileName;
            }
            else
            {
               student.ProfilePicture = data.ProfilePicture;
           }
            data.FullName = student.FullName;
            data.Email = student.Email;
            data.ContactNumber = student.ContactNumber;
            data.Address = student.Address;
            data.Password = student.Password;

            AuthContexts.SaveChanges();
            return RedirectToAction("ViewCategory");
        }


        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("mysession") != null)
            {
                HttpContext.Session.Remove("mysession");
                return RedirectToAction("StudentLogin");
            }
            return View();
        }
    }
}
