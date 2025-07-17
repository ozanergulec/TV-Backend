using System.Threading.Tasks;

namespace TV_Backend.Services
{
    public interface ISanTsgTokenService
    {
        Task<string> GetTokenAsync();
    }
} 