using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TreeStructure.Entities;

namespace TreeStructure.DAL
{
    internal sealed class TreeConfiguration : IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.HasKey(x => x.TreeId);
            builder.Property(x => x.TreeId).ValueGeneratedNever();
        }
    }
}

