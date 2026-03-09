using Microsoft.AspNetCore.Identity;
using Ms_Auth.Dto;
using Ms_Auth.Mappers;
using Ms_Auth.Models;
using Ms_Auth.Repositories;
using Ms_Auth.Services.Jwt;

namespace Ms_Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly UserMapper userMapper;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IjwtService jwtService;
        public AuthService(IAuthRepository authRepository , UserMapper userMapper , IPasswordHasher<User> passwordHasher , IjwtService ijwtService)
        {
            this.authRepository = authRepository;
            this.userMapper = userMapper;
            this.passwordHasher = passwordHasher;
            this.jwtService = ijwtService;
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
        public AuthResponse Login(LoginDto dto)
        {
            User user = authRepository.GetUSer(dto.Email);
            if(user == null)
            {
                return null;
            }
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user , user.HashedPassword , dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return new AuthResponse
            {
                Token = jwtService.GenerateToken(user.Id),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
