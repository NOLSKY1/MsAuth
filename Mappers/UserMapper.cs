using Ms_Auth.Dto;
using Ms_Auth.Models;

namespace Ms_Auth.Mappers
{
    public class UserMapper
    {
        public User RegisterDtoToUser(RegisterDto dto)
        {
            return new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
            };
        }
    }
}
