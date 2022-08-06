using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeController : MonoBehaviour
{
    //変数の宣言
    [SerializeField] private GameObject[] MessageObj;//プレイヤーに判定を伝えるゲームオブジェクト
    [SerializeField] NotesManager notesManager;//スクリプト「notesManager」を入れる変数
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//〇キーが押されたとき
        {
            if (notesManager.listLaneNum[0] == 0)//押されたボタンはレーンの番号とあっているか？
            {
                Judgement(GetABS(Time.time - notesManager.listNotesTime[0]));
                /*
                本来ノーツをたたく場所と実際にたたいた場所がどれくらいずれているかを求め、
                その絶対値をJudgement関数に送る
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

        if (Time.time > notesManager.listNotesTime[0] + 0.2f)//本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
        {
            message(3);
            deleteData();
            Debug.Log("Miss");
        }
    }
    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
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
    float GetABS(float num)//引数の絶対値を返す関数
    {
        return num >= 0 ? num : -num;
    }
    void deleteData()//すでにたたいたノーツを削除する関数
    {
        notesManager.listNotesTime.RemoveAt(0);
        notesManager.listLaneNum.RemoveAt(0);
        notesManager.listNoteType.RemoveAt(0);
    }

    void message(int judge)//判定を表示する
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.listLaneNum[0] - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
