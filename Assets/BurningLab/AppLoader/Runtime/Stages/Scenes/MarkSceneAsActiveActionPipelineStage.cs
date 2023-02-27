using UnityEngine;
using UnityEngine.SceneManagement;
using BurningLab.ActionsPipeline;
using BurningLab.AppLoader.Attributes;

namespace BurningLab.AppLoader.Stages
{
    /// <summary>
    /// Mark scene as active action pipeline stage.
    /// </summary>
    [AddTypeMenu("Burning-Lab/Scenes/Mark Scene As Active Stage")]
    [System.Serializable]
    public class MarkSceneAsActiveActionPipelineStage : ActionPipelineStage
    {
        #region Settings

        [Tooltip("Scene to mark as active.")]
        [SerializeField, SceneAssetField] private string _scene;

        #endregion

        #region Event Handlers

        /// <summary>
        /// On active scene changed event handler.
        /// </summary>
        /// <param name="previousActiveScene">Previous active scene.</param>
        /// <param name="currentActiveScene">Current active scene.</param>
        private void OnActiveSceneChangedEventHandler(Scene previousActiveScene, Scene currentActiveScene)
        {
            if (currentActiveScene.path == _scene)
            {
                Next(ActionsPipelineStageResult.Success);
            }
        }

        #endregion
        
        protected override void OnInit()
        {
            base.OnInit();
            
            SceneManager.activeSceneChanged += OnActiveSceneChangedEventHandler;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            Scene targetScene = SceneManager.GetSceneByPath(_scene);
            if (targetScene.isLoaded == false)
            {
                Next(ActionsPipelineStageResult.Error);
            }

            SceneManager.SetActiveScene(targetScene);
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            
            SceneManager.activeSceneChanged -= OnActiveSceneChangedEventHandler;
        }
    }
}