using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(Wheel))]
public class WheelTest : UnityEditor.Editor
{
    int sectionNum;
    int testNums;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Wheel myWheel = (Wheel)target;

        testNums = (int)UnityEditor.EditorGUILayout.Slider("input test numbers", testNums, 100, 1000000);
        if (GUILayout.Button("Test "+ testNums + " Cases"))
        {
            myWheel.testResults(1000);
        }
        sectionNum = (int)UnityEditor.EditorGUILayout.Slider("input sectionNum", sectionNum, 1, myWheel.wheelData.dataset.Length);
        if (GUILayout.Button("Test sections"))
        {
            myWheel.testSectionResult(sectionNum - 1);
        }
    }
}

#endif
