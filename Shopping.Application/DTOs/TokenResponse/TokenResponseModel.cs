namespace Shopping.Application.DTOs.TokenResponse;
public class TokenResponseModel
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
    public string UserEmail { get; set; }

}

