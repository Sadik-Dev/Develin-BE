using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Gender { get; set; }
        public List<Task> Tasks { get; set; }
        public List<ProjectEmployee> Projects { get; set; }
        [JsonIgnore]
        public string Password { get;  set; }
        public string Email { get;  set; }
        public string Token { get; set; }
        public Boolean IsManager { get; set; }

   
        public Employee(String name, String gender, String email, String password)
        {
            Name = name;
            Gender = gender;
            Email = email;
            Password = password;

            if (this.GetType().Name.Equals("Manager"))
                IsManager = true;
            else
                IsManager = false;

            Tasks = new List<Task>();
            Projects = new List<ProjectEmployee>();
        }

        public Employee()
        {
            Tasks = new List<Task>();
            Projects = new List<ProjectEmployee>();
        }


        public void AddTask(Task task)
        {
            Tasks.Add(task);
            task.Employee = this;
        }

        public void voegGebruikerAanProject(Project project)
        {
            ProjectEmployee pe = new ProjectEmployee(project, this);
            project.addEmployeeToProject(pe);
            Projects.Add(pe);
        }
    }
}
