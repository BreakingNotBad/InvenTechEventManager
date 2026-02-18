using Service.Contracts.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.IService
{
    public interface IAuthService
    {
        Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password);
        Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
        Task SetPasswordAsync(SetPasswordDto dto);
        Task ForgotPasswordAsync(ForgotPasswordDto dto);
        Task ChangePasswordAsync(int staffId, ChangePasswordDto dto);


    }
}
