using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmGameManager : MonoBehaviour
{
    static public RhythmGameManager gameManager;
    static public SoundManager soundManager;
    static public FadeManager fadeManager;
    static public RhythmSceneManager sceneManager;
    static public InputManager inputManager;

    [Space(10)]
    [Header("ê∂ê¨ópManagerPrefab")]
    [SerializeField]
    private GameObject prefabSoundManager;
    [SerializeField]
    private GameObject prefabFadeManager;
    [SerializeField]
    private GameObject prefabSceneManager;
    [SerializeField]
    private GameObject prefabInputManager;

    [Space(5)]
    [Header("ëIëã»èÓïÒ")]
    public ScriptableMusicData scrMusicData;

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
        if (RhythmGameManager.gameManager == null)
        {
            RhythmGameManager.gameManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (soundManager == null)
        {
            AddSoundManager();
        }

        if (fadeManager == null)
        {
            AddFadeManager();
        }

        if (sceneManager == null)
        {
            AddSceneManager();
        }

        if(inputManager == null)
        {
            AddInputManager();
        }

        DontDestroyOnLoad(gameObject);
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
    void AddSceneManager()
    {
        GameObject _SceneManager = Instantiate(prefabSceneManager, new Vector3(), Quaternion.identity, this.gameObject.transform);
        sceneManager = _SceneManager.GetComponent<RhythmSceneManager>();
    }

    void AddInputManager()
    {
        GameObject _InputManager = Instantiate(prefabInputManager, new Vector3(), Quaternion.identity, this.gameObject.transform);
        inputManager = _InputManager.GetComponent<InputManager>();
    }

}
