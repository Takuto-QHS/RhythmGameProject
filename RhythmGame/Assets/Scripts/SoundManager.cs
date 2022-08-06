using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip> ();

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartBGM(0);
    }

    void Init()
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        audioSources.Add(audioSource);
    }

    void StartBGM(int index)
    {
        audioSources[0].clip = audioClips[index];
        audioSources[0].Play();
    }
}
