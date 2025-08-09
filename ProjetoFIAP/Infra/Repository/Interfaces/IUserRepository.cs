using ProjetoFIAP.Api.Application.DTOs;

namespace ProjetoFIAP.Api.Infra.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponseDTO> RegisterUserAsync(UserRegistrationDTO registration);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO?> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}