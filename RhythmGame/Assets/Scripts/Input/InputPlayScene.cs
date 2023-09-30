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
        Debug.Log("started(�P����)");
        TouchJudgement(ETouchType.Tap);
    }

    public void PressPerformed()
    {

        Debug.Log("performed(������)");
        TouchJudgement(ETouchType.LongTap);
    }

    void TouchJudgement(ETouchType eTouchType)
    {
        Ray ray;

        if (Touchscreen.current != null) /* Android�^�b�v���̏��� */
        {
            TouchControl touchCtl;

            // �^�b�`�����ARay���΂�
            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // �^�b�`���̍��W����Ray�𐶐�����
                touchCtl = Touchscreen.current.touches[i];
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());

                // ���[�����擾
                Lites hitLitesLane = RaySerchLaneObj(ray);
                if (hitLitesLane)
                {
                    hitLitesLane.ColorChange();

                    // �^�C�v�ʔ��菈���N��
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
        else if (Mouse.current != null) /* �}�E�X�N���b�N���̏��� */
        {
            // �N���b�N���̍��W����Ray�𐶐�����
            ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            // ���[�����擾
            Lites hitLitesLane = RaySerchLaneObj(ray);
            if (hitLitesLane)
            {
                hitLitesLane.ColorChange();

                // �^�C�v�ʔ��菈���N��
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
        RaycastHit[] hits = new RaycastHit[10]; // Ray���q�b�g����I�u�W�F�N�g���i�[
        int hitNum = Physics.RaycastNonAlloc(_ray, hits); // Ray���q�b�g���Ă���I�u�W�F�N�g����������

        for (int j = 0; j < hitNum; j++)
        {
            foreach (RaycastHit hit in hits)
            {
                // hit����������BoxCollider�̂���I�u�W�F�N�g�Ƀq�b�g���Ă��邩
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
                    //Debug.Log("�����ɃI�u�W�F�N�g�͖�����");
                }
            }
        }

        return hitLites;
    }
}
