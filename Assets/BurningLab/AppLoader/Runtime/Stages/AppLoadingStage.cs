using System;
using BurningLab.AppLoader.Types;
using UnityEngine;

#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
using System.Diagnostics;
using BurningLab.AppLoader.Utils;
#endif

namespace BurningLab.AppLoader.Stages
{
    /// <summary>
    /// Base app loading stage.
    /// </summary>
    [Serializable]
    public abstract class AppLoadingStage : IAppLoadingStage
    {
        #region Settings

        [Tooltip("Loading stage name.")]
        [SerializeField] private string _stageName;

        #endregion
        
        #region Private Fields

#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
        private Stopwatch _stopWatch;
#endif

        #endregion
        
        #region Puplic Properties

        /// <summary>
        /// Loading stage name.
        /// </summary>
        public string StageName => _stageName;

        #endregion

        #region Stage Base

        /// <summary>
        /// Begin loading stage.
        /// </summary>
        public virtual void Begin()
        {
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            UnityConsole.PrintLog("AppLoadingStage", "Begin", $"Begin loading stage: {_stageName}");
#endif
        }
        
        /// <summary>
        /// Loading stage complete event.
        /// </summary>
        public event Action<LoadingStageResult> OnComplete;

        /// <summary>
        /// Call next app loading stage.
        /// </summary>
        /// <param name="result">Loading stage result.</param>
        protected void Next(LoadingStageResult result)
        {
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            _stopWatch.Stop();
            double elapsedMilliseconds = Math.Round(_stopWatch.Elapsed.TotalMilliseconds);
            UnityConsole.PrintLog("AppLoadingStage", "End", $"Stage: {_stageName} loaded in: {elapsedMilliseconds}ms.");
#endif
            OnComplete?.Invoke(result);
        }

        #endregion
    }
}