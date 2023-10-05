using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmSceneManager : MonoBehaviour
{
    public string sceneSelect = "Select&ResultScene";
    public string scenePlay = "GameScene";

    private AsyncOperation asyncLoad;

    void Start()
    {
        
    }

    public void ChangePlayScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(scenePlay);
        asyncLoad.allowSceneActivation = false;

        RhythmGameManager.fadeManager.CanvasFadeOut();
        RhythmGameManager.fadeManager.fadeFinishCallback.onComplete.AddListener(CompleateLoad);
    }

    public void ChangeResultScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneSelect);
        asyncLoad.allowSceneActivation = false;

        RhythmGameManager.fadeManager.CanvasFadeOut();
        RhythmGameManager.fadeManager.fadeFinishCallback.onComplete.AddListener(CompleateLoad);
    }

    /// <summary>
    /// フェードのAnimationClipの終了検知イベント用コールバック関数
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
