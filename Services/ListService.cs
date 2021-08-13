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
    public interface IListService
    {
        public List<ListModel> GetAll(int offset, int count);
        public ListModel Get(int id);
        public ListModel Add(ListModel list);
        public ListModel Delete(int id);
        public ListModel Update(ListModel list);
        public ListModel Merge(int id, int mergeId);
    }

    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;
        public ListService(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository;
            _taskRepository = taskRepository;
        }

        public List<ListModel> GetAll(int offset, int count)
        {
            var result = (List<ListModel>)_listRepository.GetLists(offset, count);
            return result;
        }

        public ListModel Get(int id) => _listRepository.GetListByID(id);

        public ListModel Add(ListModel list)
        {
            list.UpdatedAt = Utils.DateTimeNow();
            list.TaskModels = new List<TaskModel>();
            _listRepository.InsertList(list);
            _listRepository.SaveList();
            return list;
        }

        public ListModel Delete(int id)
        {
            ListModel list = Get(id);
            if (list is null)
                return null;

            _listRepository.DeleteList(list);
            _listRepository.SaveList();
            return list;
        }

        public ListModel Update(ListModel list)
        {
            ListModel existingList = _listRepository.GetListByID(list.ListModelId);

            existingList.Name = list.Name;
            existingList.Description = list.Description;
            existingList.UpdatedAt = Utils.DateTimeNow();
            existingList.TaskModels = list.TaskModels;

            _listRepository.SaveList();
            return list;
        }

        public ListModel Merge(int id, int mergeId)
        {
            ListModel list = _listRepository.GetListByID(id);
            ListModel listToMerge = _listRepository.GetListByID(mergeId);

            foreach (var task in listToMerge.TaskModels)
                list.TaskModels.Add(task);

            _listRepository.DeleteList(listToMerge);
            _listRepository.SaveList();
            return list;
        }
    }
}
