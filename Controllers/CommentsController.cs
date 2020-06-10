using System;
using System.Collections.Generic;
using System.Linq;
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

    public class CommentsController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CommentsController(ITaskRepository taskRepo,ICommentRepository commentRepo, IEmployeeRepository empRepo)
        {
            _taskRepository = taskRepo;
            _commentRepository = commentRepo;
            _employeeRepository = empRepo;
        }

        // GET: api/Comments/id
        /// <summary>
        /// Get the Comment with given id
        /// </summary>
        /// <param name="id">the id of the Comment</param>
        /// <returns>The Comment</returns>
        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            Comment comment = _commentRepository.GetBy(id);
            if (comment == null)
                return NotFound();
            return comment;

        }

        // POST: api/Comments
        /// <summary>
        /// Add a new Comment to an existing Task
        /// </summary>
        /// <param name="Comment">the new Task</param>
        /// <param name="id">the task where to put the new comment</param>
        [HttpPost]
        public ActionResult<Comment> PostComment(int id, Comment comment)
        {
            Task task = _taskRepository.GetBy(id);
            Employee employee = _employeeRepository.GetBy(comment.Author.Id);
            comment.Author = employee;
            task.addComment(comment);

            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }
    }
}