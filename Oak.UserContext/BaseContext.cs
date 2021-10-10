using System;
using System.Collections.Generic;
using Oak.Core;

namespace Oak.UserContext
{
    /// <summary>
    /// Easy to use base to implement IContext without setting each 
    /// property on the implementation
    /// </summary>
    public abstract class BaseContext : IContext
    {
        protected abstract IContext Context { get; set; }
        public bool Authenticated => this.Context?.Authenticated ?? false;
        public Guid Id => this.Context?.Id ?? default;
        public string Email => this.Context?.Email ?? null;
        public Types.Authentication Type => this.Context?.Type ?? Types.Authentication.Standard;
        public List<string> Permissions => this.Context.Permissions ?? new List<string>();
    }
}