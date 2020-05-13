using Microsoft.EntityFrameworkCore;
using upromiscontractapi.Models;

namespace upromiscontractapi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AccountInfo> AccountInfo { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

    }
}