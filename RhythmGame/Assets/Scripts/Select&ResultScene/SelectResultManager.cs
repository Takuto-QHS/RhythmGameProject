using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CameraˆÚ“®‚Æ
/// Select‰æ–Ê‚ÌÅ‰‚Ì‘I‘ğ‹È‚ª—¬‚ê‚È‚¢‚æ‚¤‚É
/// SetActive‚Å§Œä‚µ‚Ä‚Ü‚·
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
