using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public class ProjectEmployee
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ProjectEmployee()
        {
        }

        public ProjectEmployee(Project project, Employee employee)
        {
            Project = project;
            Employee = employee;
            ProjectId = project.Id;
            EmployeeId = employee.Id;
        }
    }
}
