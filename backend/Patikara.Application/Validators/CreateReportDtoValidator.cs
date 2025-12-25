using FluentValidation;
using Patikara.Application.DTOs;

namespace Patikara.Application.Validators;

public class CreateReportDtoValidator : AbstractValidator<CreateReportDto>
{
    public CreateReportDtoValidator()
    {
        RuleFor(x => x.AdSoyad)
            .NotEmpty().WithMessage("Ad Soyad alanı zorunludur.")
            .MaximumLength(200).WithMessage("Ad Soyad en fazla 200 karakter olabilir.");

        RuleFor(x => x.TelefonNumarasi)
            .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olabilir.")
            .When(x => !string.IsNullOrEmpty(x.TelefonNumarasi));

        RuleFor(x => x.EpostaAdresi)
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(200).WithMessage("E-posta adresi en fazla 200 karakter olabilir.")
            .When(x => !string.IsNullOrEmpty(x.EpostaAdresi));

        RuleFor(x => x.Mesaj)
            .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir.")
            .When(x => !string.IsNullOrEmpty(x.Mesaj));

        RuleFor(x => x.IhbarAciklamasi)
            .NotEmpty().WithMessage("İhbar açıklaması alanı zorunludur.")
            .MaximumLength(2000).WithMessage("İhbar açıklaması en fazla 2000 karakter olabilir.");

        RuleFor(x => x.Ayrinti)
            .MaximumLength(5000).WithMessage("Ayrıntı en fazla 5000 karakter olabilir.")
            .When(x => !string.IsNullOrEmpty(x.Ayrinti));

        RuleFor(x => x.AcikAdres)
            .NotEmpty().WithMessage("Açık adres alanı zorunludur.")
            .MaximumLength(500).WithMessage("Açık adres en fazla 500 karakter olabilir.");
    }
}

