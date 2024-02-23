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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student= await DbContext.Students.FindAsync(id);
            return  View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student =await DbContext.Students.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;    
                student.Phone = viewModel.Phone;
                student.Subscibed= viewModel.Subscibed;
                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await DbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == viewModel.Id);

            if(student is not null ) 
            {
                DbContext.Students.Remove(viewModel);
                await DbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }
      


        


    }
}
