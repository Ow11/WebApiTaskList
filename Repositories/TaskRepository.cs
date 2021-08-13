using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskList.Data;
using WebApiTaskList.Models;

namespace WebApiTaskList.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetTasks(int offset, int count);
        TaskModel GetTaskByID(int taskId);
        void InsertTask(TaskModel task);
        void DeleteTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void SaveTask();
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly ApiDbContext _context;

        public TaskRepository(ApiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskModel> GetTasks(int offset, int count)
        {
            return _context.Tasks.AsNoTracking().Skip(offset).Take(count).ToList();
        }

        public TaskModel GetTaskByID(int taskId)
        {
            return _context.Tasks.Find(taskId);
        }

        public void InsertTask(TaskModel task)
        {
            _context.Tasks.Add(task);
        }

        public void DeleteTask(TaskModel task)
        {
            _context.Tasks.Remove(task);
        }

        public void UpdateTask(TaskModel task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }

        public void SaveTask()
        {
            _context.SaveChanges();
        }
    }
}
