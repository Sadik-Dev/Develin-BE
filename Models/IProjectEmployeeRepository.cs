using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Models
{
    public interface  IProjectEmployeeRepository
    {
        ProjectEmployee GetBy(int eid, int pid);

        void Add(ProjectEmployee pe);
        void Delete(ProjectEmployee pe);
        void SaveChanges();

    }
}
