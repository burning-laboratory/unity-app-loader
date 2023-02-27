using System;
using BurningLab.ActionsPipeline;
using BurningLab.AppLoader.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace BurningLab.AppLoader.Stages.GameObjects
{
    [AddTypeMenu("Burning-Lab/Game Objects/Instantiate Game Object Stage")]
    [Serializable]
    public class InstantiateLocalGameObjectActionPipelineStage : ActionPipelineStage
    {
        #region Settings

        [Header("Required fields")]
        [Tooltip("Game object prefab to instantiate.")]
        [SerializeField] private GameObject _prefab;
        
        [Header("Optional fields")]
        [Tooltip("Optional. Target scene to instantiate game object,")]
        [SerializeField, SceneAssetField] private string _targetScene;
        
        [Tooltip("Optional. Game object parent.")]
        [SerializeField] private Transform _parent;
        
        [Tooltip("Optional. ")]
        [SerializeField] private Vector3 _position;
        [SerializeField] private Quaternion _rotation;

        #endregion

        protected override void OnStart()
        {
            base.OnStart();

            GameObject prefab = Object.Instantiate(_prefab, _parent);

            if (_parent != null)
            {
                prefab.transform.localPosition = _position;
                prefab.transform.localRotation = _rotation;
            }
            else
            {
                prefab.transform.position = _position;
                prefab.transform.rotation = _rotation;
            }

            if (String.IsNullOrEmpty(_targetScene) == false)
            {
                Scene targetScene = SceneManager.GetSceneByPath(_targetScene);
                SceneManager.MoveGameObjectToScene(prefab,targetScene);
            }
            
            Next(ActionsPipelineStageResult.Success);
        }
    }
}