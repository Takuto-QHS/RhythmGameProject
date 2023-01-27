using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string strAnimBoolName = "LoadingNow";

    public FadeFinishCallback fadeFinishCallback;

    void Start()
    {
    }

    public void CanvasFadeIn()
    {
        animator.SetBool(strAnimBoolName, false);
    }
    public void CanvasFadeOut()
    {
        animator.SetBool(strAnimBoolName, true);
    }
}
