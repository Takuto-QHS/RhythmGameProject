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

public class BGMController : MonoBehaviour
{
    public SoundManager soundManager;

    // BGM��ԊǗ�
    public enum BGM_STATE
    {
        WAIT,
        FADE_IN,
        NOW_PLAY,
        FADE_OUT,
        PAUSE,
        END,
    }

    [SerializeField]
    private BGM_STATE _bgm_state;

    private AudioSource listAudioSourcesBGM;

    private AudioMixer audioMixer;

    // �t�F�[�h����
    [SerializeField]
    private float fadeInTime = 1.0f;
    [SerializeField]
    private float fadeOutTime = 1.0f;

    private bool isFade = false;

    void Start()
    {
        _bgm_state = BGM_STATE.WAIT;

        // BGM�pAudioSource
        listAudioSourcesBGM = this.gameObject.AddComponent<AudioSource>();
        listAudioSourcesBGM.volume = 0.5f;

        // AudioMixer
        audioMixer = RhythmGameManager.gameManager.amgSelectScene.audioMixer;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_bgm_state)
        {
            // �������ĂȂ���
            case BGM_STATE.WAIT:
                break;

            // �t�F�[�h�C������
            case BGM_STATE.FADE_IN:

                //�t�F�[�h�C���������I�������
                _bgm_state = BGM_STATE.NOW_PLAY;
                break;

            // �ʏ�Đ�������
            case BGM_STATE.NOW_PLAY:

                // �Ȃ̏I���̎��̓t�F�[�h�A�E�g
                if (soundManager.nowPlayMusicData.audioClip.length - listAudioSourcesBGM.time <= fadeOutTime)
                {
                    _bgm_state = BGM_STATE.FADE_OUT;
                }
                break;

            // �t�F�[�h�A�E�g����
            case BGM_STATE.FADE_OUT:

                if(!isFade)
                {
                    AudioMixerSnapshot[] snapshots =
                        {
                        RhythmGameManager.gameManager.snapshotSelect,
                        RhythmGameManager.gameManager.snapshotMute
                    };
                    float[] weights = { 0.0f, 1.0f };

                    isFade = true;
                    FadeOut(snapshots, weights, fadeOutTime);
                }

                // �t�F�[�h�A�E�g�I�����A����������Γ���
                if (!listAudioSourcesBGM.isPlaying)
                {
                    _bgm_state = BGM_STATE.END;

                    isFade = false;
                    soundManager.deligateMusicEnd();
                }
                break;

            case BGM_STATE.END:
                break;
        }
    }

    // �ŏ��Ƀt�F�[�h�C�������Ȃ�
    public void StartPlaySceneBGM()
    {
        RhythmGameManager.soundManager.nowPlayMusicData = RhythmGameManager.gameManager.musicDataParam.musicData;

        listAudioSourcesBGM.clip = RhythmGameManager.soundManager.nowPlayMusicData.audioClip;
        listAudioSourcesBGM.outputAudioMixerGroup = RhythmGameManager.gameManager.amgSelectScene;
        listAudioSourcesBGM.Play();

        _bgm_state = BGM_STATE.NOW_PLAY;
    }

    // �ŏ��Ƀt�F�[�h�C��������
    public void StartListBGM(AudioClip clip, AudioMixerGroup group)
    {
        listAudioSourcesBGM.clip = clip;
        listAudioSourcesBGM.outputAudioMixerGroup = group;
        listAudioSourcesBGM.Play();

        AudioMixerSnapshot[] snapshots =
            {
            RhythmGameManager.gameManager.snapshotMute,
            RhythmGameManager.gameManager.snapshotSelect
        };
        float[] weights = { 0.0f, 1.0f };

        FadeIn(snapshots, weights, fadeInTime);
    }

    public void FadeOut(AudioMixerSnapshot[] snapshots, float[] weights, float fadeTime)
    {
        _bgm_state = BGM_STATE.FADE_OUT;
        audioMixer.TransitionToSnapshots(snapshots, weights, fadeTime);
    }

    public void FadeIn(AudioMixerSnapshot[] snapshots, float[] weights, float fadeTime)
    {
        _bgm_state = BGM_STATE.FADE_IN;
        audioMixer.TransitionToSnapshots(snapshots, weights, fadeTime);
    }
}
