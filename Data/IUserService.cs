using Backend_Develin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Data
{
    public interface IUserService
    {
        Employee Authenticate(string username, string password);
        IEnumerable<Employee> GetAll();
    }
}
