using Ms.Services.AuthAPI.Models.Dto;

namespace Ms.Services.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<string> Register(RegisterRequestDto registerRequestDto);
        
    }
}