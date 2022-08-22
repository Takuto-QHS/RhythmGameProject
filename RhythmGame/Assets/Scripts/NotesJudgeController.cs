using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
        if (notesManager.listNotesTime.Count == 0)
        {
            return;
        }

        if (Time.time > notesManager.listNotesTime[0] + timeBadMiss)//�{���m�[�c���������ׂ����Ԃ���0.15�b�����Ă����͂��Ȃ������ꍇ
        {
            Message(4);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueMiss;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            PlaySceneManager.psManager.valueCombo = 0;
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo);
            scoreBoxWindow.UpdateTxtMiss(PlaySceneManager.psManager.valueMiss++);
            deleteData();
            //Debug.Log("Miss");
        }
    }

    public void RaneJudge(int _hitsRaneNum)
    {
        if (notesManager.listNotesTime.Count == 0)
        {
            return;
        }
        //Debug.Log(notesManager.listNotesTime.Count);

        if (notesManager.listLaneNum[0] == _hitsRaneNum)
        {
            /*
            �{���m�[�c���������ꏊ�Ǝ��ۂɂ��������ꏊ���ǂꂭ�炢����Ă��邩�����߁A
            ���̐�Βl��Judgement�֐��ɑ���
            */
            Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
        }
    }

    void Judgement(float timeLag)
    {
        if (timeLag <= timePerfect)//�{���m�[�c���������ׂ����ԂƎ��ۂɃm�[�c�������������Ԃ̌덷��timePerfect�b�ȉ���������
        {
            //Debug.Log("Perfect");
            Message(0);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValuePerfect;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtPerfect(PlaySceneManager.psManager.valuePerfect++);
            deleteData();

            PlaySceneManager.psManager.soundManager.StartSE(0);
        }
        else if (timeLag <= timeGood)
        {
            //Debug.Log("Great");
            Message(1);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGreat;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtGreat(PlaySceneManager.psManager.valueGreat++);
            deleteData();

            PlaySceneManager.psManager.soundManager.StartSE(0);
        }
        else if (timeLag <= timeGreat)
        {
            //Debug.Log("Great");
            Message(2);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueGood;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtGood(PlaySceneManager.psManager.valueGood++);
            deleteData();

            PlaySceneManager.psManager.soundManager.StartSE(0);
        }
        else if (timeLag <= timeBadMiss)
        {
            //Debug.Log("Bad");
            Message(3);
            PlaySceneManager.psManager.valueScore = PlaySceneManager.psManager.valueScore + PlaySceneManager.psManager.addScoreValueBad;
            scoreBoxWindow.UpdateTxtScore(PlaySceneManager.psManager.valueScore);
            scoreBoxWindow.UpdateTxtCombo(PlaySceneManager.psManager.valueCombo++);
            scoreBoxWindow.UpdateTxtBad(PlaySceneManager.psManager.valueBad++);
            deleteData();

            PlaySceneManager.psManager.soundManager.StartSE(0);
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
    void Message(int judge)//�����\������
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.listLaneNum[0] - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
