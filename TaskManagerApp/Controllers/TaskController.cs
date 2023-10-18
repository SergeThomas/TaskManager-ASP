using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                TempData["success"] = "Task Created succesfully";
                return RedirectToAction("Index");       // redirect to the list page
            }
            return View(obj);
        }


        // GET - this will get the task and display it using the task number since it is the primary key
        public IActionResult Edit(int? taskNumber)
        {
            // return task if taskNumber found
            if (taskNumber==null || taskNumber == 0)
            {
                return NotFound();
            }
            var taskFromDb = _db.TaskMans.Find(taskNumber);     // it will find the task based of it's taskNumber and assign to the variable
            //var taskFromDb = _db.TaskMans.FirstOrDefault(u => u.TaskNumber == taskNumber);

            if (taskFromDb == null)
            {
                return NotFound();
            }

            return View(taskFromDb);    // if task is found, then return to view
        }

        // POST - the edit view will pass a task object that we need to update to the table using this method 
        [HttpPost]      // always add httpPost when posting data
        [ValidateAntiForgeryToken]      // this will prevent cross site forgery on form
        public IActionResult Edit(TaskMan obj)
        {
            if (obj.DueDate.Date.ToString() == obj.DateAdded.Date.ToString())
            {
                ModelState.AddModelError("DueDate", "Due date can not be the same as current date (task must be assigned 24hrs in advance)");
            }

            if (ModelState.IsValid)     // validating whether model is valid
            {
                _db.TaskMans.Update(obj);      // method to update record based on primary key
                _db.SaveChanges();          // this will push it to the database
                TempData["success"] = "Task updated succesfully";
                return RedirectToAction("Index");       // redirect to the list page
            }
            return View(obj);
        }


        // this is the delete action method
        // GET - this will get the task and display it using the task number since it is the primary key
        public IActionResult Delete(int? taskNumber)
        {
            // return task if taskNumber found
            if (taskNumber == null || taskNumber == 0)
            {
                return NotFound();
            }
            var taskFromDb = _db.TaskMans.Find(taskNumber);     // it will find the task based of it's taskNumber and assign to the variable
            //var taskFromDb = _db.TaskMans.FirstOrDefault(u => u.TaskNumber == taskNumber);

            if (taskFromDb == null)
            {
                return NotFound();
            }

            return View(taskFromDb);    // if task is found, then return to view
        }

        // POST - this will delete the obj from the database
        [HttpPost, ActionName("Delete")]   // always add httpPost when posting data
        [ValidateAntiForgeryToken]      // this will prevent cross site forgery on form
        public IActionResult DeletePOST(int? taskNumber)
        {
            // return task if taskNumber found
            if (taskNumber == null || taskNumber == 0)
            {
                return NotFound();
            }
            var obj = _db.TaskMans.Find(taskNumber);     // it will find the task based of it's taskNumber and assign to the variable
            //var taskFromDb = _db.TaskMans.FirstOrDefault(u => u.TaskNumber == taskNumber);


            if (ModelState.IsValid)     // validating whether model is valid
            {
                _db.TaskMans.Remove(obj);      // method to update record based on primary key
                _db.SaveChanges();          // this will push it to the database
                TempData["success"] = "Task deleted succesfully";
                return RedirectToAction("Index");       // redirect to the list page
            }
            return View(obj);
        }

        [HttpGet, ActionName("Complete")]
        public IActionResult MarkAsComplete(int? taskNumber)
        {
            // return task if taskNumber found
            if (taskNumber == null || taskNumber == 0)
            {
                return NotFound();
            }
            var taskFromDb = _db.TaskMans.Find(taskNumber);     // it will find the task based of it's taskNumber and assign to the variable
            //var taskFromDb = _db.TaskMans.FirstOrDefault(u => u.TaskNumber == taskNumber);

            if (taskFromDb == null)
            {
                return NotFound();
            }

            return View(taskFromDb);    // if task is found, then return to view
        }

        // POST - Marking As complete
        [HttpPost, ActionName("Complete")]      // always add httpPost when posting data
        [ValidateAntiForgeryToken]      // this will prevent cross site forgery on form
        public IActionResult MarkTaskComplete(int? taskNumber)
        {
            if(taskNumber == null || taskNumber == 0)
            {
                return NotFound();
            }

            var obj = _db.TaskMans.Find(taskNumber);

            if (ModelState.IsValid)     // validating whether model is valid
            {
                if (obj.CompletionStatus == false)
                {
                    obj.CompletionStatus = true;
                    _db.TaskMans.Update(obj);      // method to update record based on primary key
                    _db.SaveChanges();          // this will push it to the database
                    return RedirectToAction("Index");       // redirect to the list page
                }
            }
            return View(obj);           
        }
    }
}
