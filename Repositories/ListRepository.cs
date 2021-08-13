using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskList.Data;
using WebApiTaskList.Models;

namespace WebApiTaskList.Repositories
{
    public interface IListRepository
    {
        IEnumerable<ListModel> GetLists(int offset, int count);
        ListModel GetListByID(int listId);
        void InsertList(ListModel list);
        void DeleteList(ListModel list);
        void UpdateList(ListModel list);
        void SaveList();
    }
    public class ListRepository : IListRepository
    {
        private readonly ApiDbContext _context;

        public ListRepository(ApiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ListModel> GetLists(int offset, int count)
        {
            return _context.Lists.Include(l => l.TaskModels).AsNoTracking().Skip(offset).Take(count).ToList();
        }

        public ListModel GetListByID(int listId)
        {
            return _context.Lists.Include(l => l.TaskModels).FirstOrDefault(k => k.ListModelId == listId);
        }

        public void InsertList(ListModel list)
        {
            _context.Lists.Add(list);
        }

        public void DeleteList(ListModel list)
        {
            _context.Lists.Remove(list);
        }

        public void UpdateList(ListModel list)
        {
            _context.Entry(list).State = EntityState.Modified;
        }

        public void SaveList()
        {
            _context.SaveChanges();
        }
    }
}
