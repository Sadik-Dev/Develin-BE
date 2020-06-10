using Backend_Develin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Develin.Data
{
    public class DataInitializer
    {
        private readonly ProjectContext _dbContext;


        public DataInitializer(ProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LoadDB()
        {
               _dbContext.Database.EnsureDeleted();
               if (_dbContext.Database.EnsureCreated())
               {
             

                Manager manager1 = new Manager("Pieter Cappelle","Man","pieter_cappele@staringjane.be",Convert.ToBase64String(Encoding.UTF8.GetBytes("piet48LZ")));
                Manager manager2 = new Manager("Oussama Sadik", "Man", "dsk0@live.fr", Convert.ToBase64String(Encoding.UTF8.GetBytes("9947sadik")));

                Employee employee1 = new Employee("Jean Dede", "Man","Jeanbaptiste@staringjane.be", Convert.ToBase64String(Encoding.UTF8.GetBytes("js96464a")));
                Employee employee2 = new Employee("Lisa Marzouki", "Vrouw","marzoukilisa@staringjane.be", Convert.ToBase64String(Encoding.UTF8.GetBytes("misamzmz")));
                Employee employee3 = new Employee("Ismail BenAhmed", "Man", "isma9600@staringjane.be", Convert.ToBase64String(Encoding.UTF8.GetBytes("isma59")));



                Project angular = new Project("Shoe&Co WebShop", manager1, new DateTime(2020, 10, 10));
                Project java15 = new Project("Java 15",manager1, new DateTime(2021, 12, 10));


                Models.Task task1 = new Models.Task("Create project", employee1);
                task1.dateOfFinish = new DateTime(2020, 1, 5);

                Models.Task task2 = new Models.Task("Markt Onderzoek",employee1);
                task2.dateOfFinish = new DateTime(2020, 2, 5);

                Models.Task task3 = new Models.Task("Kosten berekenen", employee1);
                task3.dateOfFinish = new DateTime(2020, 3, 5);

                Models.Task task4 = new Models.Task("Bestelling Foto Materiaal", employee1);
                task4.dateOfFinish = new DateTime(2029, 4, 5);

                Models.Task task5 = new Models.Task("Analyse Design", employee1);
                task5.dateOfFinish = new DateTime(202, 5, 5);

                Models.Task task6 = new Models.Task("Analyse Use Cases", employee1);
                task6.dateOfFinish = new DateTime(2019, 6, 5);

                Models.Task task7 = new Models.Task("Ontwerpen", employee2);
                task7.dateOfFinish = new DateTime(2019, 7, 5);

                Models.Task task8 = new Models.Task("Implementatie Design", employee2);
                task8.dateOfFinish = new DateTime(2019, 8, 5);

                Models.Task task9 = new Models.Task("Implementatie Back End", employee2);
                task9.dateOfFinish = new DateTime(2019, 9, 5);

                Models.Task task10 = new Models.Task("Implementatie Paypal", employee2);
                task10.dateOfFinish = new DateTime(2019, 10, 5);

                Models.Task task11 = new Models.Task("Implementatie Cart", employee2);
                task11.dateOfFinish = new DateTime(2019, 11, 5);


                Models.Task task12 = new Models.Task("Live zetten", employee2);
                task12.dateOfFinish = new DateTime(2019, 12, 5);

                employee1.voegGebruikerAanProject(angular);
                employee2.voegGebruikerAanProject(angular);

                angular.addTaskToProject(task1,employee1);
                angular.addTaskToProject(task2, employee1);
                angular.addTaskToProject(task3, employee1);
                angular.addTaskToProject(task4, employee1);
                angular.addTaskToProject(task5, employee1);
                angular.addTaskToProject(task6, employee1);

                angular.addTaskToProject(task7, employee2);
                angular.addTaskToProject(task8, employee2);
                angular.addTaskToProject(task9, employee2);
                angular.addTaskToProject(task10, employee2);
                angular.addTaskToProject(task11, employee2);
                angular.addTaskToProject(task12, employee2);

                Random random = new Random();

                angular.Tasks.ForEach(t => {
                    t.State = State.Done;
                    t.SpentTime = random.Next(8);
                });



                _dbContext.Projects.AddRange(angular, java15);
                _dbContext.Employees.AddRange(manager1, employee1, employee2,manager2,employee3);
                _dbContext.SaveChanges();
        }
        


        }

    
    }
}
