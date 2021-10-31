using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            this.Populate();
        }

        public List<Claim> Claims { get; private set; }
        protected override IContext Context { get; set; }

        private void Populate()
        {
            var context = new Context();
            context.Authenticated = this.httpContext.Identity.IsAuthenticated;
            if (context.Authenticated)
            {
                var claims = this.httpContext.Claims.ToList();

                var authId = claims.FirstOrDefault(c => c.Type == Constants.ClaimTypes.ID);
                context.Id = new Guid(authId.Value);

                var email = claims.FirstOrDefault(c => c.Type == Constants.ClaimTypes.Email);
                context.Email = email.Value;

                var permissions = claims.Where(c => c.Type == Constants.ClaimTypes.Permissions);
                context.Permissions = permissions.Select(c => c.Value).ToList();

                var type = claims.FirstOrDefault(c => c.Type == Constants.ClaimTypes.Type);
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