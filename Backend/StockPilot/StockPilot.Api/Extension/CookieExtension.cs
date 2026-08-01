using Microsoft.AspNetCore.CookiePolicy;
using StockPilot.Domain.Common;

namespace StockPilot.Api.Extension
{
    public static class CookieExtension
    {
        private const string AccessTokenCookie = "accessToken";
        private const string RefreshTokenCookie = "refreshToken";
        public static void SetAuthCookies(HttpResponse response, string accessToken, string refreshToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(refreshToken))
            {
                response.Cookies.Append(
                    AccessTokenCookie,
                    accessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                        Path = "/"
                    });
                response.Cookies.Append(
                    RefreshTokenCookie,
                    refreshToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                        Path = "/"
                    });
            }
        }
        public static async Task ClearCookies(HttpResponse response)
        {
            response.Cookies.Delete(AccessTokenCookie);
            response.Cookies.Delete(RefreshTokenCookie);
        }

        public static Result<string> GetRefreshTokenFromCookie(HttpRequest request)
        {
            if (!request.Cookies.TryGetValue(RefreshTokenCookie, out var token))
            {
                return Result<string>.Unauthorized();
            }
            return Result<string>.Success(token);

        }
    }
}
