using Microsoft.AspNetCore.Mvc;
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

    public ReportsController(IReportService reportService, ILogger<ReportsController> logger)
    {
        _reportService = reportService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ReportResponseDto>>> CreateReport([FromBody] CreateReportDto createReportDto)
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

            var result = await _reportService.CreateReportAsync(createReportDto);
            return Ok(ApiResponse<ReportResponseDto>.SuccessResponse(result, "İhbar başarıyla kaydedildi."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "İhbar kaydedilirken bir hata oluştu.");
            return StatusCode(500, ApiResponse<ReportResponseDto>.ErrorResponse("İhbar kaydedilirken bir hata oluştu."));
        }
    }
}

