using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.DTO;
using Todo.Models.RequestDTO;
using Todo.Models.ResponseDTO;

namespace Todo.DAL.Interface
{
    public interface IManageNoteInterface
    {
        public ApiResponse AddTask(ListRequest l1);
        public ApiResponse UpdateTask(ListRequest l1);
        public ApiResponse DeleteTask(ListRequest l1);
        public ApiResponse GetAllTask(requestUserNotes r1);

    }
}
