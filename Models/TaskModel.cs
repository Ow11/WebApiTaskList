using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTaskList.Models
{
    [Table("Task")]
    public class TaskModel
    {
        [Key]
        [Required]
        public int TaskModelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }

        public int ListModelId { get; set; }
        [ForeignKey("ListModelForeignKey")]
        public ListModel ListModel { get; set;  }
    }
}
