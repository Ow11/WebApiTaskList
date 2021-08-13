using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTaskList.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApiTaskList.Models
{
    public class TaskMinified
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
