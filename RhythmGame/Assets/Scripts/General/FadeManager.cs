using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイ時にフェードインが入るのはAnimatorが働いてる為
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string strAnimBoolName = "LoadingNow";

    public FadeFinishCallback fadeFinishCallback;

    public void CanvasFadeIn()
    {
        animator.SetBool(strAnimBoolName, false);
    }
    public void CanvasFadeOut()
    {
        animator.SetBool(strAnimBoolName, true);
    }
}
