using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Oak.Core;

namespace Oak.UserContext
{
    /// <summary>
    /// IContext implementation from HTTP Requests
    /// </summary>
    public class HttpUserContext : BaseContext, IContext
    {
        private readonly ClaimsPrincipal httpContext;

        public HttpUserContext(ClaimsPrincipal context)
        {
            this.httpContext = context;
            this.populate();
        }

        public List<Claim> Claims { get; private set; }
        protected override IContext Context { get; set; }

        private void populate()
        {
            var context = new Context();
            context.Authenticated = this.httpContext.Identity.IsAuthenticated;
            if (context.Authenticated)
            {
                var claims = this.httpContext.Claims.ToList();

                var authid = claims.FirstOrDefault(c => c.Type == JWT.ClaimTypes.ID);
                context.Id = new Guid(authid.Value);

                var email = claims.FirstOrDefault(c => c.Type == JWT.ClaimTypes.Email);
                context.Email = email.Value;

                var permissions = claims.Where(c => c.Type == JWT.ClaimTypes.Permissions);
                context.Permissions = permissions.Select(c => c.Value).ToList();

                var type = claims.FirstOrDefault(c => c.Type == JWT.ClaimTypes.Type);
                context.Type = (Types.Authentication)Enum.Parse(typeof(Types.Authentication), type.Value);
                this.Claims = claims;
            }
            else
            {
                context.Authenticated = false;
            }

            this.Context = context;
        }
    }
}