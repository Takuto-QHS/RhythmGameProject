using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera移動と
/// Select画面の最初の選択曲が流れないように
/// SetActiveで制御してます
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
