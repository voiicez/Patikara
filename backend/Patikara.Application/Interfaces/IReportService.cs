using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Patikara.Application.DTOs;

namespace Patikara.Application.Interfaces;

public interface IReportService
{
    Task<ReportResponseDto> CreateReportAsync(CreateReportDto createReportDto, IFormFile[]? fotograflar, IHostEnvironment environment);
}

