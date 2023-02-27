using BurningLab.AppLoader.Types;
using TMPro;
using UnityEngine;

namespace BurningLab.AppLoader.Samples.Simple_App.Scripts
{
    public class UiAppLoadingTimeDrawer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AppLoader _appLoader;
        [SerializeField] private TMP_Text _appLoadingResultTitle;
        
        [Header("Settings")]
        [SerializeField] private string _elapsedMsMarker;
        [SerializeField] private string _loadingResultMarker;
        [SerializeField, TextArea] private string _text; 
        
        private void Awake()
        {
            _appLoader ??= FindObjectOfType<AppLoader>();
            
            _appLoader.OnAppLoadingPipelineComplete += OnAppLoadingComplete;
        }

        private void OnAppLoadingComplete(ApplicationLoadingReport report)
        {
            int elapsedMs = Mathf.RoundToInt((float) report.elapsedTime.TotalMilliseconds);
            string textWithElapsedTime = _text.Replace(_elapsedMsMarker, elapsedMs.ToString());
            string textWithResultStatus = textWithElapsedTime.Replace(_loadingResultMarker, report.result.ToString().ToLower());
            _appLoadingResultTitle.SetText(textWithResultStatus);
        }

        private void OnDestroy()
        {
            _appLoader.OnAppLoadingPipelineComplete -= OnAppLoadingComplete;
        }
    }
}