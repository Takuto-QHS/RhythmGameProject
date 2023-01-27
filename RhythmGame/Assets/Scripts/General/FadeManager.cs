using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    string strAnimBoolName = "LoadingNow";

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
