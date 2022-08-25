using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputTouchManager : MonoBehaviour
{
    [SerializeField]
    private NotesJudgeController notesJudgeCon;

    void Awake()
    {
        // EnhancedTouchの有効化
        EnhancedTouchSupport.Enable();
    }

    public void OnPress()
    {
        //Debug.Log("Press");

        TouchJudgement();
    }

    void TouchJudgement()
    {
        Ray ray;

        if (Touchscreen.current != null) // Androidタップ時の処理
        {
            TouchControl touchCtl;

            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // タッチ数分、タッチ情報を確認する
                touchCtl = Touchscreen.current.touches[i];
                // タッチ情報の座標からRayを生成する
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());

                RaySerch(ray);
            }
        }
        else if (Mouse.current != null) // マウスクリック時の処理
        {
            // クリック情報の座標からRayを生成する
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaySerch(ray);
        }

        void RaySerch(Ray _ray)
        {
            RaycastHit[] hits = new RaycastHit[10]; // Rayがヒットするオブジェクトを格納

            // Rayがヒットしているオブジェクトを検索する
            int hitNum = Physics.RaycastNonAlloc(_ray, hits);

            for (int j = 0; j < hitNum; j++)
            {
                foreach(RaycastHit hit in hits)
                {
                    // hitを検索してBoxColliderのあるオブジェクトにヒットしているか
                    BoxCollider touchObj = hit.collider.GetComponent<BoxCollider>();
                    if (touchObj != null)
                    {
                        Lites liteObj = touchObj.transform.GetChild(0).GetComponent<Lites>();

                        //Debug.Log("LaneNum = " + liteObj.lightNum);

                        liteObj.ColorChange();

                        notesJudgeCon.RaneJudge(liteObj.lightNum);
                        break;
                    }
                    else
                    {
                        //Debug.Log("そこにオブジェクトは無いよ");
                    }
                }
            }
        }
    }

    // Debug用
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)//〇キーが押されたとき
        {
            notesJudgeCon.RaneJudge(0);
            PlaySceneManager.psManager.listRaneLite[0].ColorChange();
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            notesJudgeCon.RaneJudge(1);
            PlaySceneManager.psManager.listRaneLite[1].ColorChange();
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            notesJudgeCon.RaneJudge(2);
            PlaySceneManager.psManager.listRaneLite[2].ColorChange();
        }
        else if (Keyboard.current.fKey.isPressed)
        {
            notesJudgeCon.RaneJudge(3);
            PlaySceneManager.psManager.listRaneLite[3].ColorChange();
        }
    }
}
