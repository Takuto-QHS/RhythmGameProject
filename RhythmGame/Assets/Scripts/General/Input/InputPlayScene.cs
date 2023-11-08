using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Events;
/// <summary>
/// Gameシーンでしか使用しません
/// </summary>
public class InputPlayScene : MonoBehaviour , IInputtable
{

    [SerializeField]
    private NotesJudgeController notesJudgeCon;

    public UnityAction<Lites> deligateTapJudge;
    public UnityAction<Lites> deligateLongTapJudge;

    enum ETouchType
    {
        Tap,
        LongTap,
    }

    LongNotesComponent longNotesComponent;
    bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        RhythmGameManager.inputManager.inputtableInterface = this;
        longNotesComponent = GameSceneManager.gsManager.notesManager.longNotesComp;
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
        Debug.Log("started(単押し)");
        TouchJudgement(ETouchType.Tap);
    }

    public void PressPerformed()
    {
        if(!isPressed)
        {
            isPressed = true;
            return;
        }

        //Debug.Log("performed(長押し)");
        TouchJudgement(ETouchType.LongTap);
    }

    public void PressCanceled()
    {
        if(isPressed)
        {
            isPressed = false;
            GameSceneManager.gsManager.soundManager.StopLongPressSE();
        }
        Debug.Log("Canceled(キャンセル)");
    }

    void TouchJudgement(ETouchType _eTouchType)
    {
        Ray _ray;

        if (Touchscreen.current != null) /* Androidタップ時の処理 */
        {
            TouchControl touchCtl;

            // タッチ数分、Rayを飛ばす
            for (int i = 0; i < Touchscreen.current.touches.Count; i++)
            {
                // Cameraからタッチした座標へRayを生成
                touchCtl = Touchscreen.current.touches[i];
                _ray = Camera.main.ScreenPointToRay(touchCtl.position.ReadValue());

                RayHitProcess(_eTouchType, _ray);
            }
        }
        else if (Mouse.current != null) /* マウスクリック時の処理 */
        {
            // Cameraからクリックした座標へRayを生成
            Vector2 vec = Mouse.current.position.ReadValue();
            _ray = Camera.main.ScreenPointToRay(vec);
            Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.red);

            RayHitProcess(_eTouchType, _ray);
        }
    }

    void RayHitProcess(ETouchType _eTouchType, Ray _ray)
    {
        Vector3 posHitRane = RayHitJudgeLinePos(_ray);       // RayがHitした位置の判定線Posを算出
        RaycastHit[] hits = JudgeLineRayHit(posHitRane);    // 判定線Pos上にあるObjを取得
        Lites hitLitesLane = RaySerchLaneObj(hits);         // レーンを取得

        if (hitLitesLane)
        {
            hitLitesLane.ColorChange();

            TouchTypeJudge(_eTouchType, hitLitesLane);  // タイプ別判定処理起動
        }

        LongNoteSE(hits);
        LongNoteEffect(hits, posHitRane);
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
            // hitを検索してBoxColliderのあるオブジェクトにヒットしているか
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
                if (deligateTapJudge != null) deligateTapJudge(_hitLitesLane);
                GameSceneManager.gsManager.soundManager.PlayNortTapSE();
                break;
            case ETouchType.LongTap:
                if (deligateLongTapJudge != null) deligateLongTapJudge(_hitLitesLane);
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

    void LongNoteEffect(RaycastHit[] _hits , Vector3 _effectPos)
    {
        Notes hitLongNote = RaySerchLongNote(_hits);
        //Debug.Log(hitLongNote);
        if (hitLongNote)
        {
            // エフェクト
            if(longNotesComponent)
            {
                longNotesComponent.LongEffectActiveSwitch(true);
                longNotesComponent.LongEffectMove(_effectPos);
            }
        }
        else
        {
            longNotesComponent.LongEffectActiveSwitch(false);
        }
    }
}
