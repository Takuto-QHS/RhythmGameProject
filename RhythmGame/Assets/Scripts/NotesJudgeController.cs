using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesJudgeController : MonoBehaviour
{
    [SerializeField]
    NotesManager notesManager;//�X�N���v�g�unotesManager�v������ϐ�
    [SerializeField]
    private ScoreBoxWindow scoreBoxWindow;//�X�N���v�g�uscoreBoxWindow������ϐ��v
    [SerializeField]
    private GameObject[] MessageObj;//�v���C���[�ɔ����`����Q�[���I�u�W�F�N�g

    [SerializeField]
    private float timePerfect = 0.05f;
    [SerializeField]
    private float timeGreat = 0.1f;
    [SerializeField]
    private float timeGood = 0.125f;
    [SerializeField]
    private float timeBadMiss = 0.15f;

    void Update()
    {

        if(notesManager.listNotesTime.Count == 0)
        {
            return;
        }
        //Debug.Log(notesManager.listNotesTime.Count);

        if (Input.GetKeyDown(KeyCode.A))//�Z�L�[�������ꂽ�Ƃ�
        {
            if (notesManager.listLaneNum[0] == 0)//�����ꂽ�{�^���̓��[���̔ԍ��Ƃ����Ă��邩�H
            {
                /*
                �{���m�[�c���������ꏊ�Ǝ��ۂɂ��������ꏊ���ǂꂭ�炢����Ă��邩�����߁A
                ���̐�Βl��Judgement�֐��ɑ���
                */
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
                PlaySceneManager.psManager.soundManager.StartSE(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (notesManager.listLaneNum[0] == 1)
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
                PlaySceneManager.psManager.soundManager.StartSE(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (notesManager.listLaneNum[0] == 2)
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
                PlaySceneManager.psManager.soundManager.StartSE(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (notesManager.listLaneNum[0] == 3)
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
                PlaySceneManager.psManager.soundManager.StartSE(0);
            }
        }
        else if (Time.time > notesManager.listNotesTime[0] + timeBadMiss)//�{���m�[�c���������ׂ����Ԃ���0.2�b�����Ă����͂��Ȃ������ꍇ
        {
            message(4);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueMiss;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            PlaySceneManager.psManager.valueCombo = 0;
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo);
            scoreBoxWindow.UpdateTxtMiss(PlaySceneManager.psManager.valueMiss++);
            deleteData();
            Debug.Log("Miss");
        }
    }
    void Judgement(float timeLag)
    {
        if (timeLag <= timePerfect)//�{���m�[�c���������ׂ����ԂƎ��ۂɃm�[�c�������������Ԃ̌덷��timePerfect�b�ȉ���������
        {
            Debug.Log("Perfect");
            message(0);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValuePerfect;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtPerfect(PlaySceneManager.psManager.valuePerfect++);
            deleteData();
        }
        else if (timeLag <= timeGood)
        {
            Debug.Log("Great");
            message(1);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGreat;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtGreat(PlaySceneManager.psManager.valueGreat++);
            deleteData();
        }
        else if (timeLag <= timeGreat)
        {
            Debug.Log("Great");
            message(2);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGood;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtGood(PlaySceneManager.psManager.valueGood++);
            deleteData();
        }
        else if (timeLag <= timeBadMiss)
        {
            Debug.Log("Bad");
            message(3);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueBad;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtBad(PlaySceneManager.psManager.valueBad++);
            deleteData();
        }
    }

    float GetABS(float num)//�����̐�Βl��Ԃ��֐�
    {
        return num >= 0 ? num : -num;
    }
    void deleteData()//���łɂ��������m�[�c���폜����֐�
    {
        notesManager.listNotesTime.RemoveAt(0);
        notesManager.listLaneNum.RemoveAt(0);
        notesManager.listNoteType.RemoveAt(0);
    }

    void message(int judge)//�����\������
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.listLaneNum[0] - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
