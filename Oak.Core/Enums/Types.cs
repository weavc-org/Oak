using System;

namespace Oak.Core
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