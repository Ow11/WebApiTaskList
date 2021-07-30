using NMTask.Models;
using System.Collections.Generic;

namespace NMList.Models
{
    public class ListDetailed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpdatedAt { get; set; }
        public List<Task> Tasks { get; set; }
    }
}