using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeFinishCallback : MonoBehaviour
{
    public UnityEvent onComplete = new UnityEvent();

    /// <summary>
    /// FadeOutAnimのフェード終わりにイベントClipを仕込んで起動
    /// </summary>
    public void CompleateFadeAnimation()
    {
        bool isSeceneCompleate = RhythmGameManager.sceneManager.GetCompleateLoad();

        if (isSeceneCompleate)
        {
            onComplete.Invoke();
            onComplete.RemoveAllListeners();
        }
        else
        {
            StartCoroutine(WaitScene());
        }
    }

    private IEnumerator WaitScene()
    {
        /* UnityDocumentから引用 */
        while (true)
        {
            yield return null;
            bool isSeceneCompleate = RhythmGameManager.sceneManager.GetCompleateLoad();

            if (isSeceneCompleate)
            {
                onComplete.Invoke();
                onComplete.RemoveAllListeners();
                break;
            }
        }
    }
}
