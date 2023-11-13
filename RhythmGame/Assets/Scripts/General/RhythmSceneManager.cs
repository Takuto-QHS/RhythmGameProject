using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmSceneManager : MonoBehaviour
{
    public enum SCENE_STATE
    {
        SELECT,
        PLAY,
        RESULT
    }
    public SCENE_STATE sceneState;

    public string sceneSelect = "Select&ResultScene";
    public string scenePlay = "GameScene";

    private AsyncOperation asyncLoad;

    public void ChangePlayScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(scenePlay);
        asyncLoad.allowSceneActivation = false;

        RhythmGameManager.fadeManager.CanvasFadeOut();
        RhythmGameManager.fadeManager.fadeFinishCallback.onComplete.AddListener(CompleateLoad);

        sceneState = SCENE_STATE.PLAY;
    }

    public void ChangeResultScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneSelect);
        asyncLoad.allowSceneActivation = false;

        RhythmGameManager.fadeManager.CanvasFadeOut();
        RhythmGameManager.fadeManager.fadeFinishCallback.onComplete.AddListener(CompleateLoad);

        // �V�[���؂�ւ��ŏ���ɏ�����null�ɂȂ邪�AIF����null����ɂȂ�Ȃ���
        RhythmGameManager.inputManager.inputtableInterface = null;

        sceneState = SCENE_STATE.RESULT;
    }

    public void ChangeSelectScene(SelectResultManager manager)
    {
        sceneState = SCENE_STATE.SELECT;
        manager.ChangeState(sceneState);
    }

    /// <summary>
    /// �t�F�[�h��AnimationClip�̏I�����m�C�x���g�p�R�[���o�b�N�֐�
    /// </summary>
    void CompleateLoad()
    {
        asyncLoad.allowSceneActivation = true;
        RhythmGameManager.fadeManager.CanvasFadeIn();
    }
    public bool GetCompleateLoad()
    {
        return asyncLoad.progress >= 0.9f;
    }
}
