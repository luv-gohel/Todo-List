using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.ResponseDTO
{
    public  class AuthResult
    {
        public int statusCode { get;set; }
        public Guid Userid{ get;set; }
    }
}
