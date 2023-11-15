using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddScoreData
{
    public float addScoreValuePerfect = 50;
    public float addScoreValueGreat = 40;
    public float addScoreValueGood = 20;
    public float addScoreValueBad = 10;
    public float addScoreValueMiss = 0;
}

public class GameSceneManager : MonoBehaviour
{
    [Space(10)]
    public static GameSceneManager gsManager = null;
    public static int initNum = 0;

    public float waitStartTime = 1.0f;
    public float startMusicTime = 2.0f;
    public int notesSpeed = 8;
    public int maxNotes;
    [Space(10)]
    public float valueScore = 0;
    public int valueCombo = 0;
    public int valueMaxCombo = 0;
    public int valuePerfect = 0;
    public int valueGreat = 0;
    public int valueGood = 0;
    public int valueBad = 0;
    public int valueMiss = 0;
    [Space(10)]
    public AddScoreData addScoreData;
    [Space(10)]
    public List<Lites> listRaneLite = new List<Lites>();
    [Space(10)]
    public NotesManager notesManager;
    [Space(10)]

    [SerializeField]
    private MusicDetailBox musicDetailBox;
    [SerializeField]
    private ScriptableScoreData scrObjScore;

    [HideInInspector]
    public SoundManager soundManager;
    [HideInInspector]
    public InputPlayScene inputPlayScene;

    [Space(20), Header("StopWatch")]
    /// <summary>
    /// StopWatch
    /// </summary>
    public float playStopWatchTime = 0.0f;
    private bool isStopWatch = false;

    [SerializeField]
    private bool isStart = false;
    [SerializeField]
    private bool isMusicStart = false;

    public void Awake()
    {
        if (gsManager == null)
        {
            gsManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Init();
    }

    void Update()
    {
        if (isStopWatch)
        {
            // �X�g�b�v�E�H�b�`�ғ�����
            playStopWatchTime += Time.deltaTime;

            // �eTapComponentStart���̐��������҂�(Awake����MoveNotes�֐���List��0)
            if (!isStart && playStopWatchTime >= -startMusicTime)
            {
                isStart = true;
                ProcessAddScore();
                Play();
            }
            // BGMStart
            else if (!isMusicStart && playStopWatchTime >= 0.0f)
            {
                isMusicStart = true;

                soundManager.InitSnapshot();
                soundManager.StartPlaySceneBGM();
                musicDetailBox.UpdateMusicSeekBar(playStopWatchTime);
            }
            // GamePlay��
            else if (isMusicStart && playStopWatchTime <= RhythmGameManager.gameManager.scrMusicData.musicDataParam.musicData.audioClip.length)
            {
                musicDetailBox.UpdateMusicSeekBar(playStopWatchTime);
            }
        }
    }

    void Init()
    {
        inputPlayScene = this.gameObject.AddComponent<InputPlayScene>();

        RhythmGameManager.sceneManager.sceneState = RhythmSceneManager.SCENE_STATE.PLAY;

        soundManager = RhythmGameManager.soundManager;
        notesManager.Load();

        musicDetailBox.UpdateMusicDetailBox
            (RhythmGameManager.gameManager.scrMusicData.musicDataParam.musicData.audioClip.length);

        isStart = false;
        isMusicStart = false;
        playStopWatchTime -= (startMusicTime + waitStartTime);

        StopWatch(true);

        soundManager.deligateMusicEnd = EndMusic;
    }

    public void Play()
    {
        StopWatch(true);
        notesManager.MoveNotes(true);
    }

    /// <summary>
    /// �uStopWatch�@�\�v
    /// �ꎞ��~�p
    /// </summary>
    public void StopWatch(bool _isPlayTime)
    {
        isStopWatch = _isPlayTime;
    }

    /// <summary>
    /// ���y���Ō�܂Ŗ�I�������
    /// </summary>
    public void EndMusic()
    {
        // �X�R�A�ێ�����
        scrObjScore.score.valueScore    = valueScore;
        scrObjScore.score.valueMaxCombo = valueMaxCombo;
        scrObjScore.score.valuePerfect  = valuePerfect;
        scrObjScore.score.valueGreat    = valueGreat;
        scrObjScore.score.valueGood     = valueGood;
        scrObjScore.score.valueBad      = valueBad;
        scrObjScore.score.valueMiss     = valueMiss;

        // Result��ʂ�
        RhythmGameManager.sceneManager.ChangeResultScene();
    }

    public void UpdateMaxCombo()
    {
        if (valueCombo < valueMaxCombo) return;
        valueMaxCombo = valueCombo;
    }

    /// <summary>
    /// �S�m�[�c������1�m�[�c�̃X�R�A���Z�o
    /// (10000�Ƃ��̒l��ʂ̏ꏊ�Ŏw�肵�Ă������ق�����Փx�ʂŕς���ĕ֗��ȋC������)
    /// </summary>
    private void ProcessAddScore()
    {
        float addPerfect = (float)10000 / maxNotes;
        addScoreData.addScoreValuePerfect = addPerfect;
        float addGreat = (float)6000 / maxNotes;
        addScoreData.addScoreValueGreat = addGreat;
        float addGood = (float)4000 / maxNotes;
        addScoreData.addScoreValueGood = addGood;
        float addBad = (float)2000 / maxNotes;
        addScoreData.addScoreValueBad = addBad;
    }
}
