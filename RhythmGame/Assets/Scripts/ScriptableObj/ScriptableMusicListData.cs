using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create ScriptableMusicListData")]
[System.Serializable]
public class ScriptableMusicListData : ScriptableObject
{
    public List<MusicDataParam> musicDataList = new List<MusicDataParam>();
}
