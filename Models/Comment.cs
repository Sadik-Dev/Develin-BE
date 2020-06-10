using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public Employee Author { get; set; }
        public DateTime Date { get; set; }

        public Comment(String text, Employee author)
        {
            Text = text;
            Author = author;
            Date = DateTime.Now;
        }

        public Comment()
        {

        }
    }
}
