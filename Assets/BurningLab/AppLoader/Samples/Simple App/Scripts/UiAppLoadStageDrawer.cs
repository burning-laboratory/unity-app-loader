using BurningLab.ActionsPipeline;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BurningLab.AppLoader.Samples.SimpleApp.Scripts
{
    public class UiAppLoadStageDrawer : MonoBehaviour
    {
        [Header("Components")]
        [Tooltip("Application loader component reference.")]
        [SerializeField] private AppLoader _appLoader;
        
        [Tooltip("Current stage name text field.")]
        [SerializeField] private TMP_Text _stageNameText;
        
        [Tooltip("Application loading progress bar reference.")]
        [SerializeField] private Slider _progressBar;

        private void Awake()
        {
            _appLoader.OnAppLoadingPipelineStageBegin += OnApplicationLoadingStageCompleteEventHandler;
            _appLoader.OnAppLoadingPipelineStageEnd += OnApplicationLoadingStageCompleteEventHandler;
        }

        private void OnApplicationLoadingStageCompleteEventHandler(ActionPipelineStage stage)
        {
            _stageNameText.SetText(stage.StageName);
            _progressBar.value = _appLoader.LoadingProgress;
        }

        private void OnDestroy()
        {
            _appLoader.OnAppLoadingPipelineStageBegin -= OnApplicationLoadingStageCompleteEventHandler;
            _appLoader.OnAppLoadingPipelineStageEnd -= OnApplicationLoadingStageCompleteEventHandler;
        }
    }
}