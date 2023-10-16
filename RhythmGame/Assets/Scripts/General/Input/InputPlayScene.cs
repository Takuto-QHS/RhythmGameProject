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

    bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        RhythmGameManager.inputManager.inputtableInterface = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPressed)
        {
            PressPerformed();
        }
    }

    public void PressStarted()
    {
        Debug.Log("started(�P����)");
        TouchJudgement(ETouchType.Tap);
    }

    public void PressPerformed()
    {
        if(!isPressed)
        {
            isPressed = true;
            return;
        }

        //Debug.Log("performed(������)");
        TouchJudgement(ETouchType.LongTap);
    }

    public void PressCanceled()
    {
        if(isPressed)
        {
            isPressed = false;
            PlaySceneManager.psManager.soundManager.StopLongPressSE();
        }
        Debug.Log("Canceled(�L�����Z��)");
    }

    void TouchJudgement(ETouchType _eTouchType)
    {
        Ray ray;

        if (Touchscreen.current != null) /* Android�^�b�v���̏��� */
        {
            TouchControl touchCtl;

            // �^�b�`�����ARay���΂�
            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // Camera����^�b�`�������W��Ray�𐶐�
                touchCtl = Touchscreen.current.touches[i];
                ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());

                RayHitProcess(_eTouchType, ray);
            }
        }
        else if (Mouse.current != null) /* �}�E�X�N���b�N���̏��� */
        {
            // Camera����N���b�N�������W��Ray�𐶐�
            Vector2 vec = Mouse.current.position.ReadValue();
            ray = Camera.main.ScreenPointToRay(vec);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            RayHitProcess(_eTouchType, ray);
        }
    }

    void RayHitProcess(ETouchType _eTouchType, Ray _ray)
    {
        Vector3 posHitRane = RayHitJudgeLinePos(_ray);       // Ray��Hit�����ʒu�̔����Pos���Z�o
        RaycastHit[] hits = JudgeLineRayHit(posHitRane);    // �����Pos��ɂ���Obj���擾
        Lites hitLitesLane = RaySerchLaneObj(hits);         // ���[�����擾

        if (hitLitesLane)
        {
            hitLitesLane.ColorChange();

            TouchTypeJudge(_eTouchType, hitLitesLane);  // �^�C�v�ʔ��菈���N��
        }

        LongNoteSE(hits);
    }

    Vector3 RayHitJudgeLinePos(Ray _ray)
    {
        Vector3 pos = new Vector3();
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit))
        {
            pos = new Vector3(hit.point.x, 0.0f, 0.0f);
        }
        return pos;
    }

    RaycastHit[] JudgeLineRayHit(Vector3 _pos)
    {
        RaycastHit[] hits = new RaycastHit[10];

        Vector3 posStart = new Vector3(_pos.x, _pos.y + 0.5f, _pos.z);

        int length = Physics.RaycastNonAlloc(posStart, Vector3.down, hits, 1.0f);
        System.Array.Resize(ref hits, length);
        Debug.DrawRay(posStart, Vector3.down, Color.blue, 1.0f);

        return hits;
    }

    Lites RaySerchLaneObj(RaycastHit[] _hits)
    {
        Lites hitLites = null;
        foreach (RaycastHit hit in _hits)
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
        }
        return hitLites;
    }

    Notes RaySerchLongNote(RaycastHit[] _hits)
    {
        Notes hitLongNote = null;
        int x = 0;
        foreach (RaycastHit hit in _hits)
        {
            Notes longNote = _hits[x].collider.GetComponent<Notes>();
            if (longNote != null)
            {
                if (longNote.isLongNote)
                {
                    hitLongNote = longNote;
                    break;
                }
            }
            x++;
        }
        return hitLongNote;
    }

    void TouchTypeJudge(ETouchType _eTouchType,Lites _hitLitesLane)
    {
        switch (_eTouchType)
        {
            case ETouchType.Tap:
                if (deligateTapJudge != null) deligateTapJudge(_hitLitesLane.lightNum);
                PlaySceneManager.psManager.soundManager.PlayNortTapSE();
                break;
            case ETouchType.LongTap:
                if (deligateLongTapJudge != null) deligateLongTapJudge(_hitLitesLane.lightNum);
                break;
        }
    }

    void LongNoteSE(RaycastHit[] _hits)
    {
        Notes hitLongNote = RaySerchLongNote(_hits);
        //Debug.Log(hitLongNote);
        if (hitLongNote)
        {
            RhythmGameManager.soundManager.StartLongPressSE();
        }
        else
        {
            RhythmGameManager.soundManager.StopLongPressSE();
        }
    }

}
