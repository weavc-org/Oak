using System;
using System.Collections.Generic;
using Oak.Shared;

namespace Oak.UserContext
{
    /// <summary>
    /// Shared security context
    /// </summary>
    public interface IContext
    {
        bool Authenticated { get; }
        Guid Id { get; }
        string Email { get; }
        Types.Authentication Type { get; }
        List<string> Permissions { get; }
    }
}