using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupMap", menuName = "ScriptableObjects/SetupMap", order = 1)]
public class SetupMapScriptableObject : ScriptableObject
{
    public int collectHero = 2;
    public int enemy = 2;
    public int obstacle1x1 = 1;
    public int obstacle1x2 = 1;
    public int obstacle2x1 = 1;
    public int obstacle2x2 = 1;
}

