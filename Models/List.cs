using NMTask.Models;
using System.Collections.Generic;

namespace NMList.Models
{
    public class LisT
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpdatedAt { get; set; }
        public List<int> Tasks { get; set; }
    }
}