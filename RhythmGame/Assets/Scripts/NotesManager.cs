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

    public Material longNoteLineMaterial;
    public Color longNoteLineColor;
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
        GameObject obj = Instantiate(noteTapObj, new Vector3(block - 2.5f, 0.03f, z), noteTapObj.transform.rotation);
        listNotesObj.Add(obj);
    }

    void InstantiateLongNotes(Note note , float time , LineRenderer line = null)
    {
        // 生成
        float z = time * PlaySceneManager.psManager.notesSpeed;
        GameObject obj1 = Instantiate(noteLongObj, new Vector3(note.block - 2.5f, 0.03f, z), noteLongObj.transform.rotation);
        listLongNotesObj.Add(obj1);

        // 線を引く準備(Meshの下頂点)
        Vector3[] lineVerticesVec3 = new Vector3[4];
        Vector3[] upperVec3 = GetObjVerticsUpperLower(obj1,true);
        lineVerticesVec3[0] = upperVec3[0];
        lineVerticesVec3[1] = upperVec3[1];

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
            float z2 = timeLongNote * PlaySceneManager.psManager.notesSpeed;
            GameObject obj2 = Instantiate(noteLongObj, new Vector3(note.notes[x].block - 2.5f, 0.03f, z2), noteLongObj.transform.rotation);
            listLongNotesObj.Add(obj2);

            // 線を引く準備(Meshの上頂点)
            Vector3[] lowerVec3 = GetObjVerticsUpperLower(obj2, false);
            lineVerticesVec3[2] = lowerVec3[0];
            lineVerticesVec3[3] = lowerVec3[1];

            InstantiateLongNotesLine(lineVerticesVec3);

            // 次の線を引く準備(Meshの下頂点)
            Vector3[] upperVec3Obj2 = GetObjVerticsUpperLower(obj2, true);
            lineVerticesVec3[0] = upperVec3Obj2[0];
            lineVerticesVec3[1] = upperVec3Obj2[1];
        }

        // リスト内を降ってくる時間順に整理

    }

    Vector3[] GetObjVerticsUpperLower(GameObject obj,bool upper)
    {
        Transform targetTransform = obj.transform;

        // 各頂点をワールド座標に変換する
        Vector3[] vertices = obj.GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = targetTransform.TransformPoint(vertices[i]);
        }

        // objの上頂点か下頂点を返す
        Vector3[] pos = new Vector3[2];
        if (upper)
        {
            pos[0] = vertices[2];
            pos[1] = vertices[3];
        }
        else
        {
            pos[0] = vertices[0];
            pos[1] = vertices[1];
        }
        return pos;
    }

    void InstantiateLongNotesLine(Vector3[] _lineVerticesVec3)
    {
        // Quadを生成
        GameObject lineObj = GameObject.CreatePrimitive(PrimitiveType.Quad);

        // Meshを作成し、Mesh端4点の頂点情報をセット
        Mesh mesh = new Mesh();
        mesh.SetVertices(_lineVerticesVec3);

        // QuadのMeshは2つの三角形で構成されています
        // 作成する頂点の順番をセット
        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;
        mesh.SetTriangles(triangles, 0);

        // 生成したQuadに各種情報をセット
        lineObj.GetComponent<MeshFilter>().mesh = mesh;
        lineObj.GetComponent<MeshRenderer>().material = longNoteLineMaterial;
        Notes notes = lineObj.AddComponent<Notes>();
        notes.isMoveZ = true;
    }
}
