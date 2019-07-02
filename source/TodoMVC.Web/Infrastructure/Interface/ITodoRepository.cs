using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Infrastructure.Interface
{
    public interface ITodoRepository
    {
        void Create(TodoModel todoModel);
        void Update(int id);
        void Delete(int id);
        void Clear();
        IEnumerable<TodoModel> GetAll(bool? status);



    }
}
