using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTaskList.Models
{
    public class ListDisplayInAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpdatedAt { get; set; }
        public int Tasks { get; set; }
    }
}
