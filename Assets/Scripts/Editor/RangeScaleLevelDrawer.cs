using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeScaleLevelAttribute))]
public class RangeScaleLevelDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.PropertyField(position, property, label);

            return;
        }

        RangeScaleLevelAttribute rangeAttribute = (RangeScaleLevelAttribute)attribute;

        EditorGUI.IntSlider(position, property, rangeAttribute.Min, rangeAttribute.Max, label);
    }
}
