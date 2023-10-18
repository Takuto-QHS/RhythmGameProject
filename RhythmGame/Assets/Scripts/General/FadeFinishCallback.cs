using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeFinishCallback : MonoBehaviour
{
    public UnityEvent onComplete = new UnityEvent();

    /// <summary>
    /// FadeOutAnim�̃t�F�[�h�I���ɃC�x���gClip���d����ŋN��
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
        /* UnityDocument������p */
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
