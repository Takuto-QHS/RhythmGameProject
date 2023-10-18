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

    // BGM状態管理
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

    private AudioSource audioSourceBGM;

    private AudioMixer audioMixer;

    // フェード時間
    [SerializeField]
    private float fadeInTime = 1.0f;
    [SerializeField]
    private float fadeOutTime = 1.0f;

    private bool isFade = false;

    void Start()
    {
        _bgm_state = BGM_STATE.WAIT;

        // BGM用AudioSource
        audioSourceBGM = this.gameObject.AddComponent<AudioSource>();
        audioSourceBGM.volume = 0.5f;

        // AudioMixer
        audioMixer = RhythmGameManager.gameManager.amgSelectScene.audioMixer;
    }

    // Update is called once per frame
    void Update()
    {
        switch (RhythmGameManager.sceneManager.sceneState)
        {
            case RhythmSceneManager.SCENE_STATE.PLAY:
                UpdatePlayProcess();
                break;
        }
    }

    void UpdatePlayProcess()
    {
        switch (_bgm_state)
        {
            // 何もしてない時
            case BGM_STATE.WAIT:
                break;

            // フェードイン処理
            case BGM_STATE.FADE_IN:

                //フェードイン処理が終わったか
                _bgm_state = BGM_STATE.NOW_PLAY;
                break;

            // 通常再生中処理
            case BGM_STATE.NOW_PLAY:

                // 曲の終わりの時はフェードアウト
                if (soundManager.nowPlayMusicData.audioClip.length - audioSourceBGM.time <= fadeOutTime)
                {
                    _bgm_state = BGM_STATE.FADE_OUT;
                }
                break;

            // フェードアウト処理
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

                // フェードアウト終了時、処理があれば入る
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

    // 選択中の音源ループ処理
    void SelectProcess()
    {

    }

    // 最初にフェードインさせない
    public void StartPlaySceneBGM()
    {
        RhythmGameManager.soundManager.nowPlayMusicData = RhythmGameManager.gameManager.scrMusicData.musicDataParam.musicData;

        audioSourceBGM.clip = RhythmGameManager.soundManager.nowPlayMusicData.audioClip;
        audioSourceBGM.outputAudioMixerGroup = RhythmGameManager.gameManager.amgSelectScene;
        audioSourceBGM.Play();

        _bgm_state = BGM_STATE.NOW_PLAY;
    }

    // 最初にフェードインさせる
    public void StartListBGM(AudioClip clip, AudioMixerGroup group)
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
