using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create ScriptableScoreData")]
[System.Serializable]
public class ScriptableScoreData : ScriptableObject
{
    public ScoreData score = new ScoreData();
}

[System.Serializable]
public class ScoreData
{
    public int valueScore       = 0;
    public int valueMaxCombo    = 0;
    [Space(5)]
    public int valuePerfect     = 0;
    public int valueGreat       = 0;
    public int valueGood        = 0;
    public int valueBad         = 0;
    public int valueMiss        = 0;
}
