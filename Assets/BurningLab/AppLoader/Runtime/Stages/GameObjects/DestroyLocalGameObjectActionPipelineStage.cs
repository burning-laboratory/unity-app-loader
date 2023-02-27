using BurningLab.ActionsPipeline;
using UnityEngine;

namespace BurningLab.AppLoader.Stages.GameObjects
{
    [AddTypeMenu("Burning-Lab/Game Objects/Destroy Local Game Object Stage")]
    [System.Serializable]
    public class DestroyLocalGameObjectActionPipelineStage : ActionPipelineStage
    {
        #region Settings

        [Tooltip("Game object to destroy.")]
        [SerializeField] private GameObject _gameObject;

        #endregion

        protected override void OnStart()
        {
            base.OnStart();
            
            Object.Destroy(_gameObject);
            
            Next(ActionsPipelineStageResult.Success);
        }
    }
}