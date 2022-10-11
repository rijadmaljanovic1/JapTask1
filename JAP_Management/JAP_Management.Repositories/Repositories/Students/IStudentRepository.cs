using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Students
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<List<Student>> GetStudentsAsync(StudentSearchRequestModel studentSearchRequestModel);
        Task<Student> GetStudentById(string studentId);
        Task<Student> CommentStudentAsync(string studentId, string userId, string comment);
        Task<Student> AddStudentAsync(Student mappedStudent);

    }
}
