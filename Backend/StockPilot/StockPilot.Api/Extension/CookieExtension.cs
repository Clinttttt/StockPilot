using Microsoft.AspNetCore.CookiePolicy;

namespace StockPilot.Api.Extension
{
    public  static class CookieExtension
    {
        private const string AccessTokenCookie = "accessToken";
        private const string RefreshTokenCookie = "refreshToken";
        public static void SetAuthCookies(HttpResponse response, string accessToken, string refreshToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(refreshToken)) {
                response.Cookies.Append(
                    AccessTokenCookie,
                    accessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                    });
                response.Cookies.Append(
                    RefreshTokenCookie,
                    refreshToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                    });
            }
        }
    }
}
