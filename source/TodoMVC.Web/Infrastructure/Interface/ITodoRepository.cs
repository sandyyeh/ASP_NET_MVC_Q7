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

        ViewModel Select(bool status, ViewModel viewModel);
        void Update(int id, bool status);

        void Delete(int id);
        void Clear();
       ViewModel GetAll();


        
    }
}
