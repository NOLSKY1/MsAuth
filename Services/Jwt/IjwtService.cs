namespace Ms_Auth.Services.Jwt
{
    public interface IjwtService
    {
        public string GenerateToken(string userId);
    }
}
