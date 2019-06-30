using System.Linq;
using TodoMVC.Web.Infrastructure.Interface;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {

        public ViewModel GetAll(string status, ViewModel viewModel)
        {
            using (var db = new Database1Entities())
            {
                
                if (status == "Active")
                {
                    var q = db.TodoModel.Where(o => o.Status == false).ToList();
                    viewModel.ToDoModels = q;
                    return viewModel;
                }
                else if (status == "Completed")
                {
                    var q = db.TodoModel.Where(o => o.Status).ToList();
                    viewModel.ToDoModels = q;
                    return viewModel;
                }
                else
                {
                    viewModel.ToDoModels = db.TodoModel.ToList();
                    return viewModel;
                }


            }
        }

        public void Create(TodoModel toDoModel)
        {
            using (var db = new Database1Entities())
            {
                if (toDoModel.Content != null)
                {
                    toDoModel.Status = false;
                    db.TodoModel.Add(toDoModel);
                    db.SaveChanges();

                }

            }

        }
      
        public void Update(int id)
        {
            using (var db = new Database1Entities())
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
            }
        }
        public void Delete(int id)
        {
            using (var db = new Database1Entities())
            {
                TodoModel toDoModel = db.TodoModel.Find(id);
                db.TodoModel.Remove(toDoModel);
                db.SaveChanges();

            }
        }
        public void Clear()
        {
            using (var db = new Database1Entities())
            {
                var q = db.TodoModel.Where(o => o.Status);
                db.TodoModel.RemoveRange(q);
                db.SaveChanges();
            }
        }
    }
}