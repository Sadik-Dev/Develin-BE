using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend_Develin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = Backend_Develin.Models.Task;

namespace Backend_Develin.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TasksController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;

        public TasksController(IProjectRepository context, IEmployeeRepository employeeRepository, ITaskRepository taskRepo)
        {
            _projectRepository = context;
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepo;

        }

        // GET: api/Tasks/id
        /// <summary>
        /// Get the Task with given id
        /// </summary>
        /// <param name="id">the id of the Task</param>
        /// <returns>The Task</returns>
        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Task task = _taskRepository.GetBy(id);
            if (task == null)
                return NotFound();
            return task;

        }

        // POST: api/Tasks
        /// <summary>
        /// Add a new Task to an existing Project
        /// </summary>
        /// <param name="task">the new Task</param>
        /// <param name="id">the project where to put the new project</param>
        [HttpPost]
        public ActionResult<Task> PostTask(int id,Task task)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Project project = _projectRepository.GetBy(id);

            if (userId != project.Manager.Id)
                return StatusCode(403);

            Employee employee = _employeeRepository.GetBy(task.Employee.Id);

            project.addTaskToProject(task, employee);

            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/id
        /// <summary>
        /// Deletes a task
        /// </summary>
        /// <param name="id">the id of the task to be deleted</param>
        [Authorize(Policy = "Manager")]
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {            
            Task task = _taskRepository.GetBy(id);

            if (task == null) { return NotFound(); }

            _taskRepository.Delete(task);
            _taskRepository.SaveChanges();
            return NoContent();
        }
        // PUT: api/Tasks/id
        /// <summary>
        /// Modifies a Task
        /// </summary>
        /// <param name="id">id of the task to be modified</param>
        [HttpPut("{id}")]
        public IActionResult PutTask(int id, Task task)
        {
            if (id != task.Id) { return BadRequest(); }

            Task updatedTask = _taskRepository.GetBy(task.Id);
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (updatedTask.Employee.Id != userId)
                return StatusCode(403);

            updatedTask.State = task.State;
            updatedTask.SpentTime = task.SpentTime;
            updatedTask.dateOfFinish = DateTime.Now;

           
            _taskRepository.Update(updatedTask);
            _taskRepository.SaveChanges();
            return NoContent();
        }
    }
}