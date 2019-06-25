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
        private DatabaseEntities db = new DatabaseEntities();
        CreateClass createClass = new CreateClass();
        // GET: Home
        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.ToDoModels = db.TodoModel.ToList();
           viewModel.ToDoModel.URL = RouteData.Values["Action"].ToString();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(TodoModel todoModel)
        {
            createClass.CreateMethod(todoModel);
            return RedirectToAction("Index");
        }




        public ActionResult Select(bool status, TodoModel todoModel, string routeValue)
        {

            ViewModel viewModel = new ViewModel();
            viewModel.ToDoModel.URL = RouteData.Values["Action"].ToString();

            if (routeValue == "Index" || todoModel.Content != null)
            {
                return RedirectToAction("Index");
            }


            if (status)
            {
                var q = db.TodoModel.Where(o => o.Status).ToList();
                viewModel.ToDoModels = q;
                return View("Index", viewModel);
            }
            else
            {
                var q = db.TodoModel.Where(o => o.Status == false).ToList();
                viewModel.ToDoModels = q;
                return View("Index", viewModel);
            }


        }


        public ActionResult Update(int? id, string route)
        {

            var data = db.TodoModel.Find(id);

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
            return RedirectToAction("Select", new { status = !data.Status, routeValue = route });

        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id, string route)
        {

            TodoModel toDoModel = db.TodoModel.Find(id);
            var firstStatus = toDoModel.Status;
            db.TodoModel.Remove(toDoModel);
            db.SaveChanges();

            return RedirectToAction("Select", new { status = firstStatus, routeValue = route });
        }

        public ActionResult Clear(TodoModel todoModel)
        {
            var query = db.TodoModel.Where(o => o.Status);
            db.TodoModel.RemoveRange(query);
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
        public class CreateClass : Controller
        {
            private DatabaseEntities db = new DatabaseEntities();
            public TodoModel CreateMethod(TodoModel toDoModel)
            {
                if (ModelState.IsValid && toDoModel.Content != null)
                {
                    toDoModel.Status = false;
                    db.TodoModel.Add(toDoModel);
                    db.SaveChanges();
                    return toDoModel;
                }
                return toDoModel;
            }
        }

    }
}