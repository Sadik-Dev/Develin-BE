using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend_Develin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Develin.Data.Repositories
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class ProjectEmployeesController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;

        public ProjectEmployeesController(IProjectRepository context, IEmployeeRepository employeeRepository,IProjectEmployeeRepository peRepo)
        {
            _projectRepository = context;
            _employeeRepository = employeeRepository;
            _projectEmployeeRepository = peRepo;

        }
        // GET: api/ProjectEmployees/id
        /// <summary>
        /// Get the Employee of a Project with the project 
        /// </summary>
        /// <param name="eid">the id of the Employee</param>
        /// <param name="pid">the id of the Project</param>
        /// <returns>The ProjectEmployee</returns>
        [HttpGet("{eid}/{pid}")]
        public ActionResult<ProjectEmployee> GetProjectEmployee(int eid, int pid)
        {
            ProjectEmployee projectE = _projectEmployeeRepository.GetBy(eid,pid);
            if (projectE == null)
                return NotFound();
            return projectE;

        }

        // POST: api/ProjectEmployees
        /// <summary>
        /// Adds an existing employee to an existing Project
        /// </summary>
        /// <param name="projectEmployee">the project with the employee object</param>
        [HttpPost]
        public ActionResult<ProjectEmployee> PostProjectEmployee(ProjectEmployee projectEmp)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
               
            Employee employee = _employeeRepository.GetBy(projectEmp.EmployeeId);
            Project project = _projectRepository.GetBy(projectEmp.ProjectId);

            
            if (project.Manager.Id != userId)
                return StatusCode(403);


            employee.voegGebruikerAanProject(project);

            _projectRepository.SaveChanges();

            Project p = _projectRepository.GetBy(projectEmp.ProjectId);


            return CreatedAtAction(nameof(GetProjectEmployee), new { eid = projectEmp.EmployeeId, pid = projectEmp.ProjectId  }, projectEmp);
        }

        // DELETE: api/ProjectEmployees/eid/pid
        /// <summary>
        /// Deletes a employee from a project
        /// </summary>
        /// <param name="eid">the id of the employee to be deleted from a project</param>
        /// <param name="pid">the id of the project where the employee has to be deleted from</param>

        [HttpDelete("{eid}/{pid}")]
        public IActionResult DeleteEmployee(int eid, int pid)
        {
            

            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            ProjectEmployee projectEmp = _projectEmployeeRepository.GetBy(eid,pid);

            if (projectEmp == null) { return NotFound(); }


            if (projectEmp.Project.Manager.Id != userId)
                return StatusCode(403);

            if (eid == projectEmp.Project.Manager.Id)
                return StatusCode(403);

            _projectEmployeeRepository.Delete(projectEmp);
            _projectEmployeeRepository.SaveChanges();
            return NoContent();
        }


    }
}