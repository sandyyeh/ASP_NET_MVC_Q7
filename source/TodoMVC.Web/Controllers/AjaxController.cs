using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Service;

namespace TodoMVC.Web.Controllers
{
    public class AjaxController : Controller
    {

        private DatabaseEntities1 _db = new DatabaseEntities1();
        private TodoService _todoService;


        public AjaxController()
        {
            _todoService = new TodoService();
        }


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(bool? status)
        {
            string json = "";
            var list = _db.TodoModel.OrderBy(o => o.Id).ToList();

            if (status == true)
            {
                list = _db.TodoModel.Where(o => o.Status == true).OrderBy(o => o.Id).ToList();
            }
            else if (status == false)
            {
                list = _db.TodoModel.Where(o => o.Status == false).OrderBy(o => o.Id).ToList();

            }


            json = JsonConvert.SerializeObject(list);
            return Json(json, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public JsonResult Create(TodoModel todoModel)
        {
            string json = "";
            _todoService.Create(todoModel);
            json = JsonConvert.SerializeObject(todoModel);

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Update(int id)
        {
            _todoService.Update(id);
            return Redirect("List");

        }

        public ActionResult Delete(int id)
        {
            _todoService.Delete(id);
            return Redirect("List");
        }

        public ActionResult Clear()
        {
            _todoService.Clear();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
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
