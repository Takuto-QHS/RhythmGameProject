using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeFinishCallback : MonoBehaviour
{
    public UnityEvent onComplete = new UnityEvent();

    public void CompleateFadeAnimation()
    {
        bool isSeceneCompleate = RhythmGameManager.sceneManager.GetCompleateLoad();

        if (isSeceneCompleate)
        {
            onComplete.Invoke();
        }
        else
        {
            StartCoroutine(WaitScene());
        }
    }

    private IEnumerator WaitScene()
    {
        /* UnityDocument‚©‚çˆø—p */
        while (true)
        {
            yield return null;
            bool isSeceneCompleate = RhythmGameManager.sceneManager.GetCompleateLoad();

            if (isSeceneCompleate)
            {
                onComplete.Invoke();
                break;
            }
        }
    }
}
