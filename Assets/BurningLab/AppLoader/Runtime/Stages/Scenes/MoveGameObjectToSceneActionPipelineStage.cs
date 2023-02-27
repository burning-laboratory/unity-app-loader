using UnityEngine;
using UnityEngine.SceneManagement;
using BurningLab.ActionsPipeline;
using BurningLab.AppLoader.Attributes;

namespace BurningLab.AppLoader.Stages
{
    /// <summary>
    /// Move game object to selected scene.
    /// </summary>
    [AddTypeMenu("Burning-Lab/Scenes/Move Game Object To Scene Stage")]
    [System.Serializable]
    public class MoveGameObjectToSceneActionPipelineStage : ActionPipelineStage
    {
        #region Settings

        [Tooltip("Game object reference to move.")]
        [SerializeField] private GameObject _gameObjectForMove;
        
        [Tooltip("Target scene to game object move.")]
        [SerializeField, SceneAssetField] private string _targetScene;

        #endregion

        #region Virtual Methods

        protected override void OnStart()
        {
            base.OnStart();

            Scene targetScene = SceneManager.GetSceneByPath(_targetScene);
            if (targetScene.isLoaded == false)
            {
                Next(ActionsPipelineStageResult.Error);
            }
            
            SceneManager.MoveGameObjectToScene(_gameObjectForMove, targetScene);

            Next(ActionsPipelineStageResult.Success);
        }

        #endregion
    }
}