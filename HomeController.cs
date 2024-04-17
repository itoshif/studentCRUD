using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DAL stud;
        public HomeController(ILogger<HomeController> logger,DAL student)
        {
            _logger = logger;
            stud = student;
        }
        [HttpPost]
        public IActionResult AddFees([FromBody] Fees studfee)
        {
            stud.addFees(studfee.StudentId,studfee.FeeStatus);
            return Json("Updated Fee Status.");
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] students studinfo)
        {
            stud.addStudent(studinfo.Id,studinfo.FirstName,studinfo.LastName,studinfo.DOB,studinfo.FatherName,studinfo.MotherName,studinfo.Address);
            return Json("Student added Succesfully.");
        }
        [HttpPost]
        public IActionResult AddMarks([FromBody] Marksmodel studmarks)
        {
            stud.addMarks(studmarks.StudentId, studmarks.Subject,studmarks.Marks);
            return Json("Marks added Successfully.");
        }
        [HttpGet]
        // function to return all students..
        public IActionResult GetMarks(int id)
        {
            IEnumerable<Getmarkmodel> marki=stud.getMarks(id);
            return View(marki);
        }

        public ActionResult GetMarksPartial(int id)
        {
            // Here, you would retrieve the marks data for the student with the given id
            // You can customize this logic based on how you retrieve the marks data
            IEnumerable<Getmarkmodel> marki = stud.getMarks(id);
            return PartialView("_Getmarkspartial",marki);
        }
        [HttpGet]
        public IActionResult GetFee(int id)
        {
            bool feestat=stud.getFees(id);
            return Json(feestat);
        }
        public IActionResult Index(string searchterm,int id)
        {
            IEnumerable<Getmarkmodel> marki = stud.getMarks(id);
            IEnumerable<students> studi = stud.getAll(searchterm);

            var viewmodel = new ViewModels
            {
                student = studi,
                mark=marki,
            };
            return View(viewmodel);
        }

        public IActionResult NewStud()
        {
            return View();
        }
        public IActionResult UpdateMarks()
        {
            IEnumerable<students> studi = stud.getAll(String.Empty);
            return View(studi);
        }
        public IActionResult UpdateFees()
        {
            IEnumerable<students> studi = stud.getAll(String.Empty);
            return View(studi);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
