using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    private BGMController bgmController;

    private List<AudioSource> listAudioSourcesSE = new List<AudioSource>();
    private int indexAudioSourcesSE;
    private AudioSource audioLongPressSE;

    public UnityAction deligateMusicEnd;

    [Space(2)]

    /* 曲情報 */
    [SerializeField]
    public List<MusicData> listMusic = new List<MusicData> ();

    /* SE情報 */
    [SerializeField]
    public List<AudioClip> listSE = new List<AudioClip>();
    [SerializeField]
    private AudioClip clipLongPressSE;

    [Space(2)]

    // 今流れてるBGM情報
    public MusicData nowPlayMusicData;

    [Space(2)]

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

        // SE用AudioSource
        for (int i = 0; i < 4; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.volume = 1.0f;
            listAudioSourcesSE.Add(audioSource);
        }

        // LongPressSEAudioSource
        AudioSource audioSourceSE = this.gameObject.AddComponent<AudioSource>();
        audioSourceSE.volume = 1.0f;
        audioSourceSE.loop = true;
        audioLongPressSE = audioSourceSE;

        // BGMController
        bgmController = this.gameObject.AddComponent<BGMController>();
        bgmController.soundManager = this;
    }

    public void StartPlaySceneBGM()
    {
        bgmController.StartPlaySceneBGM();
    }

        public void StartListBGM(AudioClip clip,AudioMixerGroup group)
    {
        bgmController.StartListBGM(clip,group);
    }

    public void StartSE(int index)
    {
        listAudioSourcesSE[indexAudioSourcesSE].PlayOneShot(listSE[index]);
        IncrementIndexSE();
    }

    public void StartLongPressSE()
    {
        if (audioLongPressSE.isPlaying) return;

        audioLongPressSE.clip = clipLongPressSE;
        audioLongPressSE.Play();
    }

    public void StopLongPressSE()
    {
        if (!audioLongPressSE.isPlaying) return;

        audioLongPressSE.Stop();
        StartSE(0);
    }

    void IncrementIndexSE()
    {
        int index = indexAudioSourcesSE < 3 ? indexAudioSourcesSE + 1 : 0;
        indexAudioSourcesSE = index;
    }

    public void ChangeSceneBGM(AudioMixerSnapshot[] snapshots, float[] weights,float fadeTime)
    {
        bgmController.FadeOut(snapshots, weights, fadeTime);
    }

    void FinishMusic()
    {

    }
}
