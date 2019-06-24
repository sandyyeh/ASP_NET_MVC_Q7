using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoMVC.Web.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            ToDoModel = new TodoModel();
        }
        public TodoModel ToDoModel { get; set; }
        public List<TodoModel> ToDoModels { get; set; }
    }
}