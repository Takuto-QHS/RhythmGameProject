using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameManager : MonoBehaviour
{
    static public RhythmGameManager gameManager;
    static public SoundManager soundManager;

    [SerializeField]
    private GameObject prefabSoundManager;

    [Space(5)]
    [Header("‘I‘ğ‹Èî•ñ")]
    public MusicDataParam musicDataParam;

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
    }

    void AddSoundManager()
    {
        GameObject _soundManager = Instantiate(prefabSoundManager, new Vector3(), Quaternion.identity, this.gameObject.transform);
        soundManager = _soundManager.GetComponent<SoundManager>();
    }


}
