namespace ProjetoFIAP.Api.Domain.Constants
{
    public static class ValidationConstants
    {
        public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\"":{}|<>])[A-Za-z\d!@#$%^&*(),.?\"":{}|<>]{8,}$";
        
        public const string PasswordErrorMessage = "A senha deve conter no m�nimo 8 caracteres, incluindo letras mai�sculas, min�sculas, n�meros e caracteres especiais";
    }
}