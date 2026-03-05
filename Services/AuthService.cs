using Microsoft.AspNetCore.Identity;
using Ms_Auth.Dto;
using Ms_Auth.Mappers;
using Ms_Auth.Models;
using Ms_Auth.Repositories;

namespace Ms_Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly UserMapper userMapper;
        private readonly IPasswordHasher<User> passwordHasher;
        public AuthService(IAuthRepository authRepository , UserMapper userMapper , IPasswordHasher<User> passwordHasher)
        {
            this.authRepository = authRepository;
            this.userMapper = userMapper;
            this.passwordHasher = passwordHasher;

        }
        public AuthResponse Register(RegisterDto dto)
        {
            User user = userMapper.RegisterDtoToUser(dto);
            user.Id = Guid.NewGuid().ToString();
            user.HashedPassword = passwordHasher.HashPassword(user, dto.Password);
            if (!authRepository.PersistUser(user))
            {
                return null;
            }
            return new AuthResponse
            {
                Token = "naoufal.token.random"
            };

        }
    }
}
