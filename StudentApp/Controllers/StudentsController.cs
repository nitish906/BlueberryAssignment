using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Models;
using StudentApp.Models.Entites;

namespace StudentApp.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController(StudentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public StudentDbContext DbContext { get; }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task <IActionResult> Add(AddStudentModel addStudentModel) 
        {
            var student = new Student
            {
                Name = addStudentModel.Name,
                Email = addStudentModel.Email,
                Phone = addStudentModel.Phone,
                Subscibed = addStudentModel.Subscibed,
            };

            await DbContext.Students.AddAsync(student);

            await DbContext.SaveChangesAsync(); 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students= await DbContext.Students.ToListAsync();

            return View(students);
        }

    }
}
