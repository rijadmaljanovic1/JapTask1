using JAP_Management.Core.Entities;
using JAP_Management.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Infrastructure.Database
{
    public static class DatabaseInitializer
    {
        public static void Init(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            #region UserDataSeed

            var userList = new List<BaseUser>();

            for (int i = 1; i <= 5; i++)
            {
                var salt = PasswordHashSaltGenerator.GenerateSalt();

                var usernamePassword = "User" + i;

                userList.Add(new BaseUser
                {
                    Username = usernamePassword,
                    PasswordSalt = salt,
                    PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                });
            }
            context.Users.AddRange(userList);
            context.SaveChanges();

            #endregion

            #region MentorDataSeed

            var mentorList = new List<Mentor>();

            for (int i = 1; i <= 5; i++)
            {
                if (i == 1)
                {
                    mentorList.Add(new Mentor
                    {
                        FullName = "Harun Cavcic"
                    });
                }
                else if (i == 2)
                {
                    mentorList.Add(new Mentor
                    {
                        FullName = "Rijad Maljanovic"
                    });
                }
                else if (i == 3)
                {
                    mentorList.Add(new Mentor
                    {
                        FullName = "Adil Joldic"
                    });
                }
                else if (i == 4)
                {
                    mentorList.Add(new Mentor
                    {
                        FullName = "Emina Junuz"
                    });
                }
                else if (i == 5)
                {
                    mentorList.Add(new Mentor
                    {
                        FullName = "Nina Bijedic"
                    });
                }
            }
            context.Mentors.AddRange(mentorList);
            context.SaveChanges();

            #endregion

            #region ProgramDataSeed

            var programList = new List<Program>();

            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                {
                    programList.Add(new Program
                    {
                        Name = "JAP_Dev",
                        Description = "On the dev track, you will upgrade your skills in .Net Core and Angular"
                    });
                }
                else if (i == 2)
                {
                    programList.Add(new Program
                    {
                        Name = "JAP_QA",
                        Description = "On the qa track, you will upgrade your skills in testing software"
                    });
                }
                else if (i == 3)
                {
                    programList.Add(new Program
                    {
                        Name = "JAP_DevOps",
                        Description = "On the devOps track, you will upgrade your skills in AWS and managing servers"
                    });
                }
                else if (i == 4)
                {
                    programList.Add(new Program
                    {
                        Name = "JAP_TA",
                        Description = "On the talent acquisition track, you will upgrade your skills in finding new potential developers"
                    });
                }
            }
            context.Programs.AddRange(programList);
            context.SaveChanges();

            #endregion

            #region TechnologiesDataSeed

            var technologyList = new List<Technologies>();

            for (int i = 1; i <= 5; i++)
            {
                if (i == 1)
                {
                    technologyList.Add(new Technologies
                    {
                        Name = ".Net Core",
                        ProgramId = 1
                    });
                }
                else if (i == 2)
                {
                    technologyList.Add(new Technologies
                    {
                        Name = "Angular",
                        ProgramId = 1
                    });
                }
                else if (i == 3)
                {
                    technologyList.Add(new Technologies
                    {
                        Name = "Manual Testing",
                        ProgramId = 2
                    });
                }
                else if (i == 4)
                {
                    technologyList.Add(new Technologies
                    {
                        Name = "AWS",
                        ProgramId = 3
                    });
                }
                else if (i == 5)
                {
                    technologyList.Add(new Technologies
                    {
                        Name = "Kotlin",
                        ProgramId = 4
                    });
                }
            }
            context.Technologies.AddRange(technologyList);
            context.SaveChanges();

            #endregion

            #region SelectionStatusDataSeed

            var selectionStatusList = new List<SelectionStatus>();

            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    selectionStatusList.Add(new SelectionStatus
                    {
                        Name = "Active"
                    });
                }
                else if (i == 2)
                {
                    selectionStatusList.Add(new SelectionStatus
                    {
                        Name = "Completed"
                    });
                }
            }
            context.SelectionStatus.AddRange(selectionStatusList);
            context.SaveChanges();

            #endregion

            #region SelectionDataSeed

            var selectionList = new List<Selection>();

            for (int i = 1; i <= 7; i++)
            {
                if (i == 1)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_DEV/2020",
                        StatusId = 1,
                        Year = "2020",
                        ProgramId = 1
                    });
                }
                else if (i == 2)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_QA/2021",
                        StatusId = 1,
                        Year = "2021",
                        ProgramId = 2
                    });
                }
                else if (i == 3)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_DEV/2022",
                        StatusId = 1,
                        Year = "2022",
                        ProgramId = 1
                    });
                }
                else if (i == 4)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_TA/2021",
                        StatusId = 2,
                        Year = "2021",
                        ProgramId = 4
                    });
                }
                else if (i == 5)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_DEVOPS/2021",
                        StatusId = 1,
                        Year = "2020",
                        ProgramId = 3
                    });
                }
                else if (i == 6)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_DEVOPS/2018",
                        StatusId = 1,
                        Year = "2020",
                        ProgramId = 3
                    });
                }
                else if (i == 7)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_TA/2017",
                        StatusId = 1,
                        Year = "2020",
                        ProgramId = 3
                    });
                }
            }
            context.Selections.AddRange(selectionList);
            context.SaveChanges();

            #endregion

            #region StudentStatusDataSeed

            var studentStatusList = new List<StudentStatus>();

            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                {
                    studentStatusList.Add(new StudentStatus
                    {
                        Name = "InProgram"
                    });
                }
                else if (i == 2)
                {
                    studentStatusList.Add(new StudentStatus
                    {
                        Name = "Success"
                    });
                }
                else if (i == 3)
                {
                    studentStatusList.Add(new StudentStatus
                    {
                        Name = "Extended"
                    });
                }
                else if (i == 4)
                {
                    studentStatusList.Add(new StudentStatus
                    {
                        Name = "Failed"
                    });
                }
            }
            context.StudentStatus.AddRange(studentStatusList);
            context.SaveChanges();

            #endregion

            #region StudentDataSeed

            var studentList = new List<Student>();

            for (int i = 1; i <= 13; i++)
            {
                if (i == 1)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Denis",
                        LastName = "Music",
                        StudentStatusId = 1,
                        DateOfBirth = DateTime.Now,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1
                    });
                }
                else if (i == 2)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Elmir",
                        LastName = "Babovic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 3,
                        MentorId = 2,
                        SelectionId = 2,
                        ProgramId = 2
                    });
                }
                else if (i == 3)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Jasmin",
                        LastName = "Azemovic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 2,
                        MentorId = 1,
                        SelectionId = 3,
                        ProgramId = 3
                    });
                }
                else if (i == 4)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Amel",
                        LastName = "Music",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 1,
                        MentorId = 3,
                        SelectionId = 5,
                        ProgramId = 3
                    });
                }
                else if (i == 5)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Lejla",
                        LastName = "Jazvin",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 1,
                        MentorId = 3,
                        SelectionId = 2,
                        ProgramId = 2
                    });
                }
                else if (i == 6)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Irma",
                        LastName = "Dedic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 4,
                        MentorId = 4,
                        SelectionId = 2,
                        ProgramId = 2
                    });
                }
                else if (i == 7)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Larisa",
                        LastName = "Dedovic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 2,
                        MentorId = 2,
                        SelectionId = 4,
                        ProgramId = 4
                    });
                }
                else if (i == 8)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Admir",
                        LastName = "Sehidic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 1,
                        MentorId = 1,
                        SelectionId = 3,
                        ProgramId = 3
                    });
                }
                else if (i == 9)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Veldin",
                        LastName = "Ovcina",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 3,
                        MentorId = 2,
                        SelectionId = 2,
                        ProgramId = 2
                    });
                }
                else if (i == 10)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Damir",
                        LastName = "Morankic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 3,
                        MentorId = 3,
                        SelectionId = 3,
                        ProgramId = 3
                    });
                }
                else if (i == 11)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Adnan",
                        LastName = "Mahovkic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 2,
                        MentorId = 4,
                        SelectionId = 4,
                        ProgramId = 4
                    });
                }
                else if (i == 12)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Mirza",
                        LastName = "Salkic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 2,
                        MentorId = 2,
                        SelectionId = 2,
                        ProgramId = 4
                    });
                }
                else if (i == 13)
                {
                    studentList.Add(new Student
                    {
                        FirstName = "Muhamed",
                        LastName = "Fazlic",
                        DateOfBirth = DateTime.Now,
                        StudentStatusId = 2,
                        MentorId = 4,
                        SelectionId = 4,
                        ProgramId = 4
                    });
                }
            }
            context.Students.AddRange(studentList);
            context.SaveChanges();

            #endregion

            #region CommentsDataSeed

            var commentList = new List<Comments>();

            for (int i = 1; i <= 5; i++)
            {
                if (i==1)
                {
                    commentList.Add(new Comments
                    {
                        UserId = 1,
                        StudentId = 1,
                        Comment = "Good student"
                    });
                }
                else if (i == 2)
                {
                    commentList.Add(new Comments
                    {
                        UserId = 1,
                        StudentId = 2,
                        Comment = "Nothing special"
                    });
                }
                else if (i == 3)
                {
                    commentList.Add(new Comments
                    {
                        UserId = 1,
                        StudentId = 4,
                        Comment = "Smart but lazy"
                    });
                }
                else if (i == 4)
                {
                    commentList.Add(new Comments
                    {
                        UserId = 1,
                        StudentId = 5,
                        Comment = "Passed the first test"
                    });
                }
                else if (i == 5)
                {
                    commentList.Add(new Comments
                    {
                        UserId = 1,
                        StudentId = 7,
                        Comment = "Ok"
                    });
                }
            }
            context.Comments.AddRange(commentList);
            context.SaveChanges();

            #endregion
        }
    }
}
