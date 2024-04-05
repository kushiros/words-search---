using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WordsearchGrid_ScriptableObject))]
public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WordsearchGrid_ScriptableObject myScriptableObject = (WordsearchGrid_ScriptableObject)target;

        if (GUILayout.Button("return to white"))
        {
            myScriptableObject.ReturnColorToWhite();
        }
    }
}