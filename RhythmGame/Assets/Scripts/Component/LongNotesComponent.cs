using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 仕様
 * 生成→InputTouchManagerで判定→Perfect内時にレーンを押した状態ならOK
 * */

public class LongNotesComponent : MonoBehaviour
{
    [SerializeField]
    NotesJudgeController notesJudgeController;

    public Material longNoteLineMaterial;

    [SerializeField]
    GameObject noteLongObj;

    [HideInInspector]
    public List<int> listLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listNoteType = new List<int>();
    //[HideInInspector]
    public List<float> listNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listLongNotesObj = new List<GameObject>();
    private List<GameObject> listLongNotesLineObj = new List<GameObject>();

    void Start()
    {
        InitNotes();
        PlaySceneManager.psManager.inputPlayScene.deligateLongTapJudge += RaneJudge;
    }

    // Update is called once per frame
    void Update()
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        //本来ノーツをたたくべき時間から0.15秒たっても入力がなかった場合
        if (PlaySceneManager.psManager.playStopWatchTime > listNotesTime[0] + notesJudgeController.timeBadMiss)
        {
            notesJudgeController.JudgeMiss(listLaneNum[0]);
            DeleteData();
        }
    }

    void InitNotes()
    {
        string inputString = PlaySceneManager.psManager.notesManager.notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].type == 2)
            {
                // ノーツの判定時間
                float time = PlaySceneManager.psManager.notesManager.GetNoteTime(inputJson.notes[i]);

                // ノーツの降ってくる時間
                float posTime = PlaySceneManager.psManager.notesManager.GetNoteTime(inputJson.notes[i], true);

                // 生成
                InstantiateLongNotes(inputJson.notes[i], time ,posTime);
                break;
            }

        }
    }

    void InstantiateLongNotes(Note _note, float _time, float _posTime, LineRenderer _line = null)
    {
        // リスト追加
        listNotesTime.Add(_time);
        listLaneNum.Add(_note.block);
        listNoteType.Add(_note.type);

        // 生成
        float z = _posTime * PlaySceneManager.psManager.notesSpeed;
        GameObject obj1 = Instantiate(noteLongObj, new Vector3(_note.block - 2.5f, 0.03f, z), noteLongObj.transform.rotation);
        listLongNotesObj.Add(obj1);

        // 線を引く準備(Meshの下頂点)
        Vector3[] lineVerticesVec3 = new Vector3[4];
        Vector3[] upperVec3 = GetObjVerticsUpperLower(obj1, true);
        lineVerticesVec3[0] = upperVec3[0];
        lineVerticesVec3[1] = upperVec3[1];

        // Noteの中のnotes配列から生成
        for (int x = 0; x < _note.notes.Length; x++)
        {
            // ノーツの判定時間
            float timeLongNote = PlaySceneManager.psManager.notesManager.GetNoteTime(_note.notes[x]);
            // ノーツの降ってくる時間
            float postimeLongNote = PlaySceneManager.psManager.notesManager.GetNoteTime(_note.notes[x], true);

            // リスト追加
            listNotesTime.Add(timeLongNote);
            listLaneNum.Add(_note.notes[x].block);
            listNoteType.Add(_note.notes[x].type);

            // 生成
            float z2 = postimeLongNote * PlaySceneManager.psManager.notesSpeed;
            GameObject obj2 = Instantiate(noteLongObj, new Vector3(_note.notes[x].block - 2.5f, 0.03f, z2), noteLongObj.transform.rotation);
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

    Vector3[] GetObjVerticsUpperLower(GameObject obj, bool upper)
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
        notes.isMove = false;

        listLongNotesLineObj.Add(lineObj);
    }

    public void DeleteData()//すでにたたいたノーツ判定を削除する関数
    {
        listNotesTime.RemoveAt(0);
        listLaneNum.RemoveAt(0);
        listNoteType.RemoveAt(0);
    }

    public void RaneJudge(int _hitsRaneNum)
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        if (listLaneNum[0] == _hitsRaneNum)
        {
            // 判定タイプ取得
            NotesJudgeController.EJudgeType eJudgeType;
            eJudgeType = notesJudgeController.Judgement(listNotesTime[0], listLaneNum[0], true);
            
            // パーフェクト時、判定処理後にList要素削除
            if (eJudgeType == NotesJudgeController.EJudgeType.Perfect)
            {
                notesJudgeController.Judgement(listNotesTime[0], listLaneNum[0]);
                DeleteData();
            }
        }
    }

    public void MoveNotes(bool _isMove)
    {
        foreach (GameObject note in listLongNotesObj)
        {
            Notes thisNote = note.GetComponent<Notes>();
            thisNote.isMove = _isMove;
        }

        foreach (GameObject line in listLongNotesLineObj)
        {
            Notes thisNote = line.GetComponent<Notes>();
            thisNote.isMove = _isMove;
        }
    }
}
