using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public interface ITaskRepository
    {
        Task GetBy(int id);

        IEnumerable<Task> GetAll();
        void Add(Task task);
        void Delete(Task task);
        void Update(Task task);
        void SaveChanges();
    }
}
