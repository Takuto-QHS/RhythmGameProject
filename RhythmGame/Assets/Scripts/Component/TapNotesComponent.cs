using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapNotesComponent : MonoBehaviour
{
    [SerializeField]
    NotesJudgeController notesJudgeController;

    [SerializeField]
    GameObject noteTapObj;

    [SerializeField]
    float destroyNoteTime = 1.0f;

    [SerializeField]
    private GameObject tapEffect;

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
        GameSceneManager.gsManager.inputPlayScene.deligateTapJudge += RaneJudge;
    }

    // Update is called once per frame
    void Update()
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        //本来ノーツをたたくべき時間から0.15秒たっても入力がなかった場合
        if (GameSceneManager.gsManager.playStopWatchTime > listNotesTime[0] + notesJudgeController.timeBadMiss)
        {
            notesJudgeController.JudgeMiss(listLaneNum[0]);
            StartCoroutine("ObjDelayDestroy", listNotesObj[0]);
            DeleteData();
        }
    }

    void InitNotes()
    {
        string inputString = GameSceneManager.gsManager.notesManager.notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // 時間計算＆List追加＆生成関数
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].type == 1)
            {
                // ノーツの判定時間
                float time = GameSceneManager.gsManager.notesManager.GetNoteTime(inputJson.notes[i]);

                // ノーツの降ってくる時間
                float posTime = GameSceneManager.gsManager.notesManager.GetNoteTime(inputJson.notes[i], true);

                InstantiateNote(time, posTime, inputJson.notes[i].block, inputJson.notes[i].type);
            }
        }
    }

    public void InstantiateNote(float _time, float _posTime, int _block,int _type)
    {
        // リスト追加
        listNotesTime.Add(_time);
        listLaneNum.Add(_block);
        listNoteType.Add(_type);

        // 生成
        float z = _posTime * GameSceneManager.gsManager.notesSpeed;
        GameObject obj = Instantiate(noteTapObj, new Vector3(_block - 2.5f, 0.01f, z), noteTapObj.transform.rotation);
        listNotesObj.Add(obj);
    }

    public void DeleteData()//すでにたたいたノーツ判定を削除する関数
    {
        listNotesTime.RemoveAt(0);
        listLaneNum.RemoveAt(0);
        listNoteType.RemoveAt(0);
        listNotesObj.RemoveAt(0);
    }

    IEnumerator ObjDelayDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(destroyNoteTime);
        Destroy(obj);
    }

    public void RaneJudge(Lites lites)
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }
        //Debug.Log(notesManager.listNotesTime.Count);

        if (listLaneNum[0] == lites.lightNum)
        {
            // 判定タイプ取得
            NotesJudgeController.EJudgeType eJudgeType;
            eJudgeType = notesJudgeController.Judgement(listNotesTime[0], listLaneNum[0]);

            if(eJudgeType != NotesJudgeController.EJudgeType.Ignore)
            {
                Transform[] effectPos = lites.gameObject.GetComponentsInChildren<Transform>();
                Instantiate(tapEffect, effectPos[1].position, Quaternion.Euler(0, 0, 0));
                Destroy(listNotesObj[0]);
                DeleteData();
            }
        }
    }

    public void MoveNotes(bool _isMove)
    {
        foreach(GameObject note in listNotesObj)
        {
            Notes thisNote = note.GetComponent<Notes>();
            thisNote.isMove = _isMove;
        }
    }
}
