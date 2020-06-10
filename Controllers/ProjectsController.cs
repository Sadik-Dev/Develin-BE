using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend_Develin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Backend_Develin.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectsController(IProjectRepository context, IEmployeeRepository employeeRepository)
        {
            _projectRepository = context;
            _employeeRepository = employeeRepository;

        }
       
        // GET: api/projects
        /// <summary>
        /// Get all projects filtered by name if parameter is given
        /// </summary>
        /// <returns>array of projects</returns>
        [HttpGet]
        public IEnumerable<Project> GetProjects(string name ="")
        {
            IEnumerable<Project> projects;

            if (name.Equals(""))
                projects = _projectRepository.GetAll().OrderBy(r => r.Name);

            projects = _projectRepository.GetAllFiltered(name);


            //Authorizing the user
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return   projects.Where(p => p.Employees.Select(e => e.EmployeeId).Contains(userId)).ToList(); 



        }
        // GET: api/Projects/id
        /// <summary>
        /// Get the project with given id
        /// </summary>
        /// <param name="id">the id of the project</param>
        /// <returns>The project</returns>
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            Project project = _projectRepository.GetBy(id);
            if (project == null)
                return NotFound();
            return project;

        }


        // POST: api/Projects
        /// <summary>
        /// Adds a new project
        /// </summary>
        /// <param name="project">the new project</param>
        [Authorize(Policy = "Manager")]
        [HttpPost]

        public ActionResult<Project> PostProject(Project project)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Project newProject = new Project();
            Manager manager = (Manager) _employeeRepository.GetBy(userId);
            List<Employee> employees = new List<Employee>();

            project.Employees.ForEach(e =>
            {
                Employee em = _employeeRepository.GetBy(e.EmployeeId);
                employees.Add(em);
            });

            newProject.Name = project.Name;
            newProject.addManagerToproject(manager);
            employees.ToList().ForEach(e => e.voegGebruikerAanProject(newProject));


            _projectRepository.Add(newProject);
            _projectRepository.SaveChanges();
            return CreatedAtAction(nameof(GetProject), new { id = newProject.Id }, newProject);
        }


        // DELETE: api/Projects/id
        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="id">the id of the project to be deleted</param>
        [Authorize(Policy = "Manager")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id) {
            Project project = _projectRepository.GetBy(id);
            if (project == null) { return NotFound(); }

            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (project.Manager.Id != userId)
                return StatusCode(403);

            _projectRepository.Delete(project);
            _projectRepository.SaveChanges(); 
            return NoContent();
        }









    }
}