using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeController : MonoBehaviour
{
    //�ϐ��̐錾
    [SerializeField] private GameObject[] MessageObj;//�v���C���[�ɔ����`����Q�[���I�u�W�F�N�g
    [SerializeField] NotesManager notesManager;//�X�N���v�g�unotesManager�v������ϐ�
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//�Z�L�[�������ꂽ�Ƃ�
        {
            if (notesManager.listLaneNum[0] == 0)//�����ꂽ�{�^���̓��[���̔ԍ��Ƃ����Ă��邩�H
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
                /*
                �{���m�[�c���������ꏊ�Ǝ��ۂɂ��������ꏊ���ǂꂭ�炢����Ă��邩�����߁A
                ���̐�Βl��Judgement�֐��ɑ���
                */
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (notesManager.listLaneNum[0] == 1)
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (notesManager.listLaneNum[0] == 2)
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (notesManager.listLaneNum[0] == 3)
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
            }
        }

        if (Time.time > notesManager.listNotesTime[0] + 0.2f)//�{���m�[�c���������ׂ����Ԃ���0.2�b�����Ă����͂��Ȃ������ꍇ
        {
            message(3);
            deleteData();
            Debug.Log("Miss");
        }
    }
    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10)//�{���m�[�c���������ׂ����ԂƎ��ۂɃm�[�c�������������Ԃ̌덷��0.1�b�ȉ���������
        {
            Debug.Log("Perfect");
            message(0);
            deleteData();
        }
        else
        {
            if (timeLag <= 0.15)
            {
                Debug.Log("Great");
                message(1);
                deleteData();
            }
            else
            {
                if (timeLag <= 0.20)
                {
                    Debug.Log("Bad");
                    message(2);
                    deleteData();
                }
            }
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
