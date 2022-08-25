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
        // EnhancedTouch�̗L����
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

        if (Touchscreen.current != null) // Android�^�b�v���̏���
        {
            TouchControl touchCtl;

            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // �^�b�`�����A�^�b�`�����m�F����
                touchCtl = Touchscreen.current.touches[i];
                // �^�b�`���̍��W����Ray�𐶐�����
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());

                RaySerch(ray);
            }
        }
        else if (Mouse.current != null) // �}�E�X�N���b�N���̏���
        {
            // �N���b�N���̍��W����Ray�𐶐�����
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaySerch(ray);
        }

        void RaySerch(Ray _ray)
        {
            RaycastHit[] hits = new RaycastHit[10]; // Ray���q�b�g����I�u�W�F�N�g���i�[

            // Ray���q�b�g���Ă���I�u�W�F�N�g����������
            int hitNum = Physics.RaycastNonAlloc(_ray, hits);

            for (int j = 0; j < hitNum; j++)
            {
                foreach(RaycastHit hit in hits)
                {
                    // hit����������BoxCollider�̂���I�u�W�F�N�g�Ƀq�b�g���Ă��邩
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
                        //Debug.Log("�����ɃI�u�W�F�N�g�͖�����");
                    }
                }
            }
        }
    }

    // Debug�p
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)//�Z�L�[�������ꂽ�Ƃ�
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
