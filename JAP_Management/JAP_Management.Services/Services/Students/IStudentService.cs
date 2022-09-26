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
        Task<List<StudentModel>> GetStudentsAsync(StudentSearchRequestModel studentRequestModel, int userId);
        Task<StudentUpsertRequest> AddStudentAsync(StudentUpsertRequest model);
        Task<StudentModel> CommentStudentAsync(int studentId, int userId, string comment);
        Task<StudentModel> GetStudentById(int studentId, int userId);
        Task<StudentUpsertRequest> UpdateStudentAsync(int studentId, StudentUpsertRequest model);
        Task<StudentUpsertRequest> GetStudentByUpsertId(int studentId, int userId);
        Task<StudentModel> DeleteStudentAsync(int id);

    }
}
