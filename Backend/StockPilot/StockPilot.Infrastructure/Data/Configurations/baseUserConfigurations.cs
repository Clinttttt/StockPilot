using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Infrastructure.Data.Configurations
{
    public class baseUserConfigurations : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);

        

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(s => s.Address)
                .HasMaxLength(200);

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.RefreshToken)
                .HasMaxLength(200);

            builder.Property(s => s.LockedUntil);
            builder.Property(s => s.LastLoginAt);
            builder.Property(s => s.RefreshToken);
            builder.Property(s => s.RefreshTokenExpiryTime);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy).HasMaxLength(100);
        }
    }
}
