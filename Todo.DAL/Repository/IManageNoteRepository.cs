using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Interface;
using Todo.Models.DTO;
using Todo.Models.RequestDTO;
using Todo.Models.ResponseDTO;

namespace Todo.DAL.Repository
{
    public class IManageNoteRepository : IManageNoteInterface
    {
        public readonly string _connectionstring;
        public IManageNoteRepository(DbConnectionOptions options)
        {
            _connectionstring = options.ConnectionString;
        }
        public ApiResponse AddTask(ListRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
               using(var connection = new SqlConnection(_connectionstring))
               {
                    connection.Open();
                    using (var command = new SqlCommand("Sp_ManageNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userid", l1.UserGID);
                        command.Parameters.AddWithValue("@taskid", Guid.Empty);
                        command.Parameters.AddWithValue("@tasktitle", l1.TaskTitle);
                        command.Parameters.AddWithValue("@taskdescription", l1.TaskDescription);
                        command.Parameters.AddWithValue("@priority", l1.Priority);
                        command.Parameters.AddWithValue("@isupdated", false);
                        command.Parameters.AddWithValue("@updatedate", null);
                        command.Parameters.AddWithValue("@createdate", DateTime.Now);
                        SqlParameter outputparam = new SqlParameter("@instatus", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputparam);
                        command.ExecuteNonQuery();
                        if (outputparam.Value != DBNull.Value && (int)outputparam.Value != 100 && (int)outputparam.Value == 101)
                        {
                            apiResponse.statuscode = 101;
                            apiResponse.message = "Task Added successfully.";
                        }
                        else
                        {
                            apiResponse.statuscode = 100;
                            apiResponse.message = "Failed to add task.";
                        }
                    }
                    connection.Close();
               }
            }
            catch (Exception E)
            {
                Console.Write(E.Message);
            }
            return apiResponse;
        }

        public ApiResponse DeleteTask(ListRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    using (var command = new SqlCommand("Sp_DeleteNOTE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userid", l1.UserGID);
                        command.Parameters.AddWithValue("@taskid", l1.TaskGID);
                        command.Parameters.AddWithValue("@isDeleted", true);
                        command.Parameters.AddWithValue("@deletedate", DateTime.Now);
                        SqlParameter outputparam = new SqlParameter("@instatus", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputparam);
                        command.ExecuteNonQuery();
                        if (outputparam.Value != DBNull.Value && (int)outputparam.Value != 102 && (int)outputparam.Value == 101)
                        {
                            apiResponse.statuscode = 101;
                            apiResponse.message = "Task Deleted successfully.";
                        }
                        else
                        {
                            apiResponse.statuscode = 102;
                            apiResponse.message = "Failed to Delete task.";
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception E)
            {
                Console.Write(E.Message);
            }
            return apiResponse;
        }

        public ApiResponse GetAllTask(requestUserNotes r1)
        {
            ApiResponse apiResponse = new ApiResponse();
            List<Lists> lists = new List<Lists>();
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetNotes",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userid", r1.UserGID);
                        command.Parameters.AddWithValue("@search", r1.Search);
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Lists lolist = new Lists()
                            {
                                ID = (DBNull.Value != reader["ID"]) ? (int)reader["ID"] : 0,
                                UserGID = (DBNull.Value != reader["UserGID"]) ? (Guid)reader["UserGID"] : Guid.Empty,
                                TaskGID = (DBNull.Value!= reader["TaskGID"])? (Guid)reader["TaskGID"]: Guid.Empty,
                                TaskTitle = (DBNull.Value != reader["TaskTitle"])? (string)reader["TaskTitle"]: string.Empty,
                                TaskDescription = (DBNull.Value != reader["TaskDescription"])? (string)reader["TaskDescription"]: string.Empty,
                                Priority = (DBNull.Value != reader["Priority"])? (int)reader["Priority"]: 0,
                                DeletedDate = (DBNull.Value != reader["DeletedDate"])? (DateTime)reader["DeletedDate"]: DateTime.MinValue,
                                isUpdated = (DBNull.Value != reader["isUpdated"])? (bool)reader["isUpdated"]: false,
                                UpdatedDate = (DBNull.Value != reader["UpdatedDate"])? (DateTime)reader["UpdatedDate"]: DateTime.MinValue,
                                IsDeleted = (DBNull.Value != reader["IsDeleted"])? (bool)reader["IsDeleted"]: false,
                            };
                            lists.Add(lolist);
                        }
                        if(lists.Count > 0)
                        {
                            apiResponse.statuscode = 101;
                            apiResponse.message = "Task Fetched successfully.";
                            apiResponse.Responselist = lists;
                        }
                        else
                        {
                            apiResponse.statuscode = 100;
                            apiResponse.message = "Failed to Fetch task.";
                        }
                    }
                    
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            return apiResponse;
        }

        public ApiResponse UpdateTask(ListRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    using (var command = new SqlCommand("Sp_ManageNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userid", l1.UserGID);
                        command.Parameters.AddWithValue("@taskid", l1.TaskGID);
                        command.Parameters.AddWithValue("@tasktitle", l1.TaskTitle);
                        command.Parameters.AddWithValue("@taskdescription", l1.TaskDescription);
                        command.Parameters.AddWithValue("@priority", l1.Priority);
                        command.Parameters.AddWithValue("@isupdated", true);
                        command.Parameters.AddWithValue("@updatedate", DateTime.Now);
                        command.Parameters.AddWithValue("@createdate", null);
                        SqlParameter outputparam = new SqlParameter("@instatus", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputparam);
                        command.ExecuteNonQuery();
                        if (outputparam.Value != DBNull.Value && (int)outputparam.Value != 100 && (int)outputparam.Value == 102)
                        {
                            apiResponse.statuscode = 102;
                            apiResponse.message = "Task Updated successfully.";
                        }
                        else
                        {
                            apiResponse.statuscode = 100;
                            apiResponse.message = "Failed to Updated task.";
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception E)
            {
                Console.Write(E.Message);
            }
            return apiResponse;
        }

    }
}
