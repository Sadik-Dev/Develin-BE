using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public class Task
    {
        public int Id { get; set; }
        public String Titel { get; set; }
        public List<Comment> Comments { get; set; }
        public Employee Employee { get; set; }
        public State State { get; set; }
        public DateTime dateOfFinish { get; set; }
        public int SpentTime { get; set; }

        public Task(String titel, Employee employee)
        {
            Titel = titel;
            Employee = employee;
            State = State.Started;
            dateOfFinish = DateTime.Now;
            Comments = new List<Comment>();
        }

        public Task()
        {
            dateOfFinish = DateTime.Now;
            Comments = new List<Comment>();
        }

        public void addComment(Comment comment)
        {
            Comments.Add(comment);
        }





    }
}
