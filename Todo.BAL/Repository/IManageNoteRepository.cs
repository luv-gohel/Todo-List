using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Interface;
using Todo.Models.DTO;
using Todo.Models.RequestDTO;
using Todo.Models.ResponseDTO;

namespace Todo.BAL.Repository
{
    public class IManageNoteRepository : Todo.BAL.Interface.IManageNoteInterface
    {
        public readonly IManageNoteInterface _manageNote;
        public IManageNoteRepository(IManageNoteInterface manageNote)
        {
            _manageNote = manageNote;
        }

        public ApiResponse AddUpdateTask(ListRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();    
            try
            {
                if((l1.TaskGID == Guid.Empty || l1.TaskGID == null) &&  l1.UserGID!=Guid.Empty)
                {
                    var dbresult = _manageNote.AddTask(l1);
                    if (dbresult.statuscode == 100)
                    {
                        apiResponse.statuscode = 100;
                        apiResponse.message = "Failed to Add task.";
                    }
                    else if(dbresult.statuscode == 101)
                    {
                        apiResponse.statuscode = dbresult.statuscode;
                        apiResponse.message = "Task Added successfully.";
                    }
                }
                else if(l1.TaskGID != Guid.Empty && l1.UserGID != Guid.Empty)
                {
                    var dbresult = _manageNote.UpdateTask(l1);
                    if (dbresult.statuscode == 100)
                    {
                        apiResponse.statuscode = 100;
                        apiResponse.message = "Failed to add task.";
                    }
                    else if(dbresult.statuscode == 102)
                    {
                        apiResponse.statuscode = dbresult.statuscode;
                        apiResponse.message = "Task Updated successfully.";
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return apiResponse;
        }

        public ApiResponse DeleteTask(ListRequest l1)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if(l1.TaskGID != null && l1.TaskGID != Guid.Empty && l1.UserGID != Guid.Empty && l1.IsDeleted)
                {
                    var dbresult = _manageNote.DeleteTask(l1);
                    if (dbresult.statuscode == 102)
                    {
                        apiResponse.statuscode = 102;
                        apiResponse.message = "Failed to delete task.";
                    }
                    else if(dbresult.statuscode == 101)
                    {
                        apiResponse.statuscode = dbresult.statuscode;
                        apiResponse.message = "Task Deleted successfully.";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return apiResponse;
        }

        public ApiResponse GetAllTask(requestUserNotes r1)
        
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if(r1.UserGID!=null && r1.UserGID != Guid.Empty)
                {
                    apiResponse = _manageNote.GetAllTask(r1);
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            return apiResponse;
        }
    }
}
