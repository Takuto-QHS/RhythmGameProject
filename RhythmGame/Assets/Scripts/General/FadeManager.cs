using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���Ƀt�F�[�h�C��������̂�Animator�������Ă��
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
