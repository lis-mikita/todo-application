using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using todo_domain_entities.Entities;
using System.IO;

namespace todo_domain_entities.EF
{
    public class TodoDBContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                 .AddJsonFile("appsettings.json", optional: false)
                 .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TodoConnection"));
        }

    }
}
