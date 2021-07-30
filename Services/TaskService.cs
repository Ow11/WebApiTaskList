using NMTask.Models;
using System.Collections.Generic;
using System.Linq;
using NMList.Services;

namespace NMTask.Services
{
    public static class TaskService
    {
        // TODO: add createdAt time
        static List<Task> Tasks { get; }
        static int nextId = 3;
        static TaskService()
        {
            Tasks = new List<Task>
            {
                new Task
                {
                    Id = 1,
                    Name = "The Godfather",
                    Description = "Watch this great movie again with my friend!",
                    CreatedAt = "2021-07-29T21:18:59.447Z"
                },
                new Task
                {
                    Id = 2,
                    Name = "12 Angry Men",
                    Description = "A reccomendation from my friend",
                    CreatedAt = "2021-07-29T21:18:59.447Z"
                },
            };
        }

        public static List<Task> GetAll(int offset, int count)
        {
            List<Task> result = new List<Task>();
            
            foreach(Task task in Tasks.Skip(offset).Take(count))
                result.Add(task);

            return result;
        }

        public static Task Get(int id) => Tasks.FirstOrDefault(t => t.Id == id);

        public static Task Add(int listId, Task task)
        {
            if (! ListService.AddTask(listId, nextId))
                return null;
            task.Id = nextId++;
            Tasks.Add(task);
            return task;
        }

        public static Task Delete(int id)
        {
            Task task = Get(id);
            if (task is null)
                return null;

            Tasks.Remove(task);
            return task;
        }

        public static Task Update(Task task)
        {
            int index = Tasks.FindIndex(t => t.Id == task.Id);
            if (index == -1)
                return null;

            Tasks[index] = task;
            return task;
        }
    }
}