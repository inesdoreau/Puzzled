using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Amplitude))]
public class AmplitudeDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var xLabel = new Rect(position.x, position.y, 15, position.height);
        var x = new Rect(xLabel.xMax, position.y, 55, position.height);

        var yLabel = new Rect(x.xMax + 10, position.y, 15, position.height);
        var y = new Rect(yLabel.xMax, position.y, 55, position.height);

        var zLabel = new Rect(y.xMax + 10, position.y, 15, position.height);
        var z = new Rect(zLabel.xMax, position.y, 55, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.LabelField(xLabel, new GUIContent("X"));
        EditorGUI.PropertyField(x, property.FindPropertyRelative("X"), GUIContent.none);

        EditorGUI.LabelField(yLabel, new GUIContent("Y"));
        EditorGUI.PropertyField(y, property.FindPropertyRelative("Y"), GUIContent.none);

        EditorGUI.LabelField(zLabel, new GUIContent("Z"));
        EditorGUI.PropertyField(z, property.FindPropertyRelative("Z"), GUIContent.none);


        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
