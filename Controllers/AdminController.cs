using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Models;

namespace school.Controllers
{
    public class AdminController : Controller
    {

        public Mydbcontext Context { get; }
        public IWebHostEnvironment Web { get; }

        public AdminController(Mydbcontext context,IWebHostEnvironment web )
        {
            Context = context;
            Web = web;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add_teacher()
        {

            return View();
        }
        public IActionResult _teach()
        {
            var data = Context.teachers.ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Add_teacher(Teacher tech)
        {
            Context.teachers.Add(tech);
            Context.SaveChanges();
            return RedirectToAction("Teacher_table");
        }

        public IActionResult Teacher_table()
        {
          var data =  Context.teachers.ToList();
            ViewData["teach"] = Context.teachers;
            return View(data);
        }

        public IActionResult Teacher_Delte(int id)
        {
            var data = Context.teachers.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Teacher_CDelte(int id)
        {
            var data = Context.teachers.Find(id);
            Context.teachers.Remove(data);
            return RedirectToAction("Teacher_table");
        }

        public IActionResult Edit_Teacher(int id)
        {
            var data = Context.teachers.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit_Teacher(int id, Teacher teach)
        {
            var data = Context.teachers.Find(id);

            data.FullName = teach.FullName;
            data.Email = teach.Email;
            data.Address = teach.Address;
            data.ContactNumber = teach.ContactNumber;
            data.Password = teach.Password;
            Context.SaveChanges();
            return RedirectToAction("Teacher_table");
        }

        public IActionResult Add_Student()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add_Student(Student stud,IFormFile ProfilePicture)
        {
            var location = Path.Combine(Web.WebRootPath, "Images", ProfilePicture.FileName);
            FileStream file = new FileStream(location, FileMode.Create);
            ProfilePicture.CopyTo(file);
            stud.ProfilePicture = ProfilePicture.FileName;
            Context.students.Add(stud);
            Context.SaveChanges();
   
            return RedirectToAction("Student_table");
        }

        public IActionResult Student_table()
        {
            var data = Context.students.ToList();
            return View(data);
        }

        public IActionResult std_Delete(int id)
        {
            var data = Context.students.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult std_cdelete(int id)
        {
            var data = Context.students.Find(id);
            var old_img = Path.Combine(Web.WebRootPath, "Images", data.ProfilePicture);
            if (System.IO.File.Exists(old_img))
            {
                System.IO.File.Delete(old_img);
            }

            Context.students.Remove(data);
            Context.SaveChanges();
            return RedirectToAction("Student_table");
        }
        public IActionResult std_Edit(int id)
        {
            var data = Context.students.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult std_Edit(Student stud,IFormFile ProfilePicture)
        {
            var data = Context.students.Where(a => a.Id == stud.Id).FirstOrDefault();
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                var old_img = Path.Combine(Web.WebRootPath, "Images", data.ProfilePicture);
                if (System.IO.File.Exists(old_img))
                {
                    System.IO.File.Delete(old_img);
                }
                var location = Path.Combine(Web.WebRootPath, "Images", ProfilePicture.FileName);
                FileStream file = new FileStream(location, FileMode.Create);
                ProfilePicture.CopyTo(file);
                data.ProfilePicture = ProfilePicture.FileName;
            }
            else
            {
                stud.ProfilePicture = data.ProfilePicture;
            }
            data.FullName = stud.FullName;
            data.Email = stud.Email;
            data.Password = stud.Password;
            data.Address = stud.Address;
            data.ContactNumber = stud.ContactNumber;
            Context.SaveChanges();
            return RedirectToAction("Student_table");
        }

        public IActionResult attendece()
        {

            return View();
        }

        [HttpPost]
   
        public IActionResult attendence()
        {
            var data = Context.teachers.ToList();
            return View(data);
        }

        public IActionResult admin_panel()
        {
            var data = Context.teachers.ToList();
            return View(data);
        }

        public IActionResult admin_edit(int id)
        {
            var data = Context.teachers.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult admin_edit(int id, Teacher teach)
        {
            var data = Context.teachers.Find(id);

            data.FullName = teach.FullName;
            data.Email = teach.Email;
            data.Password = teach.Password;
            Context.SaveChanges();
            return RedirectToAction("Teacher_table");
        }

    }
}
