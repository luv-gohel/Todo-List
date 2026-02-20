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
    public interface IAuthentication
    {
        public AuthResult Authentication(LoginRequest l1);
        public AuthResult Authorization(RegisterRequest l1);
    }
}
