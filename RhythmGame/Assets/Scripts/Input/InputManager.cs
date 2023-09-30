using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.InputSystem;

/* Input�̗���
 * 1.InputTouch��ActionMaps�uPlayer�v
 * 2.Action���uPress�v��Action Type���uButton�v�ɂ��鎖�Ń^�b�`�A�N���b�N�����ۂ̌��m������
 * 3.InputManager�Ƃ���GameObj�ɂ���PlayerInput�R���|�[�l���g��Behavior���uInvoke Unity Events�v�ɂȂ��Ă���ׁA
 *   ����Events�g�́uPlayer�v���uPress�v�ɂ��̃X�N���v�g�́uOnPress�v�֐������Ă��Ă��Č��m�����甭�΂���
 * 4.Scene�ɔ�ׂ�inputttableInterface�ϐ��Ɋe��Scene��InputManager������ׁA�e��Scene��InputManager�̊Y���֐����Ă΂��
 */

public class InputManager : MonoBehaviour
{
    public IInputtable inputtableInterface;

    public void Awake()
    {
        if(RhythmGameManager.inputManager == null)
        {
            RhythmGameManager.inputManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // EnhancedTouch�̗L����
        EnhancedTouchSupport.Enable();
    }

    public void OnPress(InputAction.CallbackContext context)
    {
        Debug.Log("Press");
        if(context.started)
        {
            inputtableInterface.PressStarted();
        }
        else if(context.performed)
        {
            inputtableInterface.PressPerformed();
        }
    }

    // Debug�p
    void Update()
    {
        //if (Keyboard.current.aKey.isPressed)//�Z�L�[�������ꂽ�Ƃ�
        //{
        //    deligateTapJudge(0);
        //    PlaySceneManager.psManager.listRaneLite[0].ColorChange();
        //}
        //else if (Keyboard.current.sKey.isPressed)
        //{
        //    deligateTapJudge(1);
        //    PlaySceneManager.psManager.listRaneLite[1].ColorChange();
        //}
        //else if (Keyboard.current.dKey.isPressed)
        //{
        //    deligateTapJudge(2);
        //    PlaySceneManager.psManager.listRaneLite[2].ColorChange();
        //}
        //else if (Keyboard.current.fKey.isPressed)
        //{
        //    deligateTapJudge(3);
        //    PlaySceneManager.psManager.listRaneLite[3].ColorChange();
        //}
    }
}
