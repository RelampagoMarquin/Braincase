﻿using Api.Dto.User;
using Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.Repository.Interfaces
{
    public interface IUserRepository: IBaseRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(String id);

        Task<User?> GetUserByEmail(String email);

        Task<IdentityResult> AddUser(CreateUserDTO createUserDTO);

        Task<IdentityResult> UpdateUser(User user);

        Task<IdentityResult> DeleteUser(User user);

        Task<IdentityResult> ChangePassword(User user, String oldPassword, String password);

        Task<bool> CheckPasswordAsync(User user, String password);

    }
}
