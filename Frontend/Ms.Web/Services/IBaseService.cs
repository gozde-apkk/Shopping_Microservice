using Ms.Web.Models;

namespace Ms.Web.Services
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}