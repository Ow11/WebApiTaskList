using NMList.Models;
using System.Collections.Generic;
using System.Linq;
using NMTask.Services;
// using NMTask.Models;

namespace NMList.Services
{
    public static class ListService
    {
        // TODO: Add DateTime
        static List<LisT> Lists { get; }
        static int nextId = 2;
        static ListService()
        {
            Lists = new List<LisT>
            {
                new LisT
                {
                    Id = 1,
                    Name = "Movies",
                    Description = "Movies to watch",
                    UpdatedAt = null,
                    Tasks = new List<int>
                    {
                        1, 2
                    }
                }
            };
        }

        public static List<LisT> GetAll() => Lists;

        public static LisT GetLisT(int id) => Lists.FirstOrDefault(t => t.Id == id);

        public static ListDetailed Get(int id)
        {
            LisT list = Lists.FirstOrDefault(t => t.Id == id);

            if (list is null)
                return null;

            ListDetailed result = new ListDetailed
            {
                Id = list.Id,
                Name = list.Name,
                Description = list.Description,
                UpdatedAt = list.UpdatedAt,
            };
            result.Tasks = list.Tasks.ConvertAll(x => TaskService.Get(x));
            return result;
        }

        public static LisT Add(LisT list)
        {
            list.Id = nextId++;
            Lists.Add(list);
            return list;
        }

        public static LisT Delete(int id)
        {
            LisT list = GetLisT(id);
            if (list is null)
                return null;

            foreach (int taskId in list.Tasks)
                TaskService.Delete(taskId);

            Lists.Remove(list);
            return list;
        }

        public static LisT Update(LisT list)
        {
            int index = Lists.FindIndex(t => t.Id == list.Id);
            if (index == -1)
                return null;

            Lists[index] = list;
            return list;
        }

        public static LisT Merge(int id, int mergeId)
        {
            int index = Lists.FindIndex(t => t.Id == id);
            LisT merge = GetLisT(mergeId);
            if (index == -1 || merge is null)
                return null;

            foreach(int task in merge.Tasks)
                Lists[index].Tasks.Add(task);

            Delete(mergeId);

            return Lists[index];
        }
    }
}