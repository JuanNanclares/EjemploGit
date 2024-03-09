using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace OralData.Frontend.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);

            var anonimous = new ClaimsIdentity();
            var user = new ClaimsIdentity(authenticationType: "test");
            var adminUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Oral"),
                new Claim("LastName", "Data"),
                new Claim(ClaimTypes.Name, "oraldata@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");

            var otherUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Karen"),
                new Claim("LastName", "Betancur"),
                new Claim(ClaimTypes.Name, "kbetancur@yopmail.com"),
                new Claim(ClaimTypes.Role, "User")
            },
            authenticationType: "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(adminUser)));
        }
    }
}
