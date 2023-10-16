using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create ScriptableMusicData")]
[System.Serializable]
public class ScriptableMusicData : ScriptableObject
{
    public MusicDataParam musicDataParam = new MusicDataParam();
}

[System.Serializable]
public class MusicDataParam
{
    public Sprite spriteLv;
    public Sprite spriteMusic;
    public MusicData musicData;
    public TextAsset notesDataSource;
}
