using System.Security.Claims;

namespace TaskManager.Api.Utils
{
    public static class ClaimExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var sub = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub");
            return Guid.Parse(sub ?? throw new Exception("no sub claim"));
        }
    }
}
