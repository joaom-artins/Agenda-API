using FluentValidation;
using Service.Domain.Request.v1.Login;

namespace Service.Domain.Validators.v1.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.EmailOrPhoneNumber)
         .NotEmpty().WithMessage("Email ou telefone é um campo obrigatório!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é um campo obrigatório!");
    }
}
