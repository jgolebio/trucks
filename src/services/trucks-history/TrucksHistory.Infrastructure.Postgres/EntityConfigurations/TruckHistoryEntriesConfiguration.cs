using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrucksHistory.Infrastructure.Postgres.DbModels;

namespace TrucksHistory.Infrastructure.Postgres.EntityConfigurations;

public class TruckHistoryEntriesConfiguration : IEntityTypeConfiguration<TruckHistoryEntryDbModel>
{
    public void Configure(EntityTypeBuilder<TruckHistoryEntryDbModel> builder)
    {
        builder.ToTable("TruckHistoryEntry");
        builder.Property(x => x.Id);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EntryDate);
        builder.Property(x => x.Status);
        builder.Property(x => x.StatusCode);
    }
}
