using BurningLab.ActionsPipeline;
using UnityEngine;

namespace BurningLab.AppLoader.Stages.Misc
{
    /// <summary>
    /// Wait any key down action pipeline stage.
    /// </summary>
    [AddTypeMenu("Burning-Lab/Misc/Wait Any Key Down Stage")]
    [System.Serializable]
    public class WaitAnyKeyDownActionPipelineStage : ActionPipelineStage
    {
        protected override void OnInit()
        {
            base.OnInit();

            Application.onBeforeRender += OnBeforeRenderEventHandler;
        }

        private void OnBeforeRenderEventHandler()
        {
            if (Input.anyKey)
            {
                Next(ActionsPipelineStageResult.Success);
            }
        }
        
        protected override void OnDeInit()
        {
            base.OnDeInit();
            
            Application.onBeforeRender -= OnBeforeRenderEventHandler;
        }
    }
}