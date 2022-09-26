using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Infrastructure.Database;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using JAP_Management.Repositories.Repositories.Base;

namespace JAP_Management.Repositories.Repositories.Students
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly DatabaseContext _databaseContext;
        public StudentRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<List<Student>> GetStudentsAsync(StudentSearchRequestModel studentRequestModel)
        {
            try
            {
                var itemsPerPage = 5;
                var list = await _databaseContext.Students
                    .Include(z => z.Comments)
                    .Include(m => m.Selection)
                    .Include(m => m.StudentStatus)
                    .Include(m => m.Mentor)
                    .Include(m => m.Program)
                    .ThenInclude(m => m.Technologies)
                    .ToListAsync();

                return list
                .Where(m => Search(m, studentRequestModel.Search, studentRequestModel.Filter))
                .OrderBy(m => Sort(m, studentRequestModel.Sorting))
                .Skip(itemsPerPage * (studentRequestModel.Page - 1))
                .Take(5)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        private bool Search(Student student, SearchModel searchModel, int filter = 0)
        {
            if (searchModel == null)
            {
                return true;
            }
            searchModel.Value = searchModel.Value.ToLower();

            if (filter == 0 || filter == 1)
            {
                var x = student.FirstName.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 2)
            {
                var x = student.LastName.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 3)
            {
                var x = student.Selection.SelectionName.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 4)
            {
                var x = student.StudentStatus.Name.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 5)
            {
                var x = student.Mentor.FullName.ToLower().Contains(searchModel.Value);
                return x;
            }
            return false;
        }

        private dynamic Sort(Student student, int sorting = 0)
        {
            if (sorting == 1)
            {
                return student.Id;
            }
            else if (sorting == 2)
            {
                return student.FirstName;
            }
            else if (sorting == 3)
            {
                return student.Selection.SelectionName;
            }
            else if (sorting == 4)
            {
                return student.StudentStatus.Name;
            }
            else if (sorting == 5)
            {
                return student.Mentor.FullName;
            }
            return student.Id;
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            var student = await _databaseContext.Students
                .Include(z => z.Comments)
                .Include(m => m.Selection)
                .Include(m => m.StudentStatus)
                .Include(m => m.Mentor)
                .Include(m => m.Program)
                .ThenInclude(m => m.Technologies)
                .FirstOrDefaultAsync(m => m.Id == studentId);

            return student;
        }

        public async Task<Student> CommentStudentAsync(int studentId, int userId, string comment)
        {
            if (!await _databaseContext.Students.AnyAsync(m => m.Id == studentId))
                return null;

            var studentComment = await _databaseContext.Comments.FirstOrDefaultAsync(mr => mr.UserId == userId && mr.StudentId == studentId);

            if (studentComment == null)
            {
                await _databaseContext.Comments.AddAsync(
                    new Comments { UserId = userId, StudentId = studentId, Comment = comment });
            }
            else
            {
                studentComment.Comment = comment;
            }

            await _databaseContext.SaveChangesAsync();

            return await _databaseContext.Students
                .FirstOrDefaultAsync(m => m.Id == studentId);
        }
    }
}
