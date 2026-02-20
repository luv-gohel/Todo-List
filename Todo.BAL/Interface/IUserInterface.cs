using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.RequestDTO;
using Todo.Models.ResponseDTO;

namespace Todo.BAL.Interface
{
    public interface IUserInterface
    {
        public ApiResponse Authentication(LoginRequest l1);
        public ApiResponse Authorization(RegisterRequest l1);
    }
}
