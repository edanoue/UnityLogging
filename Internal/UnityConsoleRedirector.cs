#nullable enable

using System;
using System.IO;

namespace Edanoue.Logging.Internal
{
    /// <summary>
    /// Redirects writes to System.Console to Unity3D's Debug.Log.
    /// </summary>
    /// <author>
    /// Jackson Dunstan, http://jacksondunstan.com/articles/2986
    /// </author>
    internal static partial class UnityConsoleRedirector
    {
        private static TextWriter? _stdOutCache = null;

        public static bool IsEnabled => _stdOutCache is not null;

        public static bool Enable()
        {
            if (!IsEnabled)
            {
                _stdOutCache = Console.Out;
                Console.SetOut(new UnityTextWriter());
            }
            return IsEnabled == true;
        }

        public static bool Disable()
        {
            if (IsEnabled)
            {
                _stdOutCache = null;
                Console.SetOut(_stdOutCache);
            }
            return IsEnabled == false;
        }
    }
}