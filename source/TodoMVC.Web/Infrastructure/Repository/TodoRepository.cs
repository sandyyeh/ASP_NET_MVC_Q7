﻿using System.Collections.Generic;
using System.Linq;
using TodoMVC.Web.Infrastructure.Interface;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {

        public IEnumerable<TodoModel> GetAll(bool? status)
        {
            using (var db = new DatabaseEntities1())
            {
         
                if (status == false)
                {
                    var q = db.TodoModel.Where(o => o.Status == false).ToList();            
                    return q;
                }
                else if (status==true)
                {
                    var q = db.TodoModel.Where(o => o.Status).ToList();              
                    return q;
                }
                else
                {
                    var q = db.TodoModel.ToList();
                    return q;
                }
            }
        }

        public void Create(TodoModel toDoModel)
        {
            using (var db = new DatabaseEntities1())
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
            using (var db = new DatabaseEntities1())
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
            using (var db = new DatabaseEntities1())
            {
                TodoModel toDoModel = db.TodoModel.Find(id);
                db.TodoModel.Remove(toDoModel);
                db.SaveChanges();

            }
        }
        public void Clear()
        {
            using (var db = new DatabaseEntities1())
            {
                var q = db.TodoModel.Where(o => o.Status);
                db.TodoModel.RemoveRange(q);
                db.SaveChanges();
            }
        }
    }
}