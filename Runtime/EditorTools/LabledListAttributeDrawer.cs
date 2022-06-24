using System;
using UnityEditor;
using UnityEngine;

namespace Goo.EditorTools
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LabledListAttribute))]
    public class LabledListAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            LabledListAttribute attr = (LabledListAttribute)attribute;

            if (Enum.IsDefined(attr.Type, pos))
            {
                label.text = Enum.GetName(attr.Type, pos);
            }
            else
            {
                label.tooltip = "Enum values overflow.";
                label.text = $"[Enum overflow {pos}]";
            }

            EditorGUI.PropertyField(rect, property, label);
        }
    }
#endif
}