using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Patikara.Application.DTOs;
using Patikara.Application.Interfaces;
using Patikara.Core.Entities;
using Patikara.Core.Enums;
using Patikara.Infrastructure.Data;

namespace Patikara.Application.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReportResponseDto> CreateReportAsync(CreateReportDto createReportDto, IFormFile[]? fotograflar, IHostEnvironment environment)
    {
        var report = new Report
        {
            AdSoyad = createReportDto.AdSoyad,
            TelefonNumarasi = createReportDto.TelefonNumarasi,
            EpostaAdresi = createReportDto.EpostaAdresi,
            Mesaj = createReportDto.Mesaj,
            IhbarAciklamasi = createReportDto.IhbarAciklamasi,
            Ayrinti = createReportDto.Ayrinti,
            AcikAdres = createReportDto.AcikAdres,
            Durum = ReportStatus.Beklemede,
            OlusturmaTarihi = DateTime.UtcNow
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        // Fotoğrafları yükle
        var fotoYollari = new List<string>();
        if (fotograflar != null && fotograflar.Length > 0)
        {
            fotoYollari = await UploadPhotosAsync(fotograflar, report.Id, environment);
            report.Fotograflar = JsonSerializer.Serialize(fotoYollari);
            await _context.SaveChangesAsync();
        }

        return new ReportResponseDto
        {
            Id = report.Id,
            AdSoyad = report.AdSoyad,
            TelefonNumarasi = report.TelefonNumarasi,
            EpostaAdresi = report.EpostaAdresi,
            Mesaj = report.Mesaj,
            IhbarAciklamasi = report.IhbarAciklamasi,
            Ayrinti = report.Ayrinti,
            AcikAdres = report.AcikAdres,
            Fotograflar = fotoYollari.Count > 0 ? fotoYollari : null,
            Durum = report.Durum,
            OlusturmaTarihi = report.OlusturmaTarihi,
            GuncellemeTarihi = report.GuncellemeTarihi
        };
    }

    private async Task<List<string>> UploadPhotosAsync(IFormFile[] files, int reportId, IHostEnvironment environment)
    {
        var uploadedPaths = new List<string>();
        var contentRootPath = environment.ContentRootPath;
        var uploadPath = Path.Combine(contentRootPath, "wwwroot", "uploads", "reports", reportId.ToString());

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        foreach (var file in files.Take(10)) // Max 10 dosya
        {
            if (file == null || file.Length == 0)
                continue;

            // Dosya boyutu kontrolü
            if (file.Length > MaxFileSize)
                continue;

            // Dosya uzantısı kontrolü
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                continue;

            // Benzersiz dosya adı oluştur
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Web path'i ekle
            var webPath = $"/uploads/reports/{reportId}/{fileName}";
            uploadedPaths.Add(webPath);
        }

        return uploadedPaths;
    }
}

