using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (GeneratingLevel))]
public class EditingLevel : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GeneratingLevel TheLevel = target as GeneratingLevel;

        TheLevel.LevelCreation();
    }
}
