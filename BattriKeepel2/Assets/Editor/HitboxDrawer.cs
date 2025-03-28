using UnityEditor;
using UnityEngine;
using Components;

[CustomPropertyDrawer(typeof(Hitbox))]
public class HitboxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty modeProperty = property.FindPropertyRelative("m_type");
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), modeProperty);

        SerializedProperty offSetProperty = property.FindPropertyRelative("m_offSet");
        EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), offSetProperty);

        HitboxType mode = (HitboxType)modeProperty.enumValueIndex;

        if (mode == HitboxType.Circle)
        {
            SerializedProperty sizeProperty = property.FindPropertyRelative("m_size");
            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight), sizeProperty);
        }
        else if (mode == HitboxType.RectangularParallelepiped)
        {
            SerializedProperty dimensionsField = property.FindPropertyRelative("m_dimensions");
            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight), dimensionsField);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty modeProperty = property.FindPropertyRelative("m_type");
        HitboxType mode = (HitboxType)modeProperty.enumValueIndex;

        float height = EditorGUIUtility.singleLineHeight;
        if (mode == HitboxType.Circle || mode == HitboxType.RectangularParallelepiped)
        {
            height += EditorGUIUtility.singleLineHeight;
        }

        return height + EditorGUIUtility.singleLineHeight;
    }
}

