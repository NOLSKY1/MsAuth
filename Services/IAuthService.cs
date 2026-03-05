using Ms_Auth.Dto;

namespace Ms_Auth.Services
{
    public interface IAuthService
    {
        public AuthResponse Register(RegisterDto dto);
        public AuthResponse Login(LoginDto dto);

    }
}
