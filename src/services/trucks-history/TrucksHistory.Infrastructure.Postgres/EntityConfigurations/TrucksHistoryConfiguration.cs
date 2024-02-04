using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.EntityConfigurations;
public class TrucksHistoryConfiguration : IEntityTypeConfiguration<TruckHistoryDbModel>
{
    public void Configure(EntityTypeBuilder<TruckHistoryDbModel> builder)
    {
        builder.ToTable("TrucksHistory");
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.CreateDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.StatusCode).IsRequired();
        builder.HasMany(x => x.Entries).WithOne(x => x.TruckHistory).HasForeignKey(x => x.TruckId);
    }
}
