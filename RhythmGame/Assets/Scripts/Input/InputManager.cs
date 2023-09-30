using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.InputSystem;

/* Inputの流れ
 * 1.InputTouchのActionMaps「Player」
 * 2.Actionｓ「Press」でAction Typeを「Button」にする事でタッチ、クリックした際の検知をする
 * 3.InputManagerというGameObjにあるPlayerInputコンポーネントのBehaviorが「Invoke Unity Events」になっている為、
 *   下のEvents枠の「Player」→「Press」にこのスクリプトの「OnPress」関数が当てられていて検知したら発火する
 * 4.Sceneに飛べばinputttableInterface変数に各種SceneのInputManagerが入る為、各種SceneのInputManagerの該当関数が呼ばれる
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

        // EnhancedTouchの有効化
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

    // Debug用
    void Update()
    {
        //if (Keyboard.current.aKey.isPressed)//〇キーが押されたとき
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
