using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [Space(10)]
    public static GameSceneManager gsManager = null;
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
                Play();
            }

            // BGMStart
            if (!isMusicStart && playStopWatchTime >= 0.0f)
            {
                isMusicStart = true;

                soundManager.InitSnapshot();
                soundManager.StartPlaySceneBGM();
            }
        }
    }

    void Init()
    {
        inputPlayScene = this.gameObject.AddComponent<InputPlayScene>();

        soundManager = RhythmGameManager.soundManager;
        notesManager.Load();

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
        scrObjScore.score.valueMaxCombo = 1000;
        scrObjScore.score.valuePerfect  = valuePerfect;
        scrObjScore.score.valueGreat    = valueGreat;
        scrObjScore.score.valueGood     = valueGood;
        scrObjScore.score.valueBad      = valueBad;
        scrObjScore.score.valueMiss     = valueMiss;

        // Result��ʂ�
        RhythmGameManager.sceneManager.ChangeResultScene();
    }
}
