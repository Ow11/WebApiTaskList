using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApiTaskList.Models
{
    public class ListMinified
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
