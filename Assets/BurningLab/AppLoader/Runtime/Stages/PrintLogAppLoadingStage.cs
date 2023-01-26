using BurningLab.AppLoader.Types;
using UnityEngine;

namespace BurningLab.AppLoader.Stages
{
    [AddTypeMenu("Burning-Lab/Misc/Print Log Stage")]
    [System.Serializable]
    public class PrintLogAppLoadingStage : AppLoadingStage
    {
        #region Settings

        [Tooltip("Message to console printing.")]
        [SerializeField] private string _message;
        
        [Tooltip("Log message type.")]
        [SerializeField] private LogType _logType;
        
        [Tooltip("Logging option.")]
        [SerializeField] private LogOption _logOption;

        #endregion

        #region Stage Base

        public override void Begin()
        {
            base.Begin();
            
            Debug.LogFormat(_logType, _logOption, null, _message);
            Next(LoadingStageResult.Success);
        }

        #endregion
    }
}