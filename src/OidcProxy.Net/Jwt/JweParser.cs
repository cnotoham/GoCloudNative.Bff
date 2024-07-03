using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using OidcProxy.Net.Cryptography;
using OidcProxy.Net.OpenIdConnect;

namespace OidcProxy.Net.Jwt;

public sealed class JweParser(IEncryptionKey encryptionKey) : JwtParser
{
    public override JwtPayload? ParseJwtPayload(string encryptedAccessToken)
    {
        try
        {
            var accessToken = encryptionKey.Decrypt(encryptedAccessToken);
            return base.ParseJwtPayload(accessToken);
        }
        catch (Exception e)
        {
            throw new AuthenticationException("Authentication failed. Unable to decrypt JWE. Use the " +
                                              "options.ConfigureJweParser(..) method to configure JWE handling" +
                                              $"correctly or implement the '{typeof(ITokenParser)}' class.", e);
        }
    }
}