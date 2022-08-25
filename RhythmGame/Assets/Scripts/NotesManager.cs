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
    [SerializeField]
    GameObject noteTapObj;
    [SerializeField]
    GameObject noteLongObj;

    [SerializeField]
    private TextAsset notesData;

    [HideInInspector]
    public int noteNum;
    private string songName;
    private int bpm;
    private int offset;

    [HideInInspector]
    public List<int> listLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listNoteType = new List<int>();
    //[HideInInspector]
    public List<float> listNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listNotesObj = new List<GameObject>();
    [HideInInspector]
    public List<int> listLongLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listLongNoteType = new List<int>();
    //[HideInInspector]
    public List<float> listLongNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listLongNotesObj = new List<GameObject>();

    void Start()
    {
        noteNum = 0;
        songName = "テスト";
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // 総ノーツ数設定
        noteNum = inputJson.notes.Length;

        bpm = inputJson.BPM;
        offset = inputJson.offset;

        // 時間計算＆List追加＆生成関数
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            switch (inputJson.notes[i].type)
            {
                case 1: // タップノーツ
                    // ノーツの降ってくる時間
                    float time = GetNoteTime(inputJson.notes[i]);

                    // リスト追加
                    listNotesTime.Add(time);
                    listLaneNum.Add(inputJson.notes[i].block);
                    listNoteType.Add(inputJson.notes[i].type);

                    // 生成
                    InstantiateTapNotes(time, inputJson.notes[i].block);
                    break;

                case 2: // ロングノーツ
                    // ノーツの降ってくる時間
                    float timeLongNote = GetNoteTime(inputJson.notes[i]);

                    // リスト追加
                    listLongNotesTime.Add(timeLongNote);
                    listLongLaneNum.Add(inputJson.notes[i].block);
                    listLongNoteType.Add(inputJson.notes[i].type);

                    // 生成
                    InstantiateLongNotes(inputJson.notes[i], timeLongNote);
                    break;
            }
            
        }
    }

    float GetNoteTime(Note note)
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

    void InstantiateTapNotes(float time, int block)
    {
        float z = time * PlaySceneManager.psManager.notesSpeed;
        listNotesObj.Add(Instantiate(noteTapObj, new Vector3(block - 2.5f, 0.03f, z), noteTapObj.transform.rotation));
    }

    void InstantiateLongNotes(Note note , float time)
    {
        float z = time * PlaySceneManager.psManager.notesSpeed;
        listLongNotesObj.Add(Instantiate(noteLongObj, new Vector3(note.block - 2.5f, 0.03f, z), noteLongObj.transform.rotation));

        // Noteの中のnotes配列から生成
        for (int x = 0; x < note.notes.Length; x++)
        {
            // ノーツの降ってくる時間
            float timeLongNote = GetNoteTime(note.notes[x]);

            // リスト追加
            listLongNotesTime.Add(timeLongNote);
            listLongLaneNum.Add(note.notes[x].block);
            listLongNoteType.Add(note.notes[x].type);

            // 生成
            InstantiateLongNotes(note.notes[x], timeLongNote);
        }

        // LineRendererで繋ぐ

        // リスト内を降ってくる時間順に整理

    }
}
