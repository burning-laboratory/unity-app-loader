using BurningLab.AppLoader.Attributes;
using BurningLab.AppLoader.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BurningLab.AppLoader.Stages
{
    [AddTypeMenu("Burning-Lab/Game Object/Instantiate Local Game Object Stage")]
    [System.Serializable]
    public class InstantiateLocalGameObjectAppLoadingStage : AppLoadingStage
    {
        #region Settings

        [Header("Required settings")]
        [Tooltip("Instantiating prefab position.")]
        [SerializeField] private Vector3 _position;

        [Tooltip("Instantiating prefab rotation.")]
        [SerializeField] private Quaternion _rotation;
        
        [Tooltip("Game object to instantiate reference.")]
        [SerializeField] private GameObject _prefab;
        
        [Header("Optional settings")]
        [Tooltip("Prefab parent transform reference. Optional.")]
        [SerializeField] private Transform _prefabParent;
        
        [Tooltip("Target scene to instantiating. Optional.")]
        [SerializeField, SceneAssetField] private string _targetScene;

        #endregion

        #region Stage Base

        public override void Begin()
        {
            base.Begin();

            GameObject gameObject = Object.Instantiate(_prefab);
            
            if (string.IsNullOrEmpty(_targetScene) == false)
            {
                Scene targetScene = SceneManager.GetSceneByPath(_targetScene);
                if (targetScene.isLoaded == false)
                {
                    Next(LoadingStageResult.Error);
                    return;
                }

                SceneManager.MoveGameObjectToScene(gameObject, targetScene);
            }

            if (_prefabParent != null)
            {
                gameObject.transform.SetParent(_prefabParent);
            }

            if (_prefabParent == null)
            {
                gameObject.transform.position = _position;
                gameObject.transform.rotation = _rotation;
            }
            else
            {
                gameObject.transform.localPosition = _position;
                gameObject.transform.localRotation = _rotation;
            }

            Next(LoadingStageResult.Success);
        }

        #endregion
    }
}