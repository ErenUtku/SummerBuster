using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RingData", menuName = "ScriptableObjects/RingDataScriptableObject", order = 1)]
public class RingData : ScriptableObject
{   
    public COLOR color;
}

public enum COLOR { YELLOW, PINK , COLORFUL};

