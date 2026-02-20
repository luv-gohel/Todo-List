using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.BAL.Interface;
using Todo.DAL.Interface;
using Todo.Models.RequestDTO;
using Todo.Models.ResponseDTO;

namespace Todo.BAL.Repository
{
    public class IUserRepository : IUserInterface
    {
        public readonly IAuthentication _authentication;
        public IUserRepository(IAuthentication userRepository)
        {
            _authentication = userRepository;
        }
        public ApiResponse Authentication(LoginRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if (string.IsNullOrEmpty(l1.Email) || string.IsNullOrEmpty(l1.Password))
                {
                    apiResponse.statuscode = 100;
                    apiResponse.message = "Email and Password cannot be empty.";
                    return apiResponse;
                }
                var dbresult = _authentication.Authentication(l1);
                if (dbresult.statusCode == 100)
                {
                    apiResponse.statuscode = 100;
                    apiResponse.message = "Invalid email or password.";
                }
                else
                {
                    apiResponse.Userid = dbresult.Userid;
                    apiResponse.statuscode = dbresult.statusCode;
                    apiResponse.message = "Authentication successful.";
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return apiResponse;
        }

        public ApiResponse Authorization(RegisterRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if(string.IsNullOrEmpty(l1.Email) || string.IsNullOrEmpty(l1.Password) || string.IsNullOrEmpty(l1.FirstName) || string.IsNullOrEmpty(l1.LastName) || string.IsNullOrEmpty(l1.PhoneNo) || string.IsNullOrEmpty(l1.Gender))
                {
                    apiResponse.statuscode = 100;
                    apiResponse.message = "All Fields Are Required" ;
                    return apiResponse;
                }
                var dbresult = _authentication.Authorization(l1);
                if (dbresult.statusCode==101)
                {
                    apiResponse.statuscode = 101;
                    apiResponse.message = $"Register Successfully {l1.FirstName} {l1.LastName}";
                    return apiResponse;
                }
                else if(dbresult.statusCode == 102)
                {
                    apiResponse.statuscode = 102;
                    apiResponse.message = "Email Already Exists.";
                    return apiResponse;
                }
                else
                {
                    apiResponse.statuscode = 100;
                    apiResponse.message = "Registration Failed.";
                    return apiResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return apiResponse; 
        }
    }
}
