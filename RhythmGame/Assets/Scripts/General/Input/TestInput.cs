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
        // EnhancedTouch�̗L����
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
        RaycastHit[] hits = new RaycastHit[10]; // Ray���q�b�g����I�u�W�F�N�g���i�[
        int touchindex = 0;
        int hitNum;

        if (Touchscreen.current != null) // Android�^�b�v���̏���
        {
            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // �^�b�`�����A�^�b�`�����m�F����
                touchCtl = Touchscreen.current.touches[i];
                // �^�b�`���̍��W����Ray�𐶐�����
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());
                // Ray���q�b�g���Ă���I�u�W�F�N�g����������
                hitNum = Physics.RaycastNonAlloc(ray, hits);
                for (int j = 0; j < hitNum; j++)
                {
                    // hit����������Lites�X�N���v�g�̂���I�u�W�F�N�g�Ƀq�b�g���Ă���΁A
                    // ���̃^�b�`�Ńn���h�������s���ꂽ�Ɣ��f����
                    Lites liteObj = hits[j].collider.GetComponent<Lites>();
                    if (liteObj != null)
                    {
                        Debug.Log("�I�u�W�F�N�g�Ƀ^�b�`������"); ;
                        touchindex = i;

                        bool isBool = findObject.activeSelf;
                        findObject.SetActive(!isBool);
                        break;
                    }
                    else
                    {
                        bool isBool = noFindObject.activeSelf;
                        noFindObject.SetActive(!isBool);
                        Debug.Log("�����ɃI�u�W�F�N�g�͖�����");
                    }
                }
            }
        }
        else if (Mouse.current != null) // �}�E�X�N���b�N���̏���
        {
            // �N���b�N���̍��W����Ray�𐶐�����
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            // Ray���q�b�g���Ă���I�u�W�F�N�g����������
            hitNum = Physics.RaycastNonAlloc(ray, hits);
            for (int j = 0; j < hitNum; j++)
            {
                // hit����������Lites�X�N���v�g�̂���I�u�W�F�N�g�Ƀq�b�g���Ă���΁A
                // ���̃^�b�`�Ńn���h�������s���ꂽ�Ɣ��f����
                Lites liteObj = hits[j].collider.GetComponent<Lites>();
                if (liteObj != null)
                {
                    Debug.Log("�I�u�W�F�N�g�Ƀ^�b�`������"); ;
                    touchindex = 1;

                    bool isBool = findObject.activeSelf;
                    findObject.SetActive(!isBool);
                    break;
                }
                else
                {
                    bool isBool = noFindObject.activeSelf;
                    noFindObject.SetActive(!isBool);
                    Debug.Log("�����ɃI�u�W�F�N�g�͖�����");
                }
            }
        }
    }
}
