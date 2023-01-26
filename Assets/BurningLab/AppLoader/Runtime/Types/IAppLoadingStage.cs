using System;

namespace BurningLab.AppLoader.Types
{
    /// <summary>
    /// App loading stage interface.
    /// </summary>
    public interface IAppLoadingStage
    {
        /// <summary>
        /// Begin load stage.
        /// </summary>
        public void Begin();
        
        /// <summary>
        /// On stage loading complete event.
        /// </summary>
        public event Action<LoadingStageResult> OnComplete;
    }
}