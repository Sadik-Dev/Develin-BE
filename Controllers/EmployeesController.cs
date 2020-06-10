using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend_Develin.Data;
using Backend_Develin.DTOs;
using Backend_Develin.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend_Develin.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private IUserService _userService;


        public EmployeesController(IEmployeeRepository employeeRepository,
            IUserService userService)
        {
            _employeeRepository = employeeRepository;
            _userService = userService;           
        }

        // GET: api/employees
        /// <summary>
        /// Get all employees ordered by name
        /// </summary>
        /// <returns>array of employees</returns>
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll().OrderBy(r => r.Name);
        }
        // GET: api/Employees/id
        /// <summary>
        /// Get the employee with given id
        /// </summary>
        /// <param name="id">the id of the employee</param>
        /// <returns>The Employee</returns>
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            Employee employee;
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (id == -1) employee = _employeeRepository.GetBy(userId);
            else
                employee = _employeeRepository.GetBy(id);


            if (employee == null)
                return NotFound();

            bool isManager = _employeeRepository.isEmployeeManager(employee.Email);

            return employee;

        }

        // POST: api/Employees/authenticate
        /// <summary>
        /// Login
        /// </summary>
        /// <returns>The logged in employee</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginDTO model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });
    
            return Ok(user);
        }

        // POST: api/Employees
        /// <summary>
        /// Make a new Employee
        /// </summary>
        [Authorize(Policy = "Manager")]
        [HttpPost]
        public IActionResult PostEmployee(Employee employee)
        {

            _employeeRepository.Add(employee);
            _employeeRepository.SaveChanges();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);

        }





        }

        
    }