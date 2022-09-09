using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
    public static PlaySceneManager psManager = null;
    public static int initNum = 0;

    public float preStartTime = 3.0f;
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
    }

    void Start()
    {
        StartCoroutine("StartScene");
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(preStartTime);
        soundManager.StartBGM(0);
    }
}
