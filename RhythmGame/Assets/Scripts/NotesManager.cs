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
}

public class NotesManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset notesData;

    public int noteNum;
    private string songName;

    public List<int> listLaneNum = new List<int>();
    public List<int> listNoteType = new List<int>();
    public List<float> listNotesTime = new List<float>();
    public List<GameObject> listNotesObj = new List<GameObject>();

    [SerializeField]
    private float notesSpeed;
    [SerializeField]
    GameObject noteObj;

    void OnEnable()
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

        // ノーツ生成＆時間計算
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            // 一小節の長さ
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            // ノーツ間の長さ
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            // ノーツの降ってくる時間
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            
            // リスト追加
            listNotesTime.Add(time);
            listLaneNum.Add(inputJson.notes[i].block);
            listNoteType.Add(inputJson.notes[i].type);

            // ノーツ生成
            float z = listNotesTime[i] * notesSpeed;
            listNotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
        }
    }
}
