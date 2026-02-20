using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Interface;
using Todo.Models.RequestDTO;
using Todo.Models.ResponseDTO;

namespace Todo.DAL.Repository
{
    public class UserRepository : IAuthentication
    {
        private readonly string _connectionstring;
        public UserRepository(DbConnectionOptions options)
        {
            _connectionstring = options.ConnectionString;
        }
        public AuthResult Authentication(LoginRequest l1)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    using (var command = new SqlCommand("Authentication", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", l1.Email);
                        command.Parameters.AddWithValue("@Password", l1.Password);
                        SqlParameter outputparam2 = new SqlParameter("@status", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputparam2);
                        SqlParameter outputparam = new SqlParameter("@userGuid", SqlDbType.UniqueIdentifier)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputparam);
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (outputparam2.Value != DBNull.Value && (int)outputparam2.Value != 100)
                        {
                            authResult.Userid = (Guid)outputparam.Value;
                            authResult.statusCode = (int)outputparam2.Value;
                        }
                        else
                        {
                            authResult.Userid = Guid.Empty;
                            authResult.statusCode = 100;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Function Authentication", e.Message);
            }
            return authResult;
        }

        public AuthResult Authorization(RegisterRequest l1)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    using (var command = new SqlCommand("Sp_Authorization", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", l1.FirstName);
                        command.Parameters.AddWithValue("@LastName", l1.LastName);
                        command.Parameters.AddWithValue("@PhoneNo", l1.PhoneNo);
                        command.Parameters.AddWithValue("@Gender", l1.Gender);
                        command.Parameters.AddWithValue("@Email", l1.Email);
                        command.Parameters.AddWithValue("@Password", l1.Password);
                        SqlParameter outputparam = new SqlParameter("@status", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputparam);
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (outputparam.Value != DBNull.Value)
                        {
                            authResult.statusCode = (int)outputparam.Value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Function Authorization", e.Message);
            }
            return authResult;
        }
    }
}
