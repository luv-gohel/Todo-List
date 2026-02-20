using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.RequestDTO
{
    public class ListRequest
    {
        public int ID { get; set; }
        public Guid TaskGID { get; set; }
        public Guid UserGID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool isUpdated { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
