using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace VDT.TransactionDaily.API.JWT
{
    public static class JwtHelpers
    {
        /// <summary>
        /// Lấy thông tin claims
        /// </summary>
        /// <param name="userAccounts"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaimDetails(this UserTokens userAccounts, DateTime expires)
        {
            IEnumerable<Claim> claims = new Claim[] {
                new Claim("Id", userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.UserName),
                    new Claim(ClaimTypes.Email, userAccounts.Email),
                    new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Expiration, expires.ToString("MMM ddd dd yyyy HH:mm:ss tt")),
                    new Claim(ClaimTypes.Expired, expires.ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }

        /// <summary>
        /// Lấy thông tin claims
        /// </summary>
        /// <param name="userAccounts"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, DateTime expires)
        {
            return GetClaimDetails(userAccounts, expires);
        }

        /// <summary>
        /// Lấy refresh token
        /// </summary>
        /// <returns></returns>
        public static string GenRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Lấy access token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static UserTokens GenAccessToken(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();

                if (model == null)
                    throw new ArgumentException(nameof(model));

                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                var Id = Guid.Empty;
                var expireTimeAccessToken = DateTime.UtcNow.AddSeconds(jwtSettings.ExpireAccessToken);
                var expireTimeRefreshToken = DateTime.UtcNow.AddSeconds(jwtSettings.ExpireRefreshToken);
                var _jwtoken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, expireTimeAccessToken),
                    expires: expireTimeAccessToken,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));

                userToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(_jwtoken);
                userToken.RefreshToken = GenRefreshToken();
                userToken.Validaty = expireTimeAccessToken.TimeOfDay;
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.Email = model.Email;
                userToken.ExpiredTime = expireTimeAccessToken;
                userToken.RefreshExpiredTime = expireTimeRefreshToken;

                return userToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validate access token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public static bool GetPrincipalFromExpiredToken(string token, JwtSettings jwtSettings)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    RequireExpirationTime = jwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.Zero,
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Đọc thông số access token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// created by vdthang 21.01.2022
        public static JwtSecurityToken ReadToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(accessToken);
            return jsonToken;
        }
    }
}
