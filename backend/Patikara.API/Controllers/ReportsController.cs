using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Patikara.Application.Common;
using Patikara.Application.DTOs;
using Patikara.Application.Interfaces;

namespace Patikara.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly ILogger<ReportsController> _logger;
    private readonly IHostEnvironment _environment;

    public ReportsController(IReportService reportService, ILogger<ReportsController> logger, IHostEnvironment environment)
    {
        _reportService = reportService;
        _logger = logger;
        _environment = environment;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<ApiResponse<ReportResponseDto>>> CreateReport([FromForm] CreateReportDto createReportDto, [FromForm] IFormFile[]? fotograflar)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<ReportResponseDto>.ErrorResponse("Validasyon hatası", errors));
            }

            // Fotoğraf validasyonu
            if (fotograflar != null && fotograflar.Length > 10)
            {
                return BadRequest(ApiResponse<ReportResponseDto>.ErrorResponse("En fazla 10 fotoğraf yüklenebilir."));
            }

            var result = await _reportService.CreateReportAsync(createReportDto, fotograflar, _environment);
            return Ok(ApiResponse<ReportResponseDto>.SuccessResponse(result, "İhbar başarıyla kaydedildi."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "İhbar kaydedilirken bir hata oluştu.");
            return StatusCode(500, ApiResponse<ReportResponseDto>.ErrorResponse("İhbar kaydedilirken bir hata oluştu."));
        }
    }
}

