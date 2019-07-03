using System.Collections.Generic;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Service
{
    public interface ITodoService
    {
        void Create(TodoModel todoModel);
        void Update(int id);
        void Delete(int id);
        void Clear();
        IEnumerable<TodoModel> GetAll(bool? status);

    }
}