using System.Collections.Generic;
using System.Linq;

using TodoMVC.Web.Infrastructure.Interface;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {

        public ViewModel GetAll()
        {
            using (var db = new DatabaseEntities())
            {
                ViewModel viewModel = new ViewModel();
                viewModel.ToDoModels = db.TodoModel.ToList();
                viewModel.ToDoModel.URL = "Index";
                return viewModel;
            }
        }

        public void Create(TodoModel toDoModel)
        {
            using (var db = new DatabaseEntities())
            {
                if (toDoModel.Content != null)
                {
                    toDoModel.Status = false;
                    db.TodoModel.Add(toDoModel);
                    db.SaveChanges();

                }

            }

        }
        public ViewModel Select(bool status, ViewModel viewModel)
        {
            using (var db = new DatabaseEntities())
            {
                if (status)
                {
                    var q = db.TodoModel.Where(o => o.Status).ToList();
                    viewModel.ToDoModels = q;
                    return viewModel;
                }
                else
                {
                    var q = db.TodoModel.Where(o => o.Status == false).ToList();
                    viewModel.ToDoModels = q;
                    return viewModel;
                }
            }
        }
        public void Update(int id, bool status)
        {
            using (var db = new DatabaseEntities())
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
            using (var db = new DatabaseEntities())
            {
                TodoModel toDoModel = db.TodoModel.Find(id);
                db.TodoModel.Remove(toDoModel);
                db.SaveChanges();

            }
        }
        public void Clear()
        {
            using (var db = new DatabaseEntities())
            {
                var query = db.TodoModel.Where(o => o.Status);
                db.TodoModel.RemoveRange(query);
                db.SaveChanges();
            }
        }
    }
}