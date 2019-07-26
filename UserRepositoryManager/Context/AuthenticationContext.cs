
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserModel;

namespace UserRepositoryManager.Context
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
