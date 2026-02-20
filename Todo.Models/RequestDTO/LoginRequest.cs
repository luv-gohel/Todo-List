using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.RequestDTO
{
    public class LoginRequest
    {
        public Guid GID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
