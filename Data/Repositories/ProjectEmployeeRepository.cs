using Backend_Develin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Data.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployeeRepository
    {
        private readonly ProjectContext _context;
        private readonly DbSet<ProjectEmployee> _projectEmployees;


        public ProjectEmployeeRepository(ProjectContext dbContext)
        {
            _context = dbContext;
            _projectEmployees = dbContext.ProjectEmployees;
        }

        public void Add(Models.ProjectEmployee pe)
        {
            _projectEmployees.Add(pe);
        }

        public void Delete(Models.ProjectEmployee pe)
        {
            _projectEmployees.Remove(pe);
        }

        public Models.ProjectEmployee GetBy(int eid, int pid)
        {
            return _projectEmployees.Where(pe => pe.EmployeeId == eid && pe.ProjectId == pid)
                .Include(p => p.Project).ThenInclude(e => e.Manager).FirstOrDefault();
            }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
