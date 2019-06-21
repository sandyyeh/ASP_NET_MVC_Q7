using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoMVC.Web.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}