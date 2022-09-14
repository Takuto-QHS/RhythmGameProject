using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class NotesJudgeController : MonoBehaviour
{
    [SerializeField]
    TapNotesComponent tapNotesComp;

    [SerializeField]
    NotesManager notesManager;
    [SerializeField]
    private ScoreBoxWindow scoreBoxWindow;
    [SerializeField]
    private GameObject[] MessageObj;//プレイヤーに判定を伝えるゲームオブジェクト

    public float timePerfect = 0.05f;
    public float timeGreat = 0.1f;
    public float timeGood = 0.125f;
    public float timeBadMiss = 0.15f;

    public enum EJudgeType
    {
        Perfect,
        Great,
        Good,
        Bad,
        Miss,
        Ignore // 無視
    }

    public EJudgeType Judgement(float noteTime, int laneNum)
    {
        // 本来ノーツを叩く時間と実際に叩いた時間がどれくらいずれているかを求め、
        // その絶対値をtimeLagとする
        float timeLag = GetABS(Time.time - noteTime);
        EJudgeType type = EJudgeType.Miss;

        if (timeLag <= timePerfect)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差がtimePerfect秒以下だったら
        {
            JudgePerfect(laneNum);
            type = EJudgeType.Perfect;
        }
        else if (timeLag <= timeGreat)
        {
            JudgeGreat(laneNum);
            type = EJudgeType.Great;
        }
        else if (timeLag <= timeGood)
        {
            JudgeGood(laneNum);
            type = EJudgeType.Good;
        }
        else if (timeLag <= timeBadMiss)
        {
            JudgeBad(laneNum);
            type = EJudgeType.Bad;
        }
        return type;
    }

    float GetABS(float num)//引数の絶対値を返す関数
    {
        return num >= 0 ? num : -num;
    }
    void Message(int judge,int laneNum)//判定を表示する
    {
        Instantiate(MessageObj[judge], new Vector3(laneNum - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
    public void JudgePerfect(int laneNum)
    {
        //Debug.Log("Perfect");
        Message(0, laneNum);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValuePerfect;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
        scoreBoxWindow.UpdateTxtPerfect(PlaySceneManager.psManager.valuePerfect++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeGreat(int laneNum)
    {
        //Debug.Log("Great");
        Message(1, laneNum);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGood;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
        scoreBoxWindow.UpdateTxtGood(PlaySceneManager.psManager.valueGood++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeGood(int laneNum)
    {
        //Debug.Log("Good");
        Message(2, laneNum);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGreat;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
        scoreBoxWindow.UpdateTxtGreat(PlaySceneManager.psManager.valueGreat++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeBad(int laneNum)
    {
        //Debug.Log("Bad");
        Message(3,laneNum);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueBad;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        PlaySceneManager.psManager.valueCombo = 0;
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo);
        scoreBoxWindow.UpdateTxtBad(PlaySceneManager.psManager.valueBad++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeMiss(int laneNum)
    {
        //Debug.Log("Miss");
        Message(4,laneNum);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueBad;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        PlaySceneManager.psManager.valueCombo = 0;
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo);
        scoreBoxWindow.UpdateTxtBad(PlaySceneManager.psManager.valueBad++);
    }
}
