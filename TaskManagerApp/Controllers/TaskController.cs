using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    public class TaskController : Controller
    {
        // creating private read field to store AppDbContext implementations locally (in this file)
        private readonly ApplicationDbContext _db;

        // constructor to pull all implementations from container
        public TaskController(ApplicationDbContext db)
        {
            _db = db;       // storing it all locally
        }

        public IActionResult Index()
        {
            // creating var to retrieve our Db_set, convert it to a list and store it all in variable which we pass to our view 
            // var objTaskList = _db.TaskMans.ToList();

            // we want this to be strongly typed so we'll use IEnumerable (loading using statement from models.)
            IEnumerable<TaskMan> objTaskList = _db.TaskMans;
            return View(objTaskList);
        }

        // GET - this will create a task, so we will be getting information from the form
        public IActionResult Create()
        {
            return View();
        }

        // POST - the create view will pass a task object that we need to add to the table using this method 
        [HttpPost]      // always add httpPost when posting data
        [ValidateAntiForgeryToken]      // this will prevent cross site forgery on form
        public IActionResult Create(TaskMan obj)
        {
            if (obj.DueDate.Date.ToString() == obj.DateAdded.Date.ToString())
            {
                ModelState.AddModelError("DueDate", "Due date can not be the same as current date (task must be assigned 24hrs in advance)");
            }
            if (ModelState.IsValid)     // validating whether model is valid
            {
                _db.TaskMans.Add(obj);      // this will add the new form to the table 
                _db.SaveChanges();          // this will push it to the database
                return RedirectToAction("Index");       // redirect to the list page
            }
            return View(obj);
        }
    }
}
