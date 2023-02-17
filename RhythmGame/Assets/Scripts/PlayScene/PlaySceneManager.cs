using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
    public static PlaySceneManager psManager = null;
    public static int initNum = 0;

    public float waitStartTime = 1.0f;
    public float startMusicTime = 2.0f;
    public int notesSpeed = 8;
    [Space(10)]
    public int valueScore = 0;
    public int valueCombo = 0;
    public int valuePerfect = 0;
    public int valueGreat = 0;
    public int valueGood = 0;
    public int valueBad = 0;
    public int valueMiss = 0;
    [Space(10)]
    public int addScoreValuePerfect = 50;
    public int addScoreValueGreat = 40;
    public int addScoreValueGood = 20;
    public int addScoreValueBad = 10;
    public int addScoreValueMiss = 0;
    [Space(10)]
    public List<Lites> listRaneLite = new List<Lites>();
    [Space(10)]
    public NotesManager notesManager;
    public ScoreBoxWindow scoreBoxWin;
    public SoundManager soundManager;

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
        if (psManager == null)
        {
            psManager = this;
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
            // ストップウォッチ稼働時間
            playStopWatchTime += Time.deltaTime;

            // 各TapComponentStart時の生成完了待ち(AwakeだとMoveNotes関数でListが0)
            if (!isStart && playStopWatchTime >= -startMusicTime)
            {
                isStart = true;
                Play();
            }

            // BGMStart
            if (!isMusicStart && playStopWatchTime >= 0.0f)
            {
                isMusicStart = true;
                StartMusic();
            }
        }
    }

    void Init()
    {
        notesManager.Load();

        isStart = false;
        isMusicStart = false;
        playStopWatchTime -= (startMusicTime + waitStartTime);

        StopWatch(true);
    }

    void StartMusic()
    {
        soundManager.StartBGM(0);
    }

    public void Play()
    {
        StopWatch(true);
        notesManager.MoveNotes(true);
    }

    /// <summary>
    /// 「StopWatch機能」
    /// 一時停止用
    /// </summary>
    public void StopWatch(bool _isPlayTime)
    {
        isStopWatch = _isPlayTime;
    }
}
