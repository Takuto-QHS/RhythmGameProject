using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputTouchManager : MonoBehaviour
{
    void Awake()
    {
        // EnhancedTouchの有効化
        EnhancedTouchSupport.Enable();
    }

    void Start()
    {
        // イベント処理
        // 指が触れた時
        Touch.onFingerDown += OnFingerDown;
        // 指を動かした（ドラッグ）時
        Touch.onFingerMove += OnFingerMove;
        // 指を離した時
        Touch.onFingerUp += OnFingerUp;
    }

    void OnFingerDown(Finger finger)
    {
        Debug.Log(finger.screenPosition);
    }
    void OnFingerMove(Finger finger)
    {
        Debug.Log(finger.screenPosition);
    }

    void OnFingerUp(Finger finger)
    {
        Debug.Log(finger.screenPosition);
    }
}
