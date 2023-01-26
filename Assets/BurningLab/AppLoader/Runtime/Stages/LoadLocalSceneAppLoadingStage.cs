using BurningLab.AppLoader.Attributes;
using BurningLab.AppLoader.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BurningLab.AppLoader.Stages
{
    [AddTypeMenu("Burning-Lab/Scenes/Load Local Scene Stage")]
    [System.Serializable]
    public class LoadLocalSceneAppLoadingStage : AppLoadingStage
    {
        #region Internal Types

        /// <summary>
        /// Scene load type.
        /// </summary>
        private enum LoadType
        {
            /// <summary>
            /// Sync load.
            /// </summary>
            Sync = 0,
            
            /// <summary>
            /// Async load.
            /// </summary>
            Async = 1
        }

        #endregion

        #region Settings

        [Tooltip("Target scene to load.")]
        [SerializeField, SceneAssetField] private string _scene;
        
        [Tooltip("Load scene mode.")]
        [SerializeField] private LoadSceneMode _loadMode;
        
        [Tooltip("Load type.")]
        [SerializeField] private LoadType _loadType;

        #endregion

        #region Event Handlers

        /// <summary>
        /// On async scene load event handler.
        /// </summary>
        /// <param name="operation"></param>
        private void OnSceneLoad(AsyncOperation operation)
        {
            Next(LoadingStageResult.Success);
        }


        #endregion

        #region Stage Base

        public override void Begin()
        {
            base.Begin();

            switch (_loadType)
            {
                case LoadType.Sync:
                    SceneManager.LoadScene(_scene, _loadMode);
                    Next(LoadingStageResult.Success);
                    break;
                
                case LoadType.Async:
                    AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(_scene, _loadMode);
                    loadSceneOperation.completed += OnSceneLoad;
                    break;
            }
        }

        #endregion
    }
}