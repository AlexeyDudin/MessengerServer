using Domain.Models;

namespace Infrastructure.Providers
{
    public interface IJwtTokenProvider
    {
        string GenerateToken(User user);
    }
}