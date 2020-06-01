using Microsoft.EntityFrameworkCore;
using upromisattachmentapi.Models;
using upromisattachmentapi.data.EntityConfigurations;

namespace upromisattachmentapi
{ 
    public class AttachmentContext : DbContext
    {
        public AttachmentContext(DbContextOptions<AttachmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfiguration());
        }

    }
}