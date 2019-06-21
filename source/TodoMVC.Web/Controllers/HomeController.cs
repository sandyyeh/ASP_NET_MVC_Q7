using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TodoMVC.Web.Models;


namespace TodoMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private ToDoModelContext db = new ToDoModelContext();

        // GET: Home
        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.ToDoModels = db.ToDoModels.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ToDoModel toDoModel)
        {
            //create
            if (ModelState.IsValid && toDoModel.Content != null)
            {
                db.ToDoModels.Add(toDoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        public ActionResult Select(string status,ToDoModel toDoModel)
        {
            ViewModel viewModel = new ViewModel();


            if (ModelState.IsValid && toDoModel.Content != null)
            {
                toDoModel.Status = false;
                db.ToDoModels.Add(toDoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (status == "true")
            {
                var q = (from o in db.ToDoModels
                         where o.Status == true
                         select o).ToList();
                viewModel.ToDoModels = q;

                return View(viewModel);

            }
            if (status == "false")
            {
                var q = (from o in db.ToDoModels
                         where o.Status == false
                         select o).ToList();
                viewModel.ToDoModels = q;
                return View(viewModel);
            }

            return RedirectToAction("Index");


        }

        public ActionResult aa()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(int? id)
        {
            ToDoModel toDoModel = new ToDoModel();
            var data = db.ToDoModels.Find(id);
            //if (data.Status == false)
            //{
            //    toDoModel.Status = true;
            //    db.SaveChanges();
            //}
            //else
            //{
            //    toDoModel.Status =false;
            //    db.SaveChanges();
            //}

            //return RedirectToAction("Index");

            return View();
        }


        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {

            ToDoModel toDoModel = db.ToDoModels.Find(id);
            db.ToDoModels.Remove(toDoModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Clear(ToDoModel toDoModel)
        {
            if (ModelState.IsValid && toDoModel.Content != null)
            {
                db.ToDoModels.Add(toDoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var query = db.ToDoModels.Where(o => o.Status == true);
            db.ToDoModels.RemoveRange(query);
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
}
