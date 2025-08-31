using AudioBooksLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioBooksLibrary.Infrastructure.Configurations;

public class AppUserConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.DisplayName).IsRequired().HasMaxLength(128);
    }
}