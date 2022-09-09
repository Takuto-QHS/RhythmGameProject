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
    private GameObject[] MessageObj;//�v���C���[�ɔ����`����Q�[���I�u�W�F�N�g

    public float timePerfect = 0.05f;
    public float timeGreat = 0.1f;
    public float timeGood = 0.125f;
    public float timeBadMiss = 0.15f;

    public void Judgement(float noteTime)
    {
        // �{���m�[�c��@�����ԂƎ��ۂɒ@�������Ԃ��ǂꂭ�炢����Ă��邩�����߁A
        // ���̐�Βl��timeLag�Ƃ���
        float timeLag = GetABS(Time.time - noteTime);

        if (timeLag <= timePerfect)//�{���m�[�c���������ׂ����ԂƎ��ۂɃm�[�c�������������Ԃ̌덷��timePerfect�b�ȉ���������
        {
            JudgePerfect();
        }
        else if (timeLag <= timeGreat)
        {
            JudgeGreat();
        }
        else if (timeLag <= timeGood)
        {
            JudgeGood();
        }
        else if (timeLag <= timeBadMiss)
        {
            JudgeBadMiss();
        }
    }

    float GetABS(float num)//�����̐�Βl��Ԃ��֐�
    {
        return num >= 0 ? num : -num;
    }
    void Message(int judge)//�����\������
    {
        Instantiate(MessageObj[judge], new Vector3(tapNotesComp.listLaneNum[0] - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
    public void JudgePerfect()
    {
        //Debug.Log("Perfect");
        Message(0);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValuePerfect;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
        scoreBoxWindow.UpdateTxtPerfect(PlaySceneManager.psManager.valuePerfect++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeGreat()
    {
        //Debug.Log("Great");
        Message(1);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGood;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
        scoreBoxWindow.UpdateTxtGood(PlaySceneManager.psManager.valueGood++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeGood()
    {
        //Debug.Log("Good");
        Message(2);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGreat;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
        scoreBoxWindow.UpdateTxtGreat(PlaySceneManager.psManager.valueGreat++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }

    public void JudgeBadMiss()
    {
        //Debug.Log("Bad or Miss");
        Message(3);
        PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueBad;
        scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
        PlaySceneManager.psManager.valueCombo = 0;
        scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo);
        scoreBoxWindow.UpdateTxtBad(PlaySceneManager.psManager.valueBad++);
        tapNotesComp.DeleteData();

        PlaySceneManager.psManager.soundManager.StartSE(0);
    }
}
