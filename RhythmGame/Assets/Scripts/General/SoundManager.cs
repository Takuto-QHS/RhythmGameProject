using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

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

    [Space(2)]

    [SerializeField]
    private List<MusicData> listMusic = new List<MusicData> ();
    [SerializeField]
    private List<AudioClip> listSE = new List<AudioClip>();

    [Space(2)]
    private AudioMixer am;

    [Header("クロスフェード秒数")]
    public float secSelectSene = 0.5f;
    public float secResultSene = 0.5f;
    public float secMute = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        am = RhythmGameManager.gameManager.amgSelectScene.audioMixer;

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
    public void StartBGM(AudioClip clip,AudioMixerGroup group)
    {
        listAudioSourcesBGM.clip = clip;
        listAudioSourcesBGM.outputAudioMixerGroup= group;
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

    public void ChangeSceneBGM(AudioMixerSnapshot[] snapshots, float[] weights,float fadeTime)
    {
        am.TransitionToSnapshots(snapshots,weights,fadeTime);
    }
}
