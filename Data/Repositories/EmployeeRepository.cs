using Backend_Develin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend_Develin.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ProjectContext _context;
        private readonly DbSet<Employee> _employees;


        public EmployeeRepository(ProjectContext dbContext)
        {
            _context = dbContext;
            _employees = _context.Employees;
        }

        public Employee GetBy(int id)
        {
            return _employees.Include(e => e.Projects).SingleOrDefault(p => p.Id == id);
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employees.Include(e => e.Projects).ToList();
        }

        public bool isEmployeeManager(string email)
        {
            Employee employee = _employees.Where(e => e.Email.Equals(email)).FirstOrDefault();

            return employee.GetType().Name.Equals("Manager")
                ? true : false;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }
    }
}
