
using api.Models;

namespace api.Interface
{
    public interface ITokenService
    {
        String CreateToken(AppUser user);
    }
}
