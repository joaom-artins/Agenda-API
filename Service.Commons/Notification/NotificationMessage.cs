namespace Service.Commons.Notification;

public class NotificationMessage
{
    public static class Common
    {
        public static readonly string UnexpectedError = "Erro inesperado!";
        public static readonly string ValidationError = "Ocorreram um ou mais erros de validação!";
        public static readonly string RequestListRequired = "Lista da requisição não pode estar vazia!";
        public static readonly string DataExists = "Dados já cadastrados";
    }

    public static class User
    {
        public static readonly string PasswordAreDifferent = "As senhas são diferentes!";
        public static readonly string EmailAlreadyExists = "Email já está em uso!";
        public static readonly string PhoneNumberAlreadyExists = "Número de telefone já está em uso!";
        public static readonly string InvalidData = "Dados inválidos!";
        public static readonly string NotFound = "Usuário não encontrado!";
    }

    public static class AvailabilitySlot
    {
        public static readonly string EndTimeCannotBeLessThanStart = "Horário final não pode ser menos que inicial!";
        public static readonly string TimesCannotBeSame = "Horários não pode ser iguais!";
        public static readonly string InvalidDate = "Horário inválido!";
        public static readonly string ImpossibleCreate = "Apenas profissionais podem criar horários!";

    }
}
