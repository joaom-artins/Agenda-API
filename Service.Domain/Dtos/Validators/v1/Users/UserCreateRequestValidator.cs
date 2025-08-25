using FluentValidation;
using Service.Domain.Dtos.Request.v1.Users;
using Service.Commons;

namespace Service.Domain.Dtos.Validators.v1.Users;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email é um campo obrigatório!")
           .EmailAddress().WithMessage("Email inválido!");

        RuleFor(x => x.Occupation)
            .NotEmpty().WithMessage("Profissão é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Profissão deve ter pelo menos 5 caracteres!")
            .MaximumLength(50).WithMessage("Profissão deve ter pelo menos 50 caracteres!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter pelo menos 5 caracteres!")
            .MaximumLength(50).WithMessage("Nome deve ter pelo menos 50 caracteres!");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Tipo de usuário é um campo obrigatório!")
            .IsInEnum().WithMessage("Tipo de usuário inválido!");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Número de telefone é um campo obrigatório!")
            .Matches(RegexStrings.PhoneNumber).WithMessage("Número de telefone só pode ter números!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é um campo obrigatório!")
            .Matches(RegexStrings.Password).WithMessage("Senha deve ter pelo menos 1 caractere maiúsculo, 1 minúsculo e 1 especial");
    }
}

