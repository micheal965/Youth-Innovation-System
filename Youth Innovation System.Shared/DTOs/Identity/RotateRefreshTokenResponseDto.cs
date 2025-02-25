using System.Text.Json.Serialization;

namespace Youth_Innovation_System.Shared.DTOs.Identity
{
    public class RotateRefreshTokenResponseDto
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        [JsonIgnore]
        public string? refreshToken { get; set; }
        public DateTime refreshTokenExpiration { get; set; }
    }
}
