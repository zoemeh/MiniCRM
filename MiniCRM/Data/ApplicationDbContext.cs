using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniCRM.Models;

namespace MiniCRM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}