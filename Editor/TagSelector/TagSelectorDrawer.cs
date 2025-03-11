using System;
using UnityEngine;
using UnityEditor; 
using Project.Utility.Runtime.TagSelector;

namespace Project.Utility.Editor
{  
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))] 
    public class TagSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            { 
                string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
                int index = Mathf.Max(0, System.Array.IndexOf(tags, property.stringValue));
 
                index = EditorGUI.Popup(position, label.text, index, tags);
                property.stringValue = tags[index];
            }
            else if (property.propertyType == SerializedPropertyType.Generic && property.isArray) 
            { 
                EditorGUI.LabelField(position, label);
                property.arraySize = EditorGUI.IntField(new Rect(position.x + 100, position.y, 50, position.height), property.arraySize);

                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty element = property.GetArrayElementAtIndex(i);
                    position.y += EditorGUIUtility.singleLineHeight;
                    
                    if (element.propertyType == SerializedPropertyType.String)
                    {
                        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
                        int index = Mathf.Max(0, System.Array.IndexOf(tags, element.stringValue));
                        index = EditorGUI.Popup(position, $"Tag {i + 1}", index, tags);
                        element.stringValue = tags[index];
                    }
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isArray)
            {
                return (property.arraySize + 1) * EditorGUIUtility.singleLineHeight;
            }
            return EditorGUIUtility.singleLineHeight;
        }
    }

}
