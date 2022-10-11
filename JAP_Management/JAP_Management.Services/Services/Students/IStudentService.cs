using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Students
{
    public interface IStudentService
    {
        Task<List<StudentModel>> GetStudentsAsync(StudentSearchRequestModel studentRequestModel, string userId);
        Task<StudentUpsertRequest> AddStudentAsync(StudentUpsertRequest model);
        Task<StudentModel> CommentStudentAsync(string studentId, string userId, string comment);
        Task<StudentModel> GetStudentById(string studentId, string userId);
        Task<StudentUpsertRequest> UpdateStudentAsync(string studentId, StudentUpsertRequest model);
        Task<StudentUpsertRequest> GetStudentByUpsertId(string studentId, string userId);
        Task<StudentModel> DeleteStudentAsync(string id);

    }
}
