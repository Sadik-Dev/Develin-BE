using Backend_Develin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = Backend_Develin.Models.Task;

namespace Backend_Develin.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectContext _context;
        private readonly DbSet<Task> _tasks;


        public TaskRepository(ProjectContext dbContext)
        {
            _context = dbContext;
            _tasks = _context.Tasks;
        }
        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void Delete(Task task)
        {
            _tasks.Remove(task);
        }

        public IEnumerable<Task> GetAll()
        {
            return _tasks;
        }

        public Task GetBy(int id)
        {
           return  _tasks.Where(t => t.Id == id).Include(e =>e.Employee).FirstOrDefault();        
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Task task)
        {
            _context.Update(task);
        }
    }
}
