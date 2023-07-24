using PracticePanther.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticePanther.Library.Services
{
    public class EmployeeService
    {
        // List of employees
        public List<Employee> Employees
        {
            get
            {
                return employees;
            }
        }
        private List<Employee> employees;
        private static EmployeeService? instance;

        // Singleton instance of EmployeeService
        public static EmployeeService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmployeeService();
                }
                return instance;
            }
        }

        // Returns an employee by id
        public Employee? Get(int id)
        {
            return Employees.FirstOrDefault(e => e.Id == id);
        }

        private EmployeeService()
        {
            // Initializes the list of employees with some default data
            employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Employee 1", Rate = 10.0m },
                new Employee { Id = 2, Name = "Employee 2", Rate = 12.0m },
                new Employee { Id = 3, Name = "Employee 3", Rate = 15.0m },
                new Employee { Id = 4, Name = "Employee 4", Rate = 18.0m },
                new Employee { Id = 5, Name = "Employee 5", Rate = 20.0m },
            };
        }

        // Deletes an employee by id
        public void Delete(int id)
        {
            var clientToRemove = Employees.FirstOrDefault(x => x.Id == id);
            if (clientToRemove != null)
            {
                Employees.Remove(clientToRemove);
            }
        }

        // Gets the last id in the list of employees
        private int LastId
        {
            get
            {
                // If the list is not empty, return the maximum id, otherwise return 0
                return Employees.Any() ? Employees.Select(c => c.Id).Max() : 0;
            }
        }

        // Adds or updates a client
        public void AddOrUpdate(Employee e)
        {
            var isAdd = false;
            if (e.Id == 0)
            {
                isAdd = true;
                // If the id is 0, it's a new employee, so assign a new id
                e.Id = LastId + 1;
            }
            if (isAdd)
            {
                // Add the employee to the list
                Employees.Add(e);
            }
        }


        // Searches for clients matching the provided query
        public IEnumerable<Employee> Search(string query)
        {
            return Employees
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }
    }
}
