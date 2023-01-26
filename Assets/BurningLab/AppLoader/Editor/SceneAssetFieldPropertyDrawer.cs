using System;
using BurningLab.AppLoader.Attributes;
using UnityEditor;
using UnityEngine;

namespace BurningLab.AppLoader.Editor
{
    [CustomPropertyDrawer(typeof(SceneAssetField))]
    public class SceneAssetFieldPropertyDrawer : PropertyDrawer
    {
        private SceneAsset _sceneAsset;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (string.IsNullOrEmpty(property.stringValue) == false)
            {
                _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);
            }

            _sceneAsset = EditorGUI.ObjectField(position, label, _sceneAsset, typeof(SceneAsset), true) as SceneAsset;

            if (_sceneAsset != null)
            {
                property.stringValue = AssetDatabase.GetAssetPath(_sceneAsset);
            }
            else
            {
                property.stringValue = String.Empty;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}