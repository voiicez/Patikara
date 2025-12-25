namespace Patikara.Application.DTOs;

public class CreateReportDto
{
    public string AdSoyad { get; set; } = string.Empty;
    public string? TelefonNumarasi { get; set; }
    public string? EpostaAdresi { get; set; }
    public string? Mesaj { get; set; }
    public string IhbarAciklamasi { get; set; } = string.Empty;
    public string? Ayrinti { get; set; }
}

