using Patikara.Core.Enums;

namespace Patikara.Application.DTOs;

public class ReportResponseDto
{
    public int Id { get; set; }
    public string AdSoyad { get; set; } = string.Empty;
    public string? TelefonNumarasi { get; set; }
    public string? EpostaAdresi { get; set; }
    public string? Mesaj { get; set; }
    public string IhbarAciklamasi { get; set; } = string.Empty;
    public string? Ayrinti { get; set; }
    public string AcikAdres { get; set; } = string.Empty;
    public List<string>? Fotograflar { get; set; }
    public ReportStatus Durum { get; set; }
    public DateTime OlusturmaTarihi { get; set; }
    public DateTime? GuncellemeTarihi { get; set; }
}

