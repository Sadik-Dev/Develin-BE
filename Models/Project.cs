using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }
        public Manager Manager { get; set; }
        public List<ProjectEmployee> Employees { get; set; }

        public DateTime Deadline { get; set; }

        public List<Task> Tasks { get; set; }

        public Project()
        {
            Created = DateTime.Now;
            Tasks = new List<Task>();
            Employees = new List<ProjectEmployee>();
        }
        public Project(string name, Manager manager, DateTime deadline) : this()
        {
            Name = name;
            Manager = manager;
            manager.Projects.Add(new ProjectEmployee(this, manager));
            Deadline = deadline;
        }

        public Project(string name, Manager manager) : this()
        {
            Name = name;
            Manager = manager;
            manager.Projects.Add(new ProjectEmployee(this, manager));
        }

        public void addEmployeeToProject(ProjectEmployee pe)
        {
            


            Employees.Add(pe);
        }

        public void addManagerToproject(Manager manager)
        {
            Manager = manager;
            manager.Projects.Add(new ProjectEmployee(this,manager));
        }

        public void addTaskToProject(Task task, Employee employee)
        {
            Tasks.Add(task);
            employee.AddTask(task);

        }
    }
}
