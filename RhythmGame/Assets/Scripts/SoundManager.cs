using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MusicData
{
    public string name;
    public string artist;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    private AudioSource listAudioSourcesBGM;
    [SerializeField]
    private List<AudioSource> listAudioSourcesSE = new List<AudioSource>();
    private int indexAudioSourcesSE;

    [SerializeField]
    private List<MusicData> listMusic = new List<MusicData> ();
    [SerializeField]
    private List<AudioClip> listSE = new List<AudioClip>();


    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        listAudioSourcesBGM = this.gameObject.AddComponent<AudioSource>();
        listAudioSourcesBGM.volume = 0.5f;

        for (int i = 0; i < 4; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.5f;
            listAudioSourcesSE.Add(audioSource);
        }
    }

    public void StartBGM(int index)
    {
        listAudioSourcesBGM.clip = listMusic[index].audioClip;
        listAudioSourcesBGM.Play();
    }
    public void StartBGM(AudioClip clip)
    {
        listAudioSourcesBGM.clip = clip;
        listAudioSourcesBGM.Play();
    }

    public void StartSE(int index)
    {
        listAudioSourcesSE[indexAudioSourcesSE].PlayOneShot(listSE[index]);
        IncrementIndexSE();
    }

    void IncrementIndexSE()
    {
        int index = indexAudioSourcesSE < 3 ? indexAudioSourcesSE + 1 : 0;
        indexAudioSourcesSE = index;
    }
}
