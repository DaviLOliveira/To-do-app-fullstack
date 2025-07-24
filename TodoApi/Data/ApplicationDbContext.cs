using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Estas propriedades se tornarão as tabelas no banco de dados.
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}