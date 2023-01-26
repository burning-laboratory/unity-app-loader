namespace BurningLab.AppLoader.Types
{
    /// <summary>
    /// Application loading stage result enumeration.
    /// </summary>
    [System.Serializable]
    public enum LoadingStageResult
    {
        /// <summary>
        /// Stage success complete.
        /// </summary>
        Success = 0,
        
        /// <summary>
        /// Stage complete with error.
        /// </summary>
        Error = 1,
        
        /// <summary>
        /// Stage skipped.
        /// </summary>
        Skipped = 2
    }
}