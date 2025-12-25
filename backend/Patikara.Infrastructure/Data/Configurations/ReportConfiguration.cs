using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patikara.Core.Entities;
using Patikara.Core.Enums;

namespace Patikara.Infrastructure.Data.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.AdSoyad)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.TelefonNumarasi)
            .HasMaxLength(20);

        builder.Property(r => r.EpostaAdresi)
            .HasMaxLength(200);

        builder.Property(r => r.Mesaj)
            .HasMaxLength(1000);

        builder.Property(r => r.IhbarAciklamasi)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.Ayrinti)
            .HasMaxLength(5000);

        builder.Property(r => r.Durum)
            .IsRequired()
            .HasDefaultValue(ReportStatus.Beklemede);

        builder.Property(r => r.OlusturmaTarihi)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(r => r.GuncellemeTarihi);
    }
}

