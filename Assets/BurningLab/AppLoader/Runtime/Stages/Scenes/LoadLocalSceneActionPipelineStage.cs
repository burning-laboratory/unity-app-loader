using UnityEngine;
using UnityEngine.SceneManagement;
using BurningLab.ActionsPipeline;
using BurningLab.AppLoader.Attributes;

namespace BurningLab.AppLoader.Stages
{
    /// <summary>
    /// Load local scene action pipeline stage.
    /// </summary>
    [AddTypeMenu("Burning-Lab/Scenes/Load Local Scene Stage")]
    [System.Serializable]
    public class LoadLocalSceneActionPipelineStage : ActionPipelineStage
    {
        #region Internal Types

        /// <summary>
        /// Scene asset load type.
        /// Sync or async.
        /// </summary>
        private enum LoadType
        {
            /// <summary>
            /// Sync scene load.
            /// </summary>
            Sync = 0,
            
            /// <summary>
            /// Async scene load.
            /// </summary>
            Async = 1
        }

        #endregion

        #region Settings

        [Tooltip("Scene to load.")]
        [SerializeField, SceneAssetField] private string _scene;
        
        [Tooltip("Load scene mode. Single or additive.")]
        [SerializeField] private LoadSceneMode _loadSceneMode;
        
        [Tooltip("Scene asset load mode. Sync or async.")]
        [SerializeField] private LoadType _assetLoadMode;

        #endregion

        #region Event Handlers

        /// <summary>
        /// On scene loaded event handler.
        /// </summary>
        /// <param name="scene">Loaded scene.</param>
        /// <param name="loadMode">Scene load mode.</param>
        private void OnSceneLoadedEventHandler(Scene scene, LoadSceneMode loadMode)
        {
            if (scene.path == _scene)
            {
                Next(ActionsPipelineStageResult.Success);
            }
        }

        #endregion

        #region Virtual Methods

        protected override void OnInit()
        {
            base.OnInit();
            
            SceneManager.sceneLoaded += OnSceneLoadedEventHandler;
        }

        protected override void OnStart()
        {
            base.OnStart();

            switch (_assetLoadMode)
            {
                case LoadType.Sync:
                    SceneManager.LoadScene(_scene, _loadSceneMode);
                    break;
                
                case LoadType.Async:
                    SceneManager.LoadSceneAsync(_scene, _loadSceneMode);
                    break;
            }
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            
            SceneManager.sceneLoaded -= OnSceneLoadedEventHandler;
        }

        #endregion
    }
}