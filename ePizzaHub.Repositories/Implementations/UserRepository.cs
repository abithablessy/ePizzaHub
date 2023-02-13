﻿using ePizzahub.Core;
using ePizzahub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {   AppDbContext context { get { return _db as AppDbContext; } }
        public UserRepository(AppDbContext db) : base(db)
        {
        }

        public bool CreateUser(User user, string Role)
        {
            Role role =  context.Roles.Where(r=>r.Name == Role).FirstOrDefault();
            if (role != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Roles.Add(role);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            User user = context.Users.Include(u=>u.Roles).Where(u=>u.Email== Email).FirstOrDefault();
            if(user != null)
            {
                bool isVerified = BCrypt.Net.BCrypt.Verify(Password, user.Password);
                if(isVerified)
                {
                    UserModel model = new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = user.Roles.Select(r => r.Name).ToArray()

                    };
                    return model;

                }
            }
            return null;
        }
    }
}
