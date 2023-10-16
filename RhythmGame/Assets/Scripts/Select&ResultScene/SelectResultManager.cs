using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera�ړ���
/// Select��ʂ̍ŏ��̑I���Ȃ�����Ȃ��悤��
/// SetActive�Ő��䂵�Ă܂�
/// </summary>

public class SelectResultManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] selectObj;
    [SerializeField]
    private GameObject resultObj;

    void Start()
    {
        ChangeState(RhythmGameManager.sceneManager.sceneState);
    }

    public void ChangeState(RhythmSceneManager.SCENE_STATE state)
    {
        foreach (GameObject obj in selectObj)
        {
            obj.SetActive(state == RhythmSceneManager.SCENE_STATE.SELECT);
        }

        resultObj.SetActive(state != RhythmSceneManager.SCENE_STATE.SELECT);
    }
}
