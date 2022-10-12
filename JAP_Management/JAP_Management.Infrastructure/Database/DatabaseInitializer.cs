using JAP_Management.Core.Entities;
using JAP_Management.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public async static void Initialize(DatabaseContext context, RoleManager<IdentityRole> roleManager, UserManager<BaseUser> userManager)
        {
            context.Database.EnsureCreated();

            #region RolesDataSeed

            bool adminRoleReady = false;
            bool studentRoleReady = false;

            var admin = new IdentityRole()
            {
                Name = "Admin",
            };

            var res = roleManager.CreateAsync(admin).Result;

            var student = new IdentityRole()
            {
                Name = "Student",
            };

            var res1 = roleManager.CreateAsync(student).Result;

            adminRoleReady = res.Succeeded;
            studentRoleReady = res1.Succeeded;

            #endregion

            #region UserDataSeed

            var userList = new List<BaseUser>();

            for (int i = 1; i <= 8; i++)
            {
                var salt = PasswordHashSaltGenerator.GenerateSalt();

                var usernamePassword = "User" + i;

                if (i == 1)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Adnan",
                        LastName = "Mahovkic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 2)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Damir",
                        LastName = "Morankic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 3)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Denis",
                        LastName = "Music",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 4)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Jasmin",
                        LastName = "Azemovic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 5)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Admir",
                        LastName = "Sehidic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 6)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Larisa",
                        LastName = "Dedovic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 7)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Nina",
                        LastName = "Bijedic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 8)
                {
                    userList.Add(new BaseUser
                    {
                        FirstName = "Elmir",
                        LastName = "Babovic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
            }
            context.Users.AddRange(userList);
            context.SaveChanges();

            #endregion

            #region AdminUserDataSeed

            var adminUserList = new List<BaseUser>();

            for (int i = 1; i <= 2; i++)
            {
                var salt = PasswordHashSaltGenerator.GenerateSalt();

                var usernamePassword = "admin" + i;

                if (i == 1)
                {
                    adminUserList.Add(new BaseUser
                    {
                        FirstName = "Rijad",
                        LastName = "Maljanovic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }
                else if (i == 2)
                {
                    adminUserList.Add(new BaseUser
                    {
                        FirstName = "Harun",
                        LastName = "Cavcic",
                        UserName = usernamePassword,
                        PasswordSalt = salt,
                        PasswordHash = PasswordHashSaltGenerator.HashPassword(salt, usernamePassword)
                    });
                }

            }
            context.Users.AddRange(adminUserList);
            context.SaveChanges();

            #endregion

            #region UserRolesDataSeed

            var users = context.Users.ToList();

            for (int i = 0; i < users.Count; i++)
            {
                if(users[i].FirstName == "Rijad" || users[i].FirstName == "Harun")
                    await userManager.AddToRoleAsync(new BaseUser() { Id = users[i].Id }, "Admin");
                else
                    await userManager.AddToRoleAsync(new BaseUser() { Id = users[i].Id }, "Student");

            }

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

            for (int i = 1; i <= 5; i++)
            {
                if (i == 1)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_DEV/2022",
                        StatusId = 1,
                        Year = "2022",
                        ProgramId = 1
                    });
                }
                else if (i == 2)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_QA/2022",
                        StatusId = 1,
                        Year = "2022",
                        ProgramId = 2
                    });
                }
                else if (i == 3)
                {
                    selectionList.Add(new Selection
                    {
                        SelectionName = "JAP_DEV/2020",
                        StatusId = 1,
                        Year = "2020",
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

            for (int i = 0; i < userList.Count; i++)
            {
                if (i==1)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1,
                        StudentStatusId = 2,
                    });
                }
                else if (i==2)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1,
                        StudentStatusId = 2,
                    });
                }
                else if (i == 3)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1,
                        StudentStatusId = 2,
                    });
                }
                else if (i == 4)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1,
                        StudentStatusId = 2,
                    });
                }
                else if (i == 5)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1,
                        StudentStatusId = 1,
                    });
                }
                else if (i == 6)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 1,
                        ProgramId = 1,
                        StudentStatusId = 1,
                    });
                }
                else if (i == 7)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 2,
                        ProgramId = 1,
                        StudentStatusId = 2,
                    });
                }
                else if (i == 8)
                {
                    studentList.Add(new Student
                    {
                        BaseUserId = userList[i].Id,
                        MentorId = 1,
                        SelectionId = 2,
                        ProgramId = 1,
                        StudentStatusId = 1,
                    });
                }

            }
            context.Students.AddRange(studentList);
            context.SaveChanges();

            #endregion

            #region AdminDataSeed

            var adminList = new List<Admin>();
            var rijad = context.Users.Where(x => x.FirstName == "Rijad").FirstOrDefault();
            var harun = context.Users.Where(x => x.FirstName == "Harun").FirstOrDefault();

            adminList.Add(new Admin
            {
                BaseUserId = rijad.Id
            });
            adminList.Add(new Admin
            {
                BaseUserId = harun.Id
            });
            context.Admins.AddRange(adminList);
            context.SaveChanges();

            #endregion

        }
    }
}
