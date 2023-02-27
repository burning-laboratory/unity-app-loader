using System;

namespace BurningLab.AppLoader.Types
{
    /// <summary>
    /// Application loading report structure.
    /// </summary>
    public struct ApplicationLoadingReport
    {
        /// <summary>
        /// Application loading result.
        /// </summary>
        public ApplicationLoadingResult result;

        /// <summary>
        /// Application loading time.
        /// </summary>
        public TimeSpan elapsedTime;
    }
}