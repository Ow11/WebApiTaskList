using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTaskList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTaskList.Data;
using WebApiTaskList.Repositories;
using WebApiTaskList.Services;

namespace WebApiTaskList.Services
{
    public interface ITaskService
    {
        public List<TaskModel> GetAll(int offset, int count);
        public TaskModel Get(int id);
        public TaskModel Add(int listId, TaskModel task);
        public TaskModel Delete(int id);
        public TaskModel Update(TaskModel task);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IListRepository _listRepository;
        public TaskService(ITaskRepository taskRepository, IListRepository listRepository)
        {
            _taskRepository = taskRepository;
            _listRepository = listRepository;
        }

        public List<TaskModel> GetAll(int offset, int count)
        {
            var result = (List<TaskModel>)_taskRepository.GetTasks(offset, count);
            return result;
        }

        public TaskModel Get(int id) => _taskRepository.GetTaskByID(id);

        public TaskModel Add(int listId, TaskModel task)
        {
            var list = _listRepository.GetListByID(listId);

            if (list is null)
                return null;
            
            task.ListModelId = listId;
            task.ListModel = list;

            task.CreatedAt = Utils.DateTimeNow();

            _taskRepository.InsertTask(task);
            _taskRepository.SaveTask();

            // if (list.TaskModels is null)
            //     list.TaskModels = new List<TaskModel>();
            list.TaskModels.Add(task);
            _listRepository.SaveList();

            return task;
        }

        public TaskModel Delete(int id)
        {
            TaskModel task = Get(id);
            if (task is null)
                return null;

            _taskRepository.DeleteTask(task);
            _taskRepository.SaveTask();
            return task;
        }

        public TaskModel Update(TaskModel task)
        {
            TaskModel existingTask = _taskRepository.GetTaskByID(task.TaskModelId);

            existingTask.Name = task.Name;
            existingTask.Description = task.Description;

            _taskRepository.SaveTask();
            return task;
        }

    }
}
