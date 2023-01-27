using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmGameManager : MonoBehaviour
{
    static public RhythmGameManager gameManager;
    static public SoundManager soundManager;
    static public FadeManager fadeManager;

    [SerializeField]
    private GameObject prefabSoundManager;
    [SerializeField]
    private GameObject prefabFadeManager;

    [Space(5)]
    [Header("ëIëã»èÓïÒ")]
    public MusicDataParam musicDataParam;

    [Space(5)]
    [Header("SoundManager")]
    public AudioMixerGroup amgSelectScene;
    public AudioMixerGroup amgResultScene;
    [Space(3)]
    public AudioMixerSnapshot snapshotSelect;
    public AudioMixerSnapshot snapshotResult;
    public AudioMixerSnapshot snapshotMute;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        if (soundManager == null)
        {
            AddSoundManager();
        }

        if (fadeManager == null)
        {
            AddFadeManager();
        }
    }

    void AddSoundManager()
    {
        GameObject _soundManager = Instantiate(prefabSoundManager, new Vector3(), Quaternion.identity, this.gameObject.transform);
        soundManager = _soundManager.GetComponent<SoundManager>();
    }
    void AddFadeManager()
    {
        GameObject _FadeManager = Instantiate(prefabFadeManager, new Vector3(), Quaternion.identity, this.gameObject.transform);
        fadeManager = _FadeManager.GetComponent<FadeManager>();
    }

}
