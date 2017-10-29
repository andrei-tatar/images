using System;
using System.Configuration;
using System.Security.Claims;
using Facebook;
using Microsoft.Owin.Security;

namespace Backend
{
    public class FacebookTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public string Protect(AuthenticationTicket data)
        {
            throw new System.NotImplementedException();
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(protectedText))
                    return null;

                var fb = new FacebookClient(protectedText);
                var app = fb.Get<JsonObject>("/app");
                if (app["id"].ToString() != ConfigurationManager.AppSettings.Get("fb:appid"))
                    return null;

                var result = fb.Get<JsonObject>("/me");
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, result["id"].ToString(), null, "Facebook")
                }, "Bearer");

                return new AuthenticationTicket(identity, new AuthenticationProperties());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}