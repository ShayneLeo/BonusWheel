using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelData", menuName = "New WheelData", order = 1)]
public class WheelData : ScriptableObject
{
    public ResData[] dataset;
}


[System.Serializable]
public struct ResData
{
    public int Id;
    public List<string> Cols;
}

