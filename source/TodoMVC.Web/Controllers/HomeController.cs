using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Service;


namespace TodoMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities1 _db = new DatabaseEntities1();
        private TodoService _todoService;


        public HomeController()
        {
            _todoService = new TodoService();
        }

        // GET: Home
        public ActionResult Index(bool? status)
        {
            var list = _todoService.GetAll(status);
            ViewModel viewModel = new ViewModel
            {
                ToDoModels = list         
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TodoModel todoModel)
        {
            _todoService.Create(todoModel);

            return RedirectToAction("Index");
        }


        public ActionResult Update(int id)
        {

            _todoService.Update(id);

            return Redirect(Request.UrlReferrer.AbsoluteUri);

        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {

            _todoService.Delete(id);

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult Clear()
        {
            _todoService.Clear();

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