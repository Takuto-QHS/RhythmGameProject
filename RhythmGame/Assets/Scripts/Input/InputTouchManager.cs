using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Events;

public class InputTouchManager : MonoBehaviour
{
    [SerializeField]
    private NotesJudgeController notesJudgeCon;

    static public UnityAction<int> deligateTapJudge;
    static public UnityAction<int> deligateLongTapJudge;

    enum ETouchType
    {
        Tap,
        LongTap,
    }

    public void Awake()
    {
        // EnhancedTouchの有効化
        EnhancedTouchSupport.Enable();
    }

    public void OnPress(InputAction.CallbackContext context)
    {
        //Debug.Log("Press");
        if(context.started)
        {
            TouchJudgement(ETouchType.Tap);
        }
        else if(context.performed)
        {
            Debug.Log("performed(長押し)");
            TouchJudgement(ETouchType.LongTap);
        }
    }

    void TouchJudgement(ETouchType eTouchType)
    {
        Ray ray;

        if (Touchscreen.current != null) /* Androidタップ時の処理 */
        {
            TouchControl touchCtl;

            // タッチ数分、Rayを飛ばす
            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // タッチ情報の座標からRayを生成する
                touchCtl = Touchscreen.current.touches[i];
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());

                // レーンを取得
                Lites hitLitesLane = RaySerchLaneObj(ray);
                if(hitLitesLane)
                {
                    hitLitesLane.ColorChange();

                    // タイプ別判定処理起動
                    switch (eTouchType)
                    {
                        case ETouchType.Tap:
                            if (deligateTapJudge != null) deligateTapJudge(hitLitesLane.lightNum);
                            break;
                        case ETouchType.LongTap:
                            if (deligateLongTapJudge != null) deligateLongTapJudge(hitLitesLane.lightNum);
                            break;
                    }
                }
            }
        }
        else if (Mouse.current != null) /* マウスクリック時の処理 */
        {
            // クリック情報の座標からRayを生成する
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            // レーンを取得
            Lites hitLitesLane = RaySerchLaneObj(ray);
            if (hitLitesLane)
            {
                hitLitesLane.ColorChange();

                // タイプ別判定処理起動
                switch (eTouchType)
                {
                    case ETouchType.Tap:
                        if (deligateTapJudge != null) deligateTapJudge(hitLitesLane.lightNum);
                        break;
                    case ETouchType.LongTap:
                        if (deligateLongTapJudge != null) deligateLongTapJudge(hitLitesLane.lightNum);
                        break;
                }
            }
        }
    }

    Lites RaySerchLaneObj(Ray _ray)
    {
        Lites hitLites = null;
        RaycastHit[] hits = new RaycastHit[10]; // Rayがヒットするオブジェクトを格納
        int hitNum = Physics.RaycastNonAlloc(_ray, hits); // Rayがヒットしているオブジェクトを検索する

        for (int j = 0; j < hitNum; j++)
        {
            foreach (RaycastHit hit in hits)
            {
                // hitを検索してBoxColliderのあるオブジェクトにヒットしているか
                BoxCollider touchObj = hit.collider.GetComponent<BoxCollider>();
                if (touchObj != null)
                {
                    Lites liteObj = touchObj.transform.GetChild(0).GetComponent<Lites>();
                    //Debug.Log("LaneNum = " + liteObj.lightNum);
                    hitLites = liteObj;
                    break;
                }
                else
                {
                    //Debug.Log("そこにオブジェクトは無いよ");
                }
            }
        }

        return hitLites;
    }

    // Debug用
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)//〇キーが押されたとき
        {
            deligateTapJudge(0);
            PlaySceneManager.psManager.listRaneLite[0].ColorChange();
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            deligateTapJudge(1);
            PlaySceneManager.psManager.listRaneLite[1].ColorChange();
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            deligateTapJudge(2);
            PlaySceneManager.psManager.listRaneLite[2].ColorChange();
        }
        else if (Keyboard.current.fKey.isPressed)
        {
            deligateTapJudge(3);
            PlaySceneManager.psManager.listRaneLite[3].ColorChange();
        }
    }
}
