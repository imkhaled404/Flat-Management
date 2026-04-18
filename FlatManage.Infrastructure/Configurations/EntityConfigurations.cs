using FlatManage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatManage.Infrastructure.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.Property(x => x.City).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.Floors)
                .WithOne(x => x.Building)
                .HasForeignKey(x => x.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.Property(x => x.MonthlyRent).HasColumnType("decimal(18,2)");
            builder.Property(x => x.SizeInSqFt).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Building)
                .WithMany()
                .HasForeignKey(x => x.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Floor)
                .WithMany(x => x.Units)
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class AgreementConfiguration : IEntityTypeConfiguration<Agreement>
    {
        public void Configure(EntityTypeBuilder<Agreement> builder)
        {
            builder.Property(x => x.MonthlyRent).HasColumnType("decimal(18,2)");
            builder.Property(x => x.SecurityDeposit).HasColumnType("decimal(18,2)");
            builder.Property(x => x.AdvanceAmount).HasColumnType("decimal(18,2)");
        }
    }

    public class RentInvoiceConfiguration : IEntityTypeConfiguration<RentInvoice>
    {
        public void Configure(EntityTypeBuilder<RentInvoice> builder)
        {
            builder.Property(x => x.RentAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.PaidAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.LateFee).HasColumnType("decimal(18,2)");
        }
    }

    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.RatePerUnit).HasColumnType("decimal(18,2)");
            builder.Property(x => x.PreviousReading).HasColumnType("decimal(18,2)");
            builder.Property(x => x.CurrentReading).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Units).HasColumnType("decimal(18,2)");
        }
    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");
        }
    }
}
