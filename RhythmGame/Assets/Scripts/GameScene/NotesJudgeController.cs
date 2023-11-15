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
    private LaneTxt laneTxtBox;
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

    public EJudgeType Judgement(float noteTime, int laneNum ,bool typeOnly = false)
    {
        // 本来ノーツを叩く時間と実際に叩いた時間がどれくらいずれているかを求め、
        // その絶対値をtimeLagとする
        float timeLag = GetABS(GameSceneManager.gsManager.playStopWatchTime - noteTime);
        EJudgeType type = EJudgeType.Ignore;

        if (timeLag <= timePerfect)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差がtimePerfect秒以下だったら
        {
            type = EJudgeType.Perfect;
        }
        else if (timeLag <= timeGreat)
        {
            type = EJudgeType.Great;
        }
        else if (timeLag <= timeGood)
        {
            type = EJudgeType.Good;
        }
        else if (timeLag <= timeBadMiss)
        {
            type = EJudgeType.Bad;
        }

        if(!typeOnly)
        {
            JudgeTypeProcess(type, laneNum);
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

    void JudgeTypeProcess(EJudgeType eJudgeType, int laneNum)
    {
        switch (eJudgeType)
        {
            case EJudgeType.Perfect:
                JudgePerfect(laneNum);
                break;
            case EJudgeType.Great:
                JudgeGreat(laneNum);
                break;
            case EJudgeType.Good:
                JudgeGood(laneNum);
                break;
            case EJudgeType.Bad:
                JudgeBad(laneNum);
                break;
        }
    }

    public void JudgePerfect(int laneNum)
    {
        //Debug.Log("Perfect");
        Message(0, laneNum);

        // Score更新処理
        GameSceneManager.gsManager.valueScore = GameSceneManager.gsManager.valueScore + GameSceneManager.gsManager.addScoreData.addScoreValuePerfect;
        scoreBoxWindow.UpdateTxtScore((int)GameSceneManager.gsManager.valueScore);
        GameSceneManager.gsManager.valueCombo++;
        laneTxtBox.UpdateLaneTxtCombo(GameSceneManager.gsManager.valueCombo);
        GameSceneManager.gsManager.UpdateMaxCombo();
        scoreBoxWindow.UpdateTxtMaxCombo(GameSceneManager.gsManager.valueMaxCombo);
        GameSceneManager.gsManager.valuePerfect++;
        scoreBoxWindow.UpdateTxtPerfect(GameSceneManager.gsManager.valuePerfect);
    }

    public void JudgeGreat(int laneNum)
    {
        //Debug.Log("Great");
        Message(1, laneNum);

        // Score更新処理
        GameSceneManager.gsManager.valueScore = GameSceneManager.gsManager.valueScore + GameSceneManager.gsManager.addScoreData.addScoreValueGreat;
        scoreBoxWindow.UpdateTxtScore((int)GameSceneManager.gsManager.valueScore);
        GameSceneManager.gsManager.valueCombo++;
        laneTxtBox.UpdateLaneTxtCombo(GameSceneManager.gsManager.valueCombo);
        GameSceneManager.gsManager.UpdateMaxCombo();
        scoreBoxWindow.UpdateTxtMaxCombo(GameSceneManager.gsManager.valueMaxCombo);
        GameSceneManager.gsManager.valueGreat++;
        scoreBoxWindow.UpdateTxtGreat(GameSceneManager.gsManager.valueGreat);
    }

    public void JudgeGood(int laneNum)
    {
        //Debug.Log("Good");
        Message(2, laneNum);

        // Score更新処理
        GameSceneManager.gsManager.valueScore = GameSceneManager.gsManager.valueScore + GameSceneManager.gsManager.addScoreData.addScoreValueGood;
        scoreBoxWindow.UpdateTxtScore((int)GameSceneManager.gsManager.valueScore);
        GameSceneManager.gsManager.valueCombo++;
        laneTxtBox.UpdateLaneTxtCombo(GameSceneManager.gsManager.valueCombo);
        GameSceneManager.gsManager.UpdateMaxCombo();
        scoreBoxWindow.UpdateTxtMaxCombo(GameSceneManager.gsManager.valueMaxCombo);
        GameSceneManager.gsManager.valueGood++;
        scoreBoxWindow.UpdateTxtGood(GameSceneManager.gsManager.valueGood);
    }

    public void JudgeBad(int laneNum)
    {
        //Debug.Log("Bad");
        Message(3,laneNum);

        // Score更新処理
        GameSceneManager.gsManager.valueScore = GameSceneManager.gsManager.valueScore + GameSceneManager.gsManager.addScoreData.addScoreValueBad;
        scoreBoxWindow.UpdateTxtScore((int)GameSceneManager.gsManager.valueScore);
        GameSceneManager.gsManager.valueCombo = 0;
        laneTxtBox.UpdateLaneTxtCombo(GameSceneManager.gsManager.valueCombo);
        GameSceneManager.gsManager.valueBad++;
        scoreBoxWindow.UpdateTxtBad(GameSceneManager.gsManager.valueBad);
    }

    public void JudgeMiss(int laneNum)
    {
        //Debug.Log("Miss");
        Message(4,laneNum);

        // Score更新処理
        GameSceneManager.gsManager.valueScore = GameSceneManager.gsManager.valueScore + GameSceneManager.gsManager.addScoreData.addScoreValueMiss;
        scoreBoxWindow.UpdateTxtScore((int)GameSceneManager.gsManager.valueScore);
        GameSceneManager.gsManager.valueCombo = 0;
        laneTxtBox.UpdateLaneTxtCombo(GameSceneManager.gsManager.valueCombo);
        GameSceneManager.gsManager.valueMiss++;
        scoreBoxWindow.UpdateTxtMiss(GameSceneManager.gsManager.valueMiss);
    }
}
