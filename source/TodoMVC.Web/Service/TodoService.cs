using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoMVC.Web.Infrastructure.Interface;
using TodoMVC.Web.Infrastructure.Repository;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Service
{
    public class TodoService
    {

        private ITodoRepository _todoRepository;

        public TodoService()
        {
            _todoRepository = new TodoRepository();
        }

        public  IEnumerable<TodoModel> GetAll(bool? status)
        {            
            var list = _todoRepository.GetAll(status);
            return list;
        }
        public void Create(TodoModel toDoModel)
        {
            if (toDoModel.Content != null)
            {
                _todoRepository.Create(toDoModel);
            }

        }

        public void Update(int id)
        {
            _todoRepository.Update(id);
        }

        public void Delete(int id)
        {
            _todoRepository.Delete(id);
        }
        public void Clear()
        {
            _todoRepository.Clear();
        }
    }
}