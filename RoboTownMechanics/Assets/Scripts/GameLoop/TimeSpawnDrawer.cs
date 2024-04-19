using TimerHandler;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(GameTimer))]
public class TimeSpawnDrawer : PropertyDrawer
{
    /// <summary>
    /// This is a function which makes the minutes and seconds in the inspector look cleaner
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty minutesProp = property.FindPropertyRelative("_minutes");
        SerializedProperty secondsProp = property.FindPropertyRelative("_seconds");

        EditorGUI.LabelField(position, label);
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, minutesProp);
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField (position, secondsProp);

        EditorGUI.EndProperty();
 
    }
}
