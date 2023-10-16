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
            // ScriptableObj�ɕێ����Ă���l��Text�ɔ��f
            resultScore.UpdateResultScoreBox();

            RhythmGameManager.soundManager.PlayResultSE();
        }
    }

    public void PushSelectBtn()
    {
        // �L�^�X�V����Save����


        RhythmGameManager.soundManager.PlayPushBtnSE();

        // SelectScene�ɖ߂�
        RhythmGameManager.sceneManager.ChangeSelectScene(manager);
    }
}
