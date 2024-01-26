using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trucks.Infrastructure.Sql.DbModels;

namespace Trucks.Infrastructure.Sql.EntityConfigurations
{
    internal class TrucksConfiguration : IEntityTypeConfiguration<TruckDbModel>
    {
        public void Configure(EntityTypeBuilder<TruckDbModel> builder)
        {
            builder.ToTable("Trucks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name);
            builder.Property(x => x.Code);
            builder.Property(x => x.Description);
            builder.Property(x => x.Status);
        }
    }
}
