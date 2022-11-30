using Compass.Data.Data.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.ImpUserRepository;
using Unit_Data.Interface;
using Unit_Data.Models.Models;

namespace Unit_Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public JwtService(IConfiguration configuration, TokenValidationParameters tokenValidationParameters, IUserRepository userRepository)
        {
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _userRepository = userRepository;
        }


        public async Task<Tokens> CreateTokenAsync(AppUser user)
        {
            var roles = await _userRepository.GetRolesByUserAsync(user);


            List<Claim> allRoles = new List<Claim>();
            foreach (var role in roles)
            {
                allRoles.Add(new Claim(ClaimTypes.Role, role));
            }
            List<Claim> my_claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim("Username", user.UserName),
                new Claim("Name", user.Name),
                new Claim("Surname", user.Surname),
                new Claim("Email", user.Email),
                new Claim("EmailConfirm", user.EmailConfirmed.ToString()),
                //new Claim("Telefon", user.PhoneNumber.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };
            my_claims.AddRange(allRoles);


            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["SecurityKey:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(my_claims),
                Expires = DateTime.UtcNow.AddMinutes(5), // 5-10 minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };


            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = GenerateRefreshToken(user, token);
            _userRepository.SetRefreshToken(refreshToken);
            await _userRepository.SaveRefreshTokenAsync(refreshToken);

            Tokens tokens = new Tokens
            {
                refreshToken = refreshToken,
                token = jwtToken
            };


            return tokens;
        }

        public string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public RefreshToken GenerateRefreshToken(AppUser user,SecurityToken jwt)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                IsUsed = false,
                UserId = user.Id,
                JwtId = jwt.Id,
                IsRevoked = false,
            };

            return refreshToken;
        }

        public async Task<ServiceResponse> VerifyTokenAsync(TokenRequestVM tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _tokenValidationParameters.ValidateLifetime = false;
                var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Will get the time stamp in unix time
                var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                // we convert the expiry date from seconds to the date
                var expDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expDate > DateTime.UtcNow)
                {
                    return new ServiceResponse()
                    {
                        Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
                        Success = false
                    };
                }

                // Check the token we got if its saved in the db
                var storedRefreshToken = await _userRepository.CheckRefreshTokenAsync(tokenRequest.RefreshToken);

                //var storedRefreshToken = await _apiDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedRefreshToken == null)
                {
                    return new ServiceResponse()
                    {
                        Errors = new List<string>() { "refresh token doesnt exist" },
                        Success = false
                    };
                }

                // Check the date of the saved token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.Expires)
                {
                    return new ServiceResponse()
                    {
                        Errors = new List<string>() { "Token has expired" },
                        Success = false
                    };
                }

                // check if the refresh token has been used
                if (storedRefreshToken.IsUsed)
                {
                    return new ServiceResponse()
                    {
                        Errors = new List<string>() { "token has been used" },
                        Success = false
                    };
                }

                // Check if the token is revoked
                if (storedRefreshToken.IsRevoked)
                {
                    return new ServiceResponse()
                    {
                        Errors = new List<string>() { "token has been revoked" },
                        Success = false
                    };
                }

                // we are getting here the jwt token id
                var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                // check the id that the recieved token has against the id saved in the db
                if (storedRefreshToken.JwtId != jti)
                {
                    return new ServiceResponse()
                    {
                        Errors = new List<string>() { "the token doenst mateched the saved token" },
                        Success = false
                    };
                }

                storedRefreshToken.IsUsed = true;
                await _userRepository.UpdateRefreshTokenAsync(storedRefreshToken);

                var dbUser = await _userRepository.GetUserByIdAsync(storedRefreshToken.UserId);

                var tokens = new Tokens();
                tokens = await CreateTokenAsync(dbUser);
                return
                    new ServiceResponse()
                    {
                        AccessToken = tokens.token,
                        RefreshToken = tokens.refreshToken.Token,
                        Success = true,
                        Message = "Token successfully updated."
                    };
            }
            catch (Exception ex)
            {
                return
                   new ServiceResponse()
                   {
                       Success = false,
                       Message = ex.Message
                   };
            }

        }
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}
