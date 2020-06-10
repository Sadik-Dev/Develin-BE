using Backend_Develin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend_Develin.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _context;
        private readonly DbSet<Project> _projects;


        public ProjectRepository(ProjectContext dbContext)
        {
            _context = dbContext;
            _projects = dbContext.Projects;
        }

        public void Add(Project project)
        {
            _projects.Add(project);
        }

        public void Delete(Project project)
        {
            _projects.Remove(project);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projects.Include(p => p.Manager).Include(p => p.Employees).ThenInclude(p => p.Employee).Include(p => p.Tasks).ThenInclude(l => l.Employee).Include(l => l.Tasks)
                .ThenInclude(l => l.Comments).ToList();    
        }
        public IEnumerable<Project> GetAllFiltered(string name)
        {
            return _projects.Include(p => p.Manager).Include(p => p.Employees).Include(p => p.Tasks).ThenInclude(l => l.Employee).Include(l => l.Tasks)
                .ThenInclude(l => l.Comments).Where(p => p.Name.Contains(name)).ToList();
        }


        public Project GetBy(int id)
        {
            Project p = _projects.Where(p => p.Id == id).Include(p => p.Manager).Include(p => p.Employees).ThenInclude(p => p.Employee).Include(p => p.Tasks).ThenInclude(l => l.Employee)
                .Include(l => l.Tasks).ThenInclude(l => l.Comments).First();
            return p;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            _context.Update(project);
        }
    }
}
