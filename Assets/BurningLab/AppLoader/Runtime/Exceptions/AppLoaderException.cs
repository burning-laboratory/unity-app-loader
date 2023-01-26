using System;

namespace BurningLab.AppLoader.Exceptions
{
    /// <summary>
    /// Application loader exception.
    /// </summary>
    public class AppLoaderException : Exception
    {
        public AppLoaderException(string message) : base(message) {}
    }
}