using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using todo_domain_entities.Entities;

namespace todo_domain_entities.EF
{
    /// <summary>
    /// A TodoDBContext instance represents a session with database.
    /// </summary>
    public class TodoDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets instances of TodoItem in database.
        /// </summary>
        /// <value>
        /// Set of TodoItem.
        /// </value>
        public DbSet<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// Gets or sets instances of TodoList in database.
        /// </summary>
        /// <value>
        /// Set of TodoList.
        /// </value>
        public DbSet<TodoList> TodoLists { get; set; }

        /// <summary>
        /// Gets or sets instances of User in database.
        /// </summary>
        /// <value>
        /// Set of User.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// The method to configure the database to be used in this context.
        /// </summary>
        /// <param name="optionsBuilder">Options of configuration.</param>
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
