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

        // Get - this will create a task, so we will be getting information from the form
        public IActionResult Create()
        {
            return View();
        }
    }
}
