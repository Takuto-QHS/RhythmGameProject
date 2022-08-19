using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputTouchManager : MonoBehaviour
{
    void Awake()
    {
        // EnhancedTouch�̗L����
        EnhancedTouchSupport.Enable();
    }

    void Start()
    {
        // �C�x���g����
        // �w���G�ꂽ��
        Touch.onFingerDown += OnFingerDown;
        // �w�𓮂������i�h���b�O�j��
        Touch.onFingerMove += OnFingerMove;
        // �w�𗣂�����
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
