namespace ProjetoFIAP.Api.Domain.Constants
{
    public static class ValidationConstants
    {
        public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\"":{}|<>])[A-Za-z\d!@#$%^&*(),.?\"":{}|<>]{8,}$";
        
        public const string PasswordErrorMessage = "A senha deve conter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais";
    }
}