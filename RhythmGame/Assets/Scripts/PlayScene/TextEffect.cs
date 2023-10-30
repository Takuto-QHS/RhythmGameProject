using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DestroyTextEffect()
/// ・Effect_JudgeTextAnimationのイベントクリップで起動
/// </summary>

public class TextEffect : MonoBehaviour
{
    public void DestroyTextEffect()
    {
        Destroy(gameObject);
    }
}
