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
            ToDoModel = new ToDoModel();
        }
        public ToDoModel ToDoModel { get; set; }
        public List<ToDoModel> ToDoModels { get; set; }
    }
}