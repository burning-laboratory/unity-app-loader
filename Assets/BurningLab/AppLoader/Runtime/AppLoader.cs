using System;
using System.Collections.Generic;
using System.Diagnostics;
using BurningLab.AppLoader.Exceptions;
using BurningLab.AppLoader.Stages;
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

        [Header("Settings")]
        [Tooltip("Application loading pipeline.")]
        [SerializeReference, SubclassSelector] private List<IAppLoadingStage> _loadingStages;

        [Tooltip("Mark object as dont destroy on load before start loading process.")]
        [SerializeField] private bool _dontDestroyOnLoad;

        [Tooltip("Destroy app loader game object after end loading process.")]
        [SerializeField] private bool _destroyOnCompleteLoading;
        
        #endregion

        #region Private Fields

        /// <summary>
        /// Application loading queue.
        /// </summary>
        private Queue<IAppLoadingStage> _loadingQueue = new();
        
        /// <summary>
        /// Current loading stage.
        /// </summary>
        private IAppLoadingStage _currentStage;

#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
        private Stopwatch _stopwatch;
#endif
        
        #endregion

        #region Events
        
        /// <summary>
        /// Start app loading process event.
        /// </summary>
        public event Action OnStartAppLoading;
        
        /// <summary>
        /// End app loading process event.
        /// </summary>
        public event Action OnEndAppLoading;

        /// <summary>
        /// Begin app loading stage.
        /// </summary>
        public event Action<IAppLoadingStage> OnBeginAppLoadingStage;
        
        /// <summary>
        /// End app loading stage.
        /// </summary>
        public event Action<IAppLoadingStage> OnEndAppLoadingStage; 

        #endregion
        
        #region Event Handlers

        /// <summary>
        /// On app loading stage complete event handler.
        /// </summary>
        /// <param name="result">Loading stage result enumeration.</param>
        private void OnStageCompleteEventHandler(LoadingStageResult result)
        {
            _currentStage.OnComplete -= OnStageCompleteEventHandler;
            OnEndAppLoadingStage?.Invoke(_currentStage);
            
            switch (result)
            {
                case LoadingStageResult.Success:
                    if (_loadingQueue.Count != 0)
                    {
                        _currentStage = _loadingQueue.Dequeue();
                        _currentStage.OnComplete += OnStageCompleteEventHandler;
                        _currentStage.Begin();   
                        
                        OnBeginAppLoadingStage?.Invoke(_currentStage);
                    }
                    else
                    {
                        OnEndAppLoading?.Invoke();
                        
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
                        UnityConsole.PrintLog("AppLoader", "OnStageCompleteEventHandler", "Application end loading.");
                        
                        _stopwatch.Stop();
                        double appLoadTime = Math.Round(_stopwatch.Elapsed.TotalMilliseconds);
                        UnityConsole.PrintLog("AppLoader", "OnStageCompleteEventHandler", $"App loading finished in: {appLoadTime}ms.");
#endif
                        if (_destroyOnCompleteLoading)
                        {
                            Destroy(gameObject);
                        }
                    }
                    break;
                
                case LoadingStageResult.Error:
                    if (_currentStage is AppLoadingStage stage)
                        throw new AppLoaderException($"Stage {stage.StageName} completing with result: {result}.");   
                    
                    break;
                
                case LoadingStageResult.Skipped:
                    break;
            }
        }

        #endregion
        
        #region Unity Event Methods

        private void Awake()
        {
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            UnityConsole.PrintLog("AppLoader", "Awake", "Application start loading.");
            _stopwatch = Stopwatch.StartNew();
#endif
            
            OnStartAppLoading?.Invoke();

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            foreach (IAppLoadingStage stage in _loadingStages)
                _loadingQueue.Enqueue(stage);
    
            _currentStage = _loadingQueue.Dequeue();
            _currentStage.OnComplete += OnStageCompleteEventHandler;
            _currentStage.Begin();
            
            OnBeginAppLoadingStage?.Invoke(_currentStage);
        }

        private void OnDestroy()
        {
#if DEBUG_APP_LOADER || DEBUG_BURNING_LAB_SDK
            UnityConsole.PrintLog("AppLoader", "OnDestroy", "App loader destroyed.");
#endif
        }

        #endregion
    }
}