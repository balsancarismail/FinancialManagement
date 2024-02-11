namespace Application.Features.Auth.Queries.GetAuthenticationTokenByRefreshToken;

public class GetAuthenticationTokenByRefreshTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public DateTime Expires { get; set; }
}