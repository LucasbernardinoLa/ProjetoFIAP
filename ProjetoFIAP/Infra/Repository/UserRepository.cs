using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Domain.Entities;
using ProjetoFIAP.Api.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ProjetoFIAP.Api.Infra.Data;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using ProjetoFIAP.Api.Infra.Utils;

namespace ProjetoFIAP.Api.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDTO> RegisterUserAsync(UserRegistrationDTO registration)
        {
            // Validar se email já existe
            if (await _context.Users.AnyAsync(u => u.Email == registration.Email))
                throw new DomainValidationException("Email já cadastrado");

            // Criar usuário com senha hash
            var user = new User(
                registration.Name,
                registration.Email,
                PasswordHasher.HashPassword(registration.Password)
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserResponseDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role.ToString()
                })
                .ToListAsync();
        }

        public async Task<UserResponseDTO?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email.ToLower() && 
                                         u.PasswordHash == hashedPassword);

            if (user == null)
                return null;

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}