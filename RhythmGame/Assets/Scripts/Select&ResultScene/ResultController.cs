using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private SelectResultManager manager;

    [SerializeField]
    private ResultScore resultScore;

    private void OnEnable()
    {
        if(RhythmGameManager.sceneManager.sceneState == RhythmSceneManager.SCENE_STATE.RESULT)
        {
            // ScriptableObjに保持してある値をTextに反映
            resultScore.UpdateResultScoreBox();

            RhythmGameManager.soundManager.PlayResultSE();
        }
    }

    public void PushSelectBtn()
    {
        // 記録更新時にSaveする


        RhythmGameManager.soundManager.PlayPushBtnSE();

        // SelectSceneに戻る
        RhythmGameManager.sceneManager.ChangeSelectScene(manager);
    }
}
