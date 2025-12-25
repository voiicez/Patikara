using Microsoft.EntityFrameworkCore;
using Patikara.Application.DTOs;
using Patikara.Application.Interfaces;
using Patikara.Core.Entities;
using Patikara.Core.Enums;
using Patikara.Infrastructure.Data;

namespace Patikara.Application.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReportResponseDto> CreateReportAsync(CreateReportDto createReportDto)
    {
        var report = new Report
        {
            AdSoyad = createReportDto.AdSoyad,
            TelefonNumarasi = createReportDto.TelefonNumarasi,
            EpostaAdresi = createReportDto.EpostaAdresi,
            Mesaj = createReportDto.Mesaj,
            IhbarAciklamasi = createReportDto.IhbarAciklamasi,
            Ayrinti = createReportDto.Ayrinti,
            Durum = ReportStatus.Beklemede,
            OlusturmaTarihi = DateTime.UtcNow
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return new ReportResponseDto
        {
            Id = report.Id,
            AdSoyad = report.AdSoyad,
            TelefonNumarasi = report.TelefonNumarasi,
            EpostaAdresi = report.EpostaAdresi,
            Mesaj = report.Mesaj,
            IhbarAciklamasi = report.IhbarAciklamasi,
            Ayrinti = report.Ayrinti,
            Durum = report.Durum,
            OlusturmaTarihi = report.OlusturmaTarihi,
            GuncellemeTarihi = report.GuncellemeTarihi
        };
    }
}

