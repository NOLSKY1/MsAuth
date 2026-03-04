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
        public AuthService(IAuthRepository authRepository , UserMapper userMapper)
        {
            this.authRepository = authRepository;
            this.userMapper = userMapper;
        }
        public AuthResponse Register(RegisterDto dto)
        {
            User user = userMapper.RegisterDtoToUser(dto);
            user.Id = Guid.NewGuid().ToString();
            if (!@authRepository.PersistUser(user))
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
