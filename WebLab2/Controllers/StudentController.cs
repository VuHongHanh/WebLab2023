using BTH1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BTH1.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private List<Student> listStudents;
        private readonly IHostingEnvironment env;
        public StudentController(IHostingEnvironment _env) //tao ds
        {
            env = _env;
            listStudents = new List<Student>()
            {
                new Student()
                {
                    Id = 101,
                    Name = "Hải Nam",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.IT,
                    Gender = Gender.Male,
                    IsRegular = true,
                    Address = "Hà Nội",
                    Email = "hainam@gmail.com"
                },
                new Student()
                {
                    Id = 102,
                    Name = "Minh Tú",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.CE,
                    Gender = Gender.Female,
                    IsRegular = true,
                    Address = "Thanh Hóa",
                    Email = "mtu@gmail.com"
                },
                new Student()
                {
                    Id = 103,
                    Name = "Hoàng Long",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.EE,
                    Gender = Gender.Male,
                    IsRegular = false,
                    Address = "Hải Phòng",
                    Email = "hlong@gmail.com"
                },
                new Student() {
                    Id = 105,
                    Name = "Lâm Hùng",
                    DateOfBorth = DateTime.Now,
                    Branch = Branch.BE,
                    Gender = Gender.Male,
                    IsRegular = false,
                    Address = "Nghệ An",
                    Email = "hung@gmail.com"
                },
            };

        }
        [Route("List")]
        public IActionResult Index()
        {
            //Trả về View Index.cshtml cùng Model là danh sách sv listStudents
            return View(listStudents);
        }
		[HttpGet]
        [Route("Add")]		

        public IActionResult Create()
		{
			//lấy danh sách các giá trị Gender để hiển thị radio button trên form
			ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
			//lấy danh sách các giá trị Branch để hiển thị select-option trên form
			//Để hiển thị select-option trên View cần dùng List<SelectListItem>
			ViewBag.AllBranches = new List<SelectListItem>()
            {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
            };
			return View();
		}
	    [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Create(Student student)
        {
            if (student.Img != null)
            {
                var file = Path.Combine(env.ContentRootPath, "wwwroot\\Image", student.Img.FileName);
                using (FileStream fileStream = new FileStream(file, FileMode.Create))
                {
                    await student.Img.CopyToAsync(fileStream);
                }
            }
            student.Id = listStudents.Last<Student>().Id + 1;
            listStudents.Add(student);
            return View("Index", listStudents);
        }

    }
}
