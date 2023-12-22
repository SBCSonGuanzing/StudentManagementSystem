﻿using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.EnrolledSubjectsServices
{
    public interface IEnrolledSubjectsService
    {

        Task<List<EnrolledSubjects>> GetAllEnrolledSubject();
        Task<int> AddEnrolledSubjects(EnrollmentDTO request);
        Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id);
    }
}
