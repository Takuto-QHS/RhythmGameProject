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

    public enum BGM_LOOP_STATE
    {
        FADE_IN,
        LOOP,
        FADE_OUT,
    }

    [SerializeField]
    private BGM_STATE _bgm_state;

    private AudioSource audioSourceBGM;

    private AudioMixer audioMixer;

    // �t�F�[�h����
    [SerializeField]
    private float fadeInTime = 1.0f;
    [SerializeField]
    private float fadeOutTime = 1.0f;

    private bool isFade = false;

    [SerializeField]
    private float loopTimer;
    private float loopStartTime = 0.0f;
    private float loopFinishTime = 8.0f;
    [SerializeField]
    private BGM_LOOP_STATE _loop_bgm_state = BGM_LOOP_STATE.LOOP;

    void Start()
    {
        _bgm_state = BGM_STATE.WAIT;

        // BGM�pAudioSource
        audioSourceBGM = this.gameObject.AddComponent<AudioSource>();
        audioSourceBGM.volume = 0.5f;

        // AudioMixer
        audioMixer = RhythmGameManager.gameManager.amgSelectScene.audioMixer;
    }

    void Update()
    {
        switch (RhythmGameManager.sceneManager.sceneState)
        {
            case RhythmSceneManager.SCENE_STATE.SELECT:
                UpdateSelectProcess();
                break;

            case RhythmSceneManager.SCENE_STATE.PLAY:
                UpdatePlayProcess();
                break;
        }
    }

    void UpdateSelectProcess()
    {
        loopTimer += Time.deltaTime;

        switch (_loop_bgm_state)
        {
            case BGM_LOOP_STATE.FADE_IN:
                if (loopTimer >= loopStartTime + 1.0f)
                {
                    _loop_bgm_state = BGM_LOOP_STATE.LOOP;
                }
                break;

            case BGM_LOOP_STATE.LOOP:
                if (loopTimer >= loopFinishTime - 1.0f)
                {
                    _loop_bgm_state = BGM_LOOP_STATE.FADE_OUT;

                    // FadeOut
                    AudioMixerSnapshot[] snapshots = {
                            RhythmGameManager.gameManager.snapshotSelect,
                            RhythmGameManager.gameManager.snapshotMute
                    };
                    float[] weights = { 0.0f, 1.0f };
                    audioMixer.TransitionToSnapshots(snapshots, weights, 2.0f);
                }
                break;

            case BGM_LOOP_STATE.FADE_OUT:
                if (loopTimer >= loopFinishTime)
                {
                    loopTimer = 0.0f;
                    audioSourceBGM.time = loopStartTime;
                    _loop_bgm_state = BGM_LOOP_STATE.FADE_IN;

                    //FadeIn
                    AudioMixerSnapshot[] snapshots = {
                        RhythmGameManager.gameManager.snapshotMute,
                        RhythmGameManager.gameManager.snapshotSelect
                    };
                    float[] weights = { 0.0f, 1.0f };
                    audioMixer.TransitionToSnapshots(snapshots, weights, 2.0f);
                }
                break;
        }
    }

    void UpdatePlayProcess()
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
                if (soundManager.nowPlayMusicData.audioClip.length - audioSourceBGM.time <= fadeOutTime)
                {
                    _bgm_state = BGM_STATE.FADE_OUT;
                }
                break;

            // �t�F�[�h�A�E�g����
            case BGM_STATE.FADE_OUT:

                if (!isFade)
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
                if (!audioSourceBGM.isPlaying)
                {
                    _bgm_state = BGM_STATE.END;

                    isFade = false;
                    Stop();
                    soundManager.deligateMusicEnd();
                }
                break;

            case BGM_STATE.END:
                break;
        }
    }

    // �v���C�V�[���ŉ������X�^�[�g�����鏈���i�ŏ��Ƀt�F�[�h�C�������Ȃ��j
    public void StartPlaySceneBGM()
    {
        RhythmGameManager.soundManager.nowPlayMusicData = RhythmGameManager.gameManager.scrMusicData.musicDataParam.musicData;

        audioSourceBGM.clip = RhythmGameManager.soundManager.nowPlayMusicData.audioClip;
        audioSourceBGM.outputAudioMixerGroup = RhythmGameManager.gameManager.amgSelectScene;
        audioSourceBGM.Play();

        _bgm_state = BGM_STATE.NOW_PLAY;
    }

    // �ȑI�����̏����i�ŏ��Ƀt�F�[�h�C��������j
    public void StartSelectListBGM(AudioClip clip, AudioMixerGroup group)
    {
        audioSourceBGM.clip = clip;
        audioSourceBGM.outputAudioMixerGroup = group;
        audioSourceBGM.Play();

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

    public void Stop()
    {
        audioSourceBGM.Stop();
    }

    public void SnapshotTo()
    {
        AudioMixerSnapshot[] snapshots =
            {
            RhythmGameManager.gameManager.snapshotMute,
            RhythmGameManager.gameManager.snapshotSelect
        };
        float[] weights = { 0.0f, 1.0f };
        audioMixer.TransitionToSnapshots(snapshots, weights, 0.0f);
    }
}
