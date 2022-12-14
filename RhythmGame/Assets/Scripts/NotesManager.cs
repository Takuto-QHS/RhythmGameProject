using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;

}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public Note[] notes;
}

public class NotesManager : MonoBehaviour
{
    public TapNotesComponent tapNotesComp;
    public LongNotesComponent longNotesComp;

    public TextAsset notesData;

    [HideInInspector]
    public int noteNum;
    private int bpm;
    private int offset;

    void Awake()
    {
        noteNum = 0;
        Load();
    }

    void Load()
    {
        string inputString = notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // 総ノーツ数設定
        noteNum = inputJson.notes.Length;

        bpm = inputJson.BPM;
        offset = inputJson.offset;

        // 時間計算＆List追加＆生成関数
        
    }

    public float GetNoteTime(Note note)
    {
        // 一小節の長さ
        float kankaku = 60 / (bpm * (float)note.LPB);
        // ノーツ間の長さ
        float beatSec = kankaku * (float)note.LPB;
        // 開始前の長さ
        float preStartTime = offset * 0.01f + PlaySceneManager.psManager.preStartTime;
        // ノーツの降ってくる時間
        float time = (beatSec * note.num / (float)note.LPB) + preStartTime;

        return time;
    }

    
}
