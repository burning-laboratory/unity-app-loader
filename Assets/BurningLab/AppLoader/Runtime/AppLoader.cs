using System;
using System.Diagnostics;
using BurningLab.ActionsPipeline;
using BurningLab.AppLoader.Types;
using BurningLab.AppLoader.Utils;
using UnityEngine;

namespace BurningLab.AppLoader
{
    /// <summary>
    /// Application loading pipeline controller.
    /// </summary>
    public class AppLoader : MonoBehaviour
    {
        #region Settings

        [Header("Loading pipeline")]
        [Tooltip("Application loading pipeline.")]
        [SerializeField] private ActionPipeline _loadingPipeline;

        [Header("Settings")] 
        [Tooltip("Mark app loader game object as dont destroy on load.")]
        [SerializeField] private bool _dontDestroyOnLoad;
        
        [Tooltip("Destroy app loader game object after complete loading pipeline.")]
        [SerializeField] private bool _destroyAfterLoading;

        #endregion
        
        #region Diagnostic

#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
        private Stopwatch _totalAppLoadingStopwatch;
        private Stopwatch _stageStopwatch;
#endif

        #endregion
        
        #region Private Properties

        /// <summary>
        /// Application loading stopwatch.
        /// </summary>
        private Stopwatch _appLoadStopwatch;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Application loading progress value.
        /// </summary>
        /// <returns></returns>
        public float LoadingProgress => _loadingPipeline.Progress;

        #endregion
        
        #region Events
        
        /// <summary>
        /// Start application loading event.
        /// </summary>
        public event Action OnAppLoadingPipelineStart;
        
        /// <summary>
        /// Begin application loading pipeline stage event.
        /// </summary>
        public event Action<ActionPipelineStage> OnAppLoadingPipelineStageBegin;
        
        /// <summary>
        /// End application loading pipeline stage event.
        /// </summary>
        public event Action<ActionPipelineStage> OnAppLoadingPipelineStageEnd;
        
        /// <summary>
        /// Application loading complete event.
        /// </summary>
        public event Action<ApplicationLoadingReport> OnAppLoadingPipelineComplete;


        #endregion

        #region Event Handlers

        private void OnApplicationLoadingPipelineCompleteEventHandler(ActionPipelineResult result)
        {
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            _totalAppLoadingStopwatch.Stop();
            double elapsedMs = Math.Round(_totalAppLoadingStopwatch.Elapsed.TotalMilliseconds);
            UnityConsole.PrintLog("AppLoader", "OnApplicationLoadingPipelineCompleteEventHandler", $"App loading complete in: {elapsedMs}ms.");
#endif
            
            _appLoadStopwatch.Stop();
            TimeSpan loadingElapsed = _appLoadStopwatch.Elapsed;

            ApplicationLoadingReport report = new ApplicationLoadingReport
            {
                elapsedTime = loadingElapsed
            };

            switch (result)
            {
                case ActionPipelineResult.Success:
                    report.result = ApplicationLoadingResult.Success;
                    break;
                
                case ActionPipelineResult.Error:
                    report.result = ApplicationLoadingResult.Error;
                    break;
            }
            
            OnAppLoadingPipelineComplete?.Invoke(report);
            
            if (_destroyAfterLoading)
            {
                Destroy(gameObject);
            }
        }

        private void OnApplicationLoadingStageBeginEventHandler(ActionPipelineStage sender)
        {
            
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            _stageStopwatch = Stopwatch.StartNew();
            UnityConsole.PrintLog("AppLoader", "OnApplicationLoadingStageBeginEventHandler", $"Stage {sender.StageName} begin.");
#endif

            OnAppLoadingPipelineStageBegin?.Invoke(sender);
        }

        private void OnApplicationLoadingPipelineEndEventHandler(ActionPipelineStage sender)
        {
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            _stageStopwatch.Stop();
            double elapsedMs = Math.Round(_stageStopwatch.Elapsed.TotalMilliseconds);
            UnityConsole.PrintLog("AppLoader", "OnApplicationLoadingPipelineEndEventHandler", $"Stage {sender.StageName} completed in {elapsedMs}ms.");
#endif
            OnAppLoadingPipelineStageEnd?.Invoke(sender);
        }

        #endregion

        #region Unity Event Methods

        private void Start()
        {
            
            _appLoadStopwatch = Stopwatch.StartNew();
            
#if DEBUG_APP_LOADER ||DEBUG_BURNING_LAB_SDK
            _totalAppLoadingStopwatch = Stopwatch.StartNew();
            UnityConsole.PrintLog("AppLoader", "Start", "Start loading application.");
#endif
            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            _loadingPipeline.OnPipelineComplete += OnApplicationLoadingPipelineCompleteEventHandler;
            _loadingPipeline.OnPipelineStageStart += OnApplicationLoadingStageBeginEventHandler;
            _loadingPipeline.OnPipelineStageEnd += OnApplicationLoadingPipelineEndEventHandler;
            
            _loadingPipeline.RunPipeline();
        }

        #endregion
    }
}