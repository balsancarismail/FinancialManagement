namespace Application.Features.Auth.Commands.CreateAuthenticationToken;

public class CreatedAuthenticationTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public DateTime Expires { get; set; }
}