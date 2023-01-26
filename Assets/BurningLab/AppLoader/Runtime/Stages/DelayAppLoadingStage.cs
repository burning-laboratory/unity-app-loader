using System.Threading.Tasks;
using UnityEngine;
using BurningLab.AppLoader.Types;

namespace BurningLab.AppLoader.Stages
{
    [AddTypeMenu("Burning-Lab/Misc/Delay Stage")]
    [System.Serializable]
    public class DelayAppLoadingStage : AppLoadingStage
    {
        #region Settings

        [Tooltip("Delay type in seconds.")]
        [SerializeField] private float _delay;

        #endregion

        #region Private Methods

        /// <summary>
        /// Call next app loading stage with delay.
        /// </summary>
        /// <param name="seconds">Delay in seconds.</param>
        private async void CallNextStageWithDelay(float seconds)
        {
            await Task.Delay((int) (seconds * 1000));
            Next(LoadingStageResult.Success);
        }

        #endregion
        
        #region Stage Base

        public override void Begin()
        {
            base.Begin();
            
            CallNextStageWithDelay(_delay);
        }

        #endregion
    }
}