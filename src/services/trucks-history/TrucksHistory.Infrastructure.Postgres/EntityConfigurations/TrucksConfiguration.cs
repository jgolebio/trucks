using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.EntityConfigurations;
public class TrucksConfiguration : IEntityTypeConfiguration<TruckDbModel>
{
    public void Configure(EntityTypeBuilder<TruckDbModel> builder)
    {
        builder.ToTable("Trucks");
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.CreateDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x=>x.StatusCode).IsRequired();
    }
}
