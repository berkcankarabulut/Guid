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
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                {
                    string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
                    int index = Mathf.Max(0, System.Array.IndexOf(tags, property.stringValue));
         
                    index = EditorGUI.Popup(position, label.text, index, tags);
                    property.stringValue = tags[index];
                    break;
                }
                case SerializedPropertyType.Generic when property.isArray:
                {
                    EditorGUI.LabelField(position, label);
                 
                    float lineHeight = EditorGUIUtility.singleLineHeight;
                    float labelWidth = EditorGUIUtility.labelWidth;
                 
                    Rect sizeRect = new Rect(position.x + labelWidth + 5, position.y, 40, lineHeight);
                    property.arraySize = EditorGUI.IntField(sizeRect, property.arraySize);
 
                    for (int i = 0; i < property.arraySize; i++)
                    {
                        SerializedProperty element = property.GetArrayElementAtIndex(i); 
                        Rect elementRect = new Rect(position.x, position.y + (i + 1) * lineHeight, position.width, lineHeight);
                    
                        if (element.propertyType == SerializedPropertyType.String)
                        {
                            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
                            int index = Mathf.Max(0, System.Array.IndexOf(tags, element.stringValue)); 
                            index = EditorGUI.Popup(elementRect, $"Tag {i + 1}", index, tags);
                            element.stringValue = tags[index];
                        }
                    }

                    break;
                }
                default:
                    EditorGUI.PropertyField(position, property, label);
                    break;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float lineHeight = EditorGUIUtility.singleLineHeight;
            
            if (property.isArray)
            {
                // Başlık satırı + her eleman için bir satır (+ biraz boşluk)
                return lineHeight + (property.arraySize * lineHeight) + 2;
            }
            
            return lineHeight;
        }
    }

}
