using System.Data;
using System.Linq;
using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Infrastructure.Repository;


namespace TodoMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities _db = new DatabaseEntities();
        TodoRepository todoRepository = new TodoRepository();

        // GET: Home
        public ActionResult Index()
        {
            var list = todoRepository.GetAll();

            return View(list);
        }

        [HttpPost]
        public ActionResult Index(TodoModel todoModel)
        {
            todoRepository.Create(todoModel);

            return RedirectToAction("Index");
        }




        public ActionResult Select(bool status, TodoModel todoModel, string routeValue)
        {

            ViewModel viewModel = new ViewModel();
            viewModel.ToDoModel.URL = RouteData.Values["Action"].ToString();

            if (routeValue == "Index" || todoModel.Content != null)
            {
                todoRepository.Create(todoModel);
                return RedirectToAction("Index");
            }

            todoRepository.Select(status, viewModel);
            return View("Index", viewModel);

        }


        public ActionResult Update(int id, bool firstStatus, string route)
        {

            todoRepository.Update(id, firstStatus);

            return RedirectToAction("Select", new { status = firstStatus, routeValue = route });

        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id, bool firstStatus, string route)
        {

            todoRepository.Delete(id);

            return RedirectToAction("Select", new { status = firstStatus, routeValue = route });
        }

        public ActionResult Clear()
        {
            todoRepository.Clear();

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}