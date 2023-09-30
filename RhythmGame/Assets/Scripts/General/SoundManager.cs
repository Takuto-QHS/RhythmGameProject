using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private BGMController bgmController;

    private List<AudioSource> listAudioSourcesSE = new List<AudioSource>();
    private int indexAudioSourcesSE;

    [Space(2)]

    /* �ȏ�� */
    [SerializeField]
    public List<MusicData> listMusic = new List<MusicData> ();

    /* SE��� */
    [SerializeField]
    public List<AudioClip> listSE = new List<AudioClip>();

    [Space(2)]

    // ������Ă�BGM���
    public MusicData nowPlayMusicData;

    [Space(2)]

    [Header("�N���X�t�F�[�h�b��")]
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

        // SE�pAudioSource
        for (int i = 0; i < 4; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.5f;
            listAudioSourcesSE.Add(audioSource);
        }

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

    void IncrementIndexSE()
    {
        int index = indexAudioSourcesSE < 3 ? indexAudioSourcesSE + 1 : 0;
        indexAudioSourcesSE = index;
    }

    public void ChangeSceneBGM(AudioMixerSnapshot[] snapshots, float[] weights,float fadeTime)
    {
        bgmController.FadeOut(snapshots, weights, fadeTime);
    }
}
