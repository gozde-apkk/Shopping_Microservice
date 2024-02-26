using Ms.Web.Model;
using Ms.Web.Models;

namespace Ms.Web.Services.AuthService
{
    public interface IAuthService
    {

        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegisterRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegisterRequestDto registrationRequestDto);
    }
}
