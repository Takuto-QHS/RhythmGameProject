using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapNotesComponent : MonoBehaviour
{
    [SerializeField]
    NotesJudgeController notesJudgeController;

    [SerializeField]
    GameObject noteTapObj;

    [HideInInspector]
    public List<int> listLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listNoteType = new List<int>();
    [HideInInspector]
    public List<float> listNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listNotesObj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitNotes();
        InputTouchManager.deligateJudge += RaneJudge;
    }

    // Update is called once per frame
    void Update()
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        if (Time.time > listNotesTime[0] + notesJudgeController.timeBadMiss)//本来ノーツをたたくべき時間から0.15秒たっても入力がなかった場合
        {
            notesJudgeController.JudgeBadMiss();
        }
    }

    void InitNotes()
    {
        string inputString = PlaySceneManager.psManager.notesManager.notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // 時間計算＆List追加＆生成関数
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].type == 1)
            {
                float time = PlaySceneManager.psManager.notesManager.GetNoteTime(inputJson.notes[i]); // ノーツの降ってくる時間
                InstantiateNote(time, inputJson.notes[i].block, inputJson.notes[i].type);
            }
        }
    }

    public void InstantiateNote(float _time,int _block,int _type)
    {
        // リスト追加
        listNotesTime.Add(_time);
        listLaneNum.Add(_block);
        listNoteType.Add(_type);

        // 生成
        float z = _time * PlaySceneManager.psManager.notesSpeed;
        GameObject obj = Instantiate(noteTapObj, new Vector3(_block - 2.5f, 0.03f, z), noteTapObj.transform.rotation);
        listNotesObj.Add(obj);
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
        //Debug.Log(notesManager.listNotesTime.Count);

        if (listLaneNum[0] == _hitsRaneNum)
        {
            notesJudgeController.Judgement(listNotesTime[0]);
        }
    }
}
