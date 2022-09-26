using AutoMapper;
using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        #region GetStudents
        public async Task<List<StudentModel>> GetStudentsAsync(StudentSearchRequestModel studentRequestModel, int userId)
        {
            try
            {
                var studentList = await _studentRepository.GetStudentsAsync(studentRequestModel);

                if (!studentList.Any())
                    return null;

                var mappedStudentList = _mapper.Map<List<StudentModel>>(studentList);

                // get user comments for student
                for (int i = 0; i < studentList.Count; i++)
                {
                    mappedStudentList[i].CommentByUser = studentList[i].Comments.FirstOrDefault(mr => mr.UserId == userId)?.Comment ?? "/";
                }

                return mappedStudentList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region AddStudent
        public async Task<StudentUpsertRequest> AddStudentAsync(StudentUpsertRequest model)
        {
            try
            {
                var mappedAddedStudent = _mapper.Map<Student>(model);

                mappedAddedStudent.ModifiedAt = null;

                var addedStudent = await _studentRepository.Add(mappedAddedStudent);

                if (addedStudent == null)
                    return null;

                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region CommentStudent
        public async Task<StudentModel> CommentStudentAsync(int studentId, int userId, string comment)
        {
            try
            {
                var ratedStudent = await _studentRepository.CommentStudentAsync(studentId, userId, comment);

                if (ratedStudent == null)
                    return null;

                var mappedRatedStudent = _mapper.Map<StudentModel>(ratedStudent);

                // get user comments for student
                mappedRatedStudent.CommentByUser = ratedStudent.Comments.FirstOrDefault(mr => mr.UserId == userId)?.Comment ?? "/";

                Console.WriteLine("Comment for student added.");

                return mappedRatedStudent;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region GetStudentById
        public async Task<StudentModel> GetStudentById(int studentId, int userId)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(studentId);

                if (student == null)
                {
                    return null;
                }

                var mappedStudent = _mapper.Map<StudentModel>(student);

                // get user comments for students
                mappedStudent.CommentByUser = student.Comments.FirstOrDefault(mr => mr.UserId == userId)?.Comment ?? "/";

                return mappedStudent;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region GetStudentByUpsertId
        public async Task<StudentUpsertRequest> GetStudentByUpsertId(int studentId, int userId)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(studentId);

                if (student == null)
                {
                    return null;
                }

                var mappedStudent = _mapper.Map<StudentUpsertRequest>(student);


                return mappedStudent;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region UpdateStudent
        public async Task<StudentUpsertRequest> UpdateStudentAsync(int studentId, StudentUpsertRequest model)
        {
            try
            {
                var mappedStudent = _mapper.Map<Student>(model);

                mappedStudent.Id = studentId;
                mappedStudent.ModifiedAt = DateTime.Now;

                var updatedStudent = await _studentRepository.Update(mappedStudent);

                if (updatedStudent == null)
                    return null;

                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region DeleteStudent
        public async Task<StudentModel> DeleteStudentAsync(int id)
        {
            try
            {
                var deletedStudent = await _studentRepository.Delete(id);

                var mappedStudentModel = _mapper.Map<StudentModel>(deletedStudent);

                return mappedStudentModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion
    }
}
