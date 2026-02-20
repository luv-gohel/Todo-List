using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.DTO;

namespace Todo.Models.ResponseDTO
{
    public class ApiResponse
    {
        public string message { get; set; } 
        public int statuscode { get; set; }
        public Guid Userid { get; set; }
        public List<Lists>? Responselist { get; set; }
        public List<User>? ResponseUser { get; set; }
    }
}
