using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputPlayScene : MonoBehaviour , IInputtable
{

    [SerializeField]
    private NotesJudgeController notesJudgeCon;

    public UnityAction<int> deligateTapJudge;
    public UnityAction<int> deligateLongTapJudge;

    enum ETouchType
    {
        Tap,
        LongTap,
    }

    // Start is called before the first frame update
    void Start()
    {
        RhythmGameManager.inputManager.inputtableInterface = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressStarted()
    {
        Debug.Log("started(単押し)");
        TouchJudgement(ETouchType.Tap);
    }

    public void PressPerformed()
    {

        Debug.Log("performed(長押し)");
        TouchJudgement(ETouchType.LongTap);
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
                if (hitLitesLane)
                {
                    hitLitesLane.ColorChange();

                    // タイプ別判定処理起動
                    switch (eTouchType)
                    {
                        case ETouchType.Tap:
                            if (deligateTapJudge != null) deligateTapJudge(hitLitesLane.lightNum);
                            PlaySceneManager.psManager.soundManager.StartSE(0);
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
                        PlaySceneManager.psManager.soundManager.StartSE(0);
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
}
