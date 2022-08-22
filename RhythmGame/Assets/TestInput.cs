using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class TestInput : MonoBehaviour
{
    public GameObject pressObject;
    public GameObject findObject;
    public GameObject noFindObject;
    public TextMeshProUGUI text;

    void Awake()
    {
        // EnhancedTouchの有効化
        EnhancedTouchSupport.Enable();

    }

    public void OnPress()
    {
        Debug.Log("Press");
        bool isBoolPress = pressObject.activeSelf;
        pressObject.SetActive(!isBoolPress);

        TouchJudgement();
    }

    void TouchJudgement()
    {
        TouchControl touchCtl;
        Ray ray;
        RaycastHit[] hits = new RaycastHit[10]; // Rayがヒットするオブジェクトを格納
        int touchindex = 0;
        int hitNum;

        if (Touchscreen.current != null) // Androidタップ時の処理
        {
            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // タッチ数分、タッチ情報を確認する
                touchCtl = Touchscreen.current.touches[i];
                // タッチ情報の座標からRayを生成する
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());
                // Rayがヒットしているオブジェクトを検索する
                hitNum = Physics.RaycastNonAlloc(ray, hits);
                for (int j = 0; j < hitNum; j++)
                {
                    // hitを検索してLitesスクリプトのあるオブジェクトにヒットしていれば、
                    // このタッチでハンドラが実行されたと判断する
                    Lites liteObj = hits[j].collider.GetComponent<Lites>();
                    if (liteObj != null)
                    {
                        Debug.Log("オブジェクトにタッチしたよ"); ;
                        touchindex = i;

                        bool isBool = findObject.activeSelf;
                        findObject.SetActive(!isBool);
                        break;
                    }
                    else
                    {
                        bool isBool = noFindObject.activeSelf;
                        noFindObject.SetActive(!isBool);
                        Debug.Log("そこにオブジェクトは無いよ");
                    }
                }
            }
        }
        else if (Mouse.current != null) // マウスクリック時の処理
        {
            // クリック情報の座標からRayを生成する
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            // Rayがヒットしているオブジェクトを検索する
            hitNum = Physics.RaycastNonAlloc(ray, hits);
            for (int j = 0; j < hitNum; j++)
            {
                // hitを検索してLitesスクリプトのあるオブジェクトにヒットしていれば、
                // このタッチでハンドラが実行されたと判断する
                Lites liteObj = hits[j].collider.GetComponent<Lites>();
                if (liteObj != null)
                {
                    Debug.Log("オブジェクトにタッチしたよ"); ;
                    touchindex = 1;

                    bool isBool = findObject.activeSelf;
                    findObject.SetActive(!isBool);
                    break;
                }
                else
                {
                    bool isBool = noFindObject.activeSelf;
                    noFindObject.SetActive(!isBool);
                    Debug.Log("そこにオブジェクトは無いよ");
                }
            }
        }
    }
}
