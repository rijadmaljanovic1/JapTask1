﻿using AutoMapper;
using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Infrastructure.AutoMapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<Student, StudentModel>()
                .ForMember(d => d.CommentByUser, s => s.MapFrom(m => 0))
                .ForMember(d => d.FullName, s => s.MapFrom(m => m.FirstName + ' ' + m.LastName))
                .ForMember(d => d.MentorName, s => s.MapFrom(m => m.Mentor.FullName))
                .ForMember(d => d.StudentStatusName, s => s.MapFrom(m => m.StudentStatus.Name))
                .ForMember(d => d.ProgramName, s => s.MapFrom(m => m.Program.Name))
                .ForMember(d => d.SelectionName, s => s.MapFrom(m => m.Selection.SelectionName)).ReverseMap();

            CreateMap<Student, StudentUpsertRequest>().ReverseMap();
            
            CreateMap<Selection, SelectionUpsertRequest>().ReverseMap();

            CreateMap<Selection, SelectionModel>()
                .ForMember(d => d.ProgramName, s => s.MapFrom(m => m.Program.Name))
                .ForMember(d => d.Students,
                    s => s.MapFrom(
                        a => a.Students.Select(ma =>
                            new StudentModel2
                            {
                                Id = ma.Id,
                                FullName = ma.FirstName + " " + ma.LastName,
                            }).ToList()));

            CreateMap<Program, ProgramModel>().ForMember(d => d.Technologies,
                    s => s.MapFrom(
                        a => a.Technologies.Select(ma =>
                            new TechnologiesModel
                            {
                                Id = ma.Id,
                                Name = ma.Name,
                            }).ToList()));
        }
    }
}
