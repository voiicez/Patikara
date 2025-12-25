using Patikara.Application.DTOs;

namespace Patikara.Application.Interfaces;

public interface IReportService
{
    Task<ReportResponseDto> CreateReportAsync(CreateReportDto createReportDto);
}

