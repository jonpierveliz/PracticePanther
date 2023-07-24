using PracticePanther.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Services
{
    public class TimeService
    {
        private List<Time> timeEntries;

        // List of time entries
        public List<Time> TimeEntries
        {
            get
            {
                return timeEntries;
            }
        }

        private static TimeService? instance;

        // Singleton instance of ProjectService
        public static TimeService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimeService();
                }

                return instance;
            }
        }

        private TimeService()
        {
            // Initializes the list of time entries
            timeEntries = new List<Time>()
            {
                new Time { Id = 1, Name = "Time Entry #1", Date = DateTime.Now, Hours = 1.5m, ProjectId = 1, EmployeeId = 1 },
                new Time { Id = 2, Name = "Time Entry #2", Date = DateTime.Now.AddDays(-1), Hours = 2.0m, ProjectId = 1, EmployeeId = 1 },
                new Time { Id = 3, Name = "Time Entry #3", Date = DateTime.Now.AddDays(-2), Hours = 0.5m, ProjectId = 1, EmployeeId = 1 },
                new Time { Id = 4, Name = "Time Entry #4", Date = DateTime.Now.AddDays(-3), Hours = 3.0m, ProjectId = 1, EmployeeId = 1 },
                new Time { Id = 5, Name = "Time Entry #5", Date = DateTime.Now.AddDays(-4), Hours = 1.0m, ProjectId = 1, EmployeeId = 1 }
            };
        }

        // Returns a time entry by id 
        public Time? Get(int id)
        {
            return TimeEntries.FirstOrDefault(t => t.Id == id);
        }

        // Deletes a time entry by id
        public void Delete(int id)
        {
            var timeEntryToRemove = TimeEntries.FirstOrDefault(x => x.Id == id);
            if (timeEntryToRemove != null)
            {
                TimeEntries.Remove(timeEntryToRemove);
            }
        }

        // Adds a time entry
        public void AddOrUpdate(Time t)
        {
            var isAdd = false;
            if (t.Id == 0)
            {
                isAdd = true;
                // If the id is 0, it's a new employee, so assign a new id
                t.Id = LastId + 1;
            }
            if (isAdd)
            {
                // Add the employee to the list
                TimeEntries.Add(t);
            }
        }

        // Gets the last id in the list of time entries
        private int LastId
        {
            get
            {
                // If the list is not empty, return the maximum id, otherwise return 0
                return TimeEntries.Any() ? TimeEntries.Select(t => t.Id).Max() : 0;
            }
        }

        // Searches for time entries matching the provided query
        public IEnumerable<Time> Search(string query)
        {
            return TimeEntries
                .Where(p => p.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }
    }
}
