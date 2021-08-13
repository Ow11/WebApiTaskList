using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiTaskList.Models;

namespace WebApiTaskList.Models
{
    [Table("List")]
    public class ListModel
    {
        [Key]
        [Required]
        public int ListModelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpdatedAt { get; set; }
        
        // [InverseProperty("ListModel")]
        public ICollection<TaskModel> TaskModels { get; set; }


        public ListModel()
        {
            TaskModels = new List<TaskModel>();
        }
    }
}
