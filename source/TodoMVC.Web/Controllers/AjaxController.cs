using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Service;

namespace TodoMVC.Web.Controllers
{
    public class AjaxController : Controller
    {

        private Database1Entities _db = new Database1Entities();
        private TodoService _todoService;


        public AjaxController()
        {
            _todoService = new TodoService();
        }


        public ActionResult Index()
        {
            return View();
        }
        // GET: Home
        public JsonResult List(bool? status, ViewModel viewModel)
        {


            string json = "";
            

            if (status == true)
            {
                var listTrue = _db.TodoModel.Where(o => o.Status == true).OrderBy(o => o.Id).ToList();
                if (listTrue.Count > 0)
                {
                    json = JsonConvert.SerializeObject(listTrue);
                }

            }
            else if (status == false)
            {
                var listFalse = _db.TodoModel.Where(o => o.Status == false).OrderBy(o => o.Id).ToList();
                json = JsonConvert.SerializeObject(listFalse);
            }
            else
            {
                var list = _db.TodoModel.OrderBy(o => o.Id).ToList();
                json = JsonConvert.SerializeObject(list);
            }







            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string content)
        {
            TodoModel todoModel = new TodoModel();
            string json = "";
            if (content != null)
            {
                todoModel.Content = content;
                todoModel.Status = false;
                _db.TodoModel.Add(todoModel);
                _db.SaveChanges();
            }
            json = JsonConvert.SerializeObject(todoModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Update(int id)
        {

            _todoService.Update(id);

            return Redirect(Request.UrlReferrer.AbsoluteUri);

        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {


            //_todoService.Delete(id);
            TodoModel toDoModel = _db.TodoModel.Find(id);
            _db.TodoModel.Remove(toDoModel);
            _db.SaveChanges();

            return Redirect(Request.UrlReferrer.AbsoluteUri);


        }

        public ActionResult Clear()
        {
            _todoService.Clear();

            return RedirectToAction("Ajax","Index");
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
