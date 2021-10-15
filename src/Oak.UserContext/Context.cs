using System;
using System.Collections.Generic;

namespace Oak.UserContext
{
    /// <summary>
    /// Basic IContext Implementation
    /// </summary>
    public class Context : IContext
    {
        public bool Authenticated { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Types.Authentication Type { get; set; }
        public List<string> Permissions { get; set; }
    }
}