using System.Collections.Generic;

namespace Enums
{
    public static class Dictionary
    {
        public static readonly Dictionary<string, string> PlayerNames = new Dictionary<string, string>
            {
                { "AI", "Robot" },
                { "PLAYER", "You" }
            };
    }
}