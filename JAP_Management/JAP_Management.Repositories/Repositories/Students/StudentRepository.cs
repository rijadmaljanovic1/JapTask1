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
using AutoMapper;

namespace JAP_Management.Repositories.Repositories.Students
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public StudentRepository(DatabaseContext databaseContext, IMapper _mapper) : base(databaseContext)
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
                    .Include(m => m.BaseUser)
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

        public async Task<Student> AddStudentAsync(Student mappedStudent)
        {
            var newStudent = new Student()
            {
                BaseUserId = mappedStudent.BaseUserId,
                MentorId = mappedStudent.MentorId,
                SelectionId = mappedStudent.SelectionId,
                ProgramId = mappedStudent.ProgramId,
                StudentStatusId = mappedStudent.StudentStatusId
            };
            _databaseContext.Students.Add(newStudent);
            _databaseContext.SaveChanges();

            var selectionItems = await _databaseContext.SelectionItems.Where(m => m.SelectionId == mappedStudent.SelectionId).ToListAsync();
            var studentItems = new List<StudentItem>();

            foreach (var item in selectionItems)
            {
                studentItems.Add(new StudentItem()
                {
                    StudentId = mappedStudent.BaseUserId,
                    ItemId = item.ItemId,
                    PercentageDone = "0%",
                    CandidateStatus = "NotStarted"
                });
            }
            await _databaseContext.AddRangeAsync(studentItems);
            await _databaseContext.SaveChangesAsync();

            return newStudent;
        }

        private bool Search(Student student, SearchModel searchModel, int filter = 0)
        {
            if (searchModel.Value == "")
            {
                return true;
            }
            searchModel.Value = searchModel.Value.ToLower();

            if (filter == 0 || filter == 1)
            {
                var x = student.BaseUser.FirstName.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 2)
            {
                var x = student.BaseUser.LastName.ToLower().Contains(searchModel.Value);
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
                return student.BaseUserId;
            }
            else if (sorting == 2)
            {
                return student.BaseUser.FirstName;
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
            return student.BaseUserId;
        }

        public async Task<StudentModel> GetStudentById(string studentId, string userId)
        {
            var student = await _databaseContext.Students
                .Include(z => z.Comments)
                .Include(m => m.Selection)
                .Include(m => m.StudentStatus)
                .Include(m => m.BaseUser)
                .Include(m => m.Mentor)
                .Include(m => m.Program)
                .ThenInclude(m => m.Technologies)
                .FirstOrDefaultAsync(m => m.BaseUserId == studentId);

            var mappedStudent = new StudentModel()
            {
                BaseUserId = student.BaseUserId,
                FullName = student.BaseUser.FirstName + ' ' + student.BaseUser.LastName,
                MentorName = student.Mentor.FullName,
                SelectionName = student.Selection.SelectionName,
                ProgramName = student.Program.Name,
                StudentStatusName = student.StudentStatus.Name,
            };

            mappedStudent.CommentByUser = student.Comments.FirstOrDefault(mr => mr.AdminId == userId)?.Comment ?? "/";

            var studentItems = await _databaseContext.StudentItems.Where(m => m.StudentId == student.BaseUserId).ToListAsync();

            var selectionItems = await _databaseContext.SelectionItems.Where(m => m.SelectionId == student.SelectionId).ToListAsync();

            var programItems = await _databaseContext.ProgramItems.ToListAsync();
            programItems = programItems.OrderBy(m => m.OrderNumber).ToList();

            var allItems = await _databaseContext.Items.ToListAsync();

            mappedStudent.Items = new List<StudentItemsModel>();


            foreach (var item in studentItems)
            {
                foreach (var x in allItems)
                {
                    if (item.ItemId == x.Id)
                    {
                        mappedStudent.Items.Add(new StudentItemsModel()
                        {
                            ItemId = x.Id,
                            ItemName = x.Name,
                            Url = x.Url,
                            ExpectedHours = x.ExpectedHours,
                        });
                    }
                }
            }
            foreach (var item in mappedStudent.Items)
            {
                foreach (var x in selectionItems)
                {
                    if (item.ItemId == x.ItemId)
                    {
                        item.StartDate = x.StartDate;
                        item.EndDate = x.EndDate;
                    }
                }
            }
            foreach (var item in mappedStudent.Items)
            {
                foreach (var program in programItems)
                {
                    if (item.ItemId == program.ItemId)
                    {
                        item.OrderNumber = program.OrderNumber;
                    }

                }
            }
            var newStudent = new StudentModel();
            mappedStudent.Items = mappedStudent.Items.OrderBy(m => m.OrderNumber).ToList();

            return mappedStudent;
        }

        public async Task<Student> CommentStudentAsync(string studentId, string userId, string comment)
        {
            if (!await _databaseContext.Students.AnyAsync(m => m.BaseUserId == studentId))
                return null;

            var studentComment = await _databaseContext.Comments.FirstOrDefaultAsync(mr => mr.AdminId == userId && mr.StudentId == studentId);

            if (studentComment == null)
            {
                await _databaseContext.Comments.AddAsync(
                    new Comments { AdminId = userId, StudentId = studentId, Comment = comment });
            }
            else
            {
                studentComment.Comment = comment;
            }

            await _databaseContext.SaveChangesAsync();

            return await _databaseContext.Students
                .FirstOrDefaultAsync(m => m.BaseUserId == studentId);
        }
        public async Task BackgroundEmailSender()
        {
           
        }
    }
}
