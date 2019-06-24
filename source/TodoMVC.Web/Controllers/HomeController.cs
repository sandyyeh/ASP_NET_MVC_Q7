using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private TodoModelContext db = new TodoModelContext();
        CreateClass createClass = new CreateClass();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.route = RouteData.Values["Action"];
            ViewModel viewModel = new ViewModel();
            viewModel.ToDoModels = db.TodoModels.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TodoModel todoModel)
        {

            createClass.CreateMethod(todoModel);
            return RedirectToAction("Index");
        }



        public ActionResult Select(bool status, TodoModel todoModel,string a)
        {
        
            ViewModel viewModel = new ViewModel();

            createClass.CreateMethod(todoModel);

            if (a=="Index")
            {
                return RedirectToAction("Index");
            }
            if (todoModel.Content != null)
            {
                return RedirectToAction("Index");
            }

            if (status)
            {
                var q = db.TodoModels.Where(o => o.Status).ToList();
                viewModel.ToDoModels = q;
                return View("Index", viewModel);

            }
            if (status == false)
            {
                var q = db.TodoModels.Where(o => o.Status == false).ToList();
                viewModel.ToDoModels = q;
                return View("Index", viewModel);
            }
            return RedirectToAction("Index");

        }



        public ActionResult Update(int? id,string route)
        {
            TodoModel todoModel = new TodoModel();
            var data = db.TodoModels.Find(id);
    


            if (data.Status == false)
            {
                data.Status = true;
                db.SaveChanges();
              
            }
            else
            {
                data.Status = false;
                db.SaveChanges();            

            }
            //var a = Request.UrlReferrer;
            return RedirectToAction("Select", new { status = !data.Status,a=route });



        }


        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {

            TodoModel toDoModel = db.TodoModels.Find(id);
            var firstStatus = toDoModel.Status;
            db.TodoModels.Remove(toDoModel);
            db.SaveChanges();

            return RedirectToAction("Select", new { status = firstStatus });
        }


        public ActionResult Clear(TodoModel todoModel)
        {
            var query = db.TodoModels.Where(o => o.Status);
            db.TodoModels.RemoveRange(query);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
    public class CreateClass : Controller
    {
        private TodoModelContext db = new TodoModelContext();
        public TodoModel CreateMethod(TodoModel toDoModel)
        {
            if (ModelState.IsValid && toDoModel.Content != null)
            {
                toDoModel.Status = false;
                db.TodoModels.Add(toDoModel);
                db.SaveChanges();
                return toDoModel;
            }
            return toDoModel;
        }
    }
}
