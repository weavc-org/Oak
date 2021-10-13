using System;

namespace Oak.Shared
{
    public static class Types
    {
        public enum Authentication
        {
            Standard,
            Provisional,
            Microsoft,
            Github,
            Google,
        }
    }
}