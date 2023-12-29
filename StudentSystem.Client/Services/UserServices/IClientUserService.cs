﻿using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.UserServices
{
    public interface IClientUserService
    {
        List<User> users { get; set; }
        Task<List<User>> DeleteUser(int id);
        Task<List<User>> GetAllUser();

    }
}
