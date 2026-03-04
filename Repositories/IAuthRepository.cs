using Ms_Auth.Models;

namespace Ms_Auth.Repositories
{
    public interface IAuthRepository
    {
        public bool PersistUser(User user);
    }
}
