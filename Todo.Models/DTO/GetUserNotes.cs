using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.DTO
{
    public class requestUserNotes
    {
        public Guid? UserGID { get; set; }
        public string? Search { get; set; } 
    }
}
