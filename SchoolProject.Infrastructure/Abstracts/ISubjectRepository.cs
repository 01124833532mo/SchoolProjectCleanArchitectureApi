﻿using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts
{
    public interface ISubjectRepository : IGenericRepository<Subjects>
    {
        Task<List<Subjects>> GetSubjectsListAsync();

    }
}
