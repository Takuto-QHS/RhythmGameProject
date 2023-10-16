using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScore : MonoBehaviour
{
    [SerializeField]
    private ScriptableScoreData scrObjScore;

    [SerializeField]
    private Image imageMusic;
    [SerializeField]
    private TextMeshProUGUI txtMusicTire;
    [SerializeField]
    private TextMeshProUGUI txtMusicName;
    [SerializeField]
    private TextMeshProUGUI txtMusicArtist;
    [Space(5)]
    [SerializeField]
    private ScoreBoxWindow scoreBox;

    public void UpdateResultScoreBox()
    {
        ResultTxtMusicData(RhythmGameManager.gameManager.scrMusicData.musicDataParam);
        scoreBox.UpdateScoreBoxWindow(scrObjScore.score);
    }

    private void ResultTxtMusicData(MusicDataParam data)
    {
        imageMusic.sprite = data.spriteMusic;
        //txtMusicTire = 
        txtMusicName.text = data.musicData.name;
        txtMusicArtist.text = data.musicData.artist;
    }
}
