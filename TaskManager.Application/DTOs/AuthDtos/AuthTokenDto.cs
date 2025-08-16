namespace TaskManager.Application.DTOs.AuthDtos
{
    public class AuthTokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
