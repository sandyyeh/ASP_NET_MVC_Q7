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

        public ActionResult List(bool? status)
        {
            string json = "";
            var list = _db.TodoModel.OrderBy(o => o.Id).ToList();

            if (status == true)
            {
                list = _todoService.GetAll(status).ToList();
            }
            else if (status == false)
            {
                list = _todoService.GetAll(status).ToList();
            }
       

            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            json = JsonConvert.SerializeObject(list, Formatting.None, setting);

            return Json(json, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Create(TodoModel todoModel)
        {

            _todoService.Create(todoModel);
            string json = JsonConvert.SerializeObject(todoModel);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(int id)
        {
            _todoService.Update(id);
            return Json(new { success = true });

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _todoService.Delete(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Clear()
        {
            _todoService.Clear();
            return Json(new { success = true });
        }

        //可有可無，此為保險起見，釋放資源用途
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
