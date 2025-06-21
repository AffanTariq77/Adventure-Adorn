using Blaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blaze.Repository.Mapping
{
    public class UserConfigurationMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(u => u.MiddleName)
                .HasMaxLength(250);

            builder.Ignore(u => u.FullName);

            // Base entity properties
            builder.Property(u => u.CreatedDate)
                .IsRequired();

            builder.Property(u => u.LastModifiedDate);

            builder.Property(u => u.CreatedBy)
                .HasMaxLength(450);

            builder.Property(u => u.LastModifiedBy)
                .HasMaxLength(450);
        }
    }
}