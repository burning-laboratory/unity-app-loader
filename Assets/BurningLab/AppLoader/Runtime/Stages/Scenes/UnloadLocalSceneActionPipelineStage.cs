using UnityEngine;
using UnityEngine.SceneManagement;
using BurningLab.ActionsPipeline;
using BurningLab.AppLoader.Attributes;

namespace BurningLab.AppLoader.Stages
{
    /// <summary>
    /// Unload selected scene action pipeline stage.
    /// </summary>
    [AddTypeMenu("Burning-Lab/Scenes/Unload Local Scene Stage")]
    [System.Serializable]
    public class UnloadLocalSceneActionPipelineStage : ActionPipelineStage
    {
        #region Settings

        [Tooltip("Scene to unload.")]
        [SerializeField, SceneAssetField] private string _scene;

        #endregion

        #region Event Handlers

        /// <summary>
        /// On scene unloaded event handler.
        /// </summary>
        /// <param name="unloadedScene">Unloaded scene.</param>
        private void OnSceneUnloadedEventHandler(Scene unloadedScene)
        {
            if (unloadedScene.path == _scene)
            {
                Next(ActionsPipelineStageResult.Success);
            }
        }

        #endregion

        #region Virtual Methods

        protected override void OnInit()
        {
            base.OnInit();
            
            SceneManager.sceneUnloaded += OnSceneUnloadedEventHandler;
        }

        protected override void OnStart()
        {
            base.OnStart();

            SceneManager.UnloadSceneAsync(_scene);
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            
            SceneManager.sceneUnloaded -= OnSceneUnloadedEventHandler;
        }

        #endregion
    }
}