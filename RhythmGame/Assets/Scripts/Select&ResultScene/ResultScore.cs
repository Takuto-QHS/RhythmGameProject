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
    private Image imageRank;
    [SerializeField]
    private Image imageLv;
    [SerializeField]
    private Image imageMusic;
    [SerializeField]
    private TextMeshProUGUI txtMusicName;
    [SerializeField]
    private TextMeshProUGUI txtMusicArtist;
    [Space(5)]
    [SerializeField]
    private ScoreBoxWindow scoreBox;
    [Space(5)]
    [SerializeField]
    private int valueRankScoreS;
    [SerializeField]
    private int valueRankScoreA;
    [SerializeField]
    private int valueRankScoreB;
    [SerializeField]
    private ScriptableRankData scrRankData;

    public void UpdateResultScoreBox()
    {
        ResultTxtMusicData(RhythmGameManager.gameManager.scrMusicData.musicDataParam);
        scoreBox.UpdateScoreBoxWindow(scrObjScore.score);
        UpdateRankImage();
    }

    private void ResultTxtMusicData(MusicDataParam data)
    {
        imageMusic.sprite = data.spriteMusic;
        imageLv.sprite = data.spriteLv;
        txtMusicName.text = data.musicData.name;
        txtMusicArtist.text = data.musicData.artist;
    }

    private void UpdateRankImage()
    {
        Sprite sprite;

        // Score‚ğŒ³‚ÉRank‰æ‘œ‚ğ‘I‘ğ
        if(scrObjScore.score.valueScore >= valueRankScoreS)
        {
            sprite = scrRankData.spriteRankS;
        }
        else if(scrObjScore.score.valueScore >= valueRankScoreA)
        {
            sprite = scrRankData.spriteRankA;
        }
        else if (scrObjScore.score.valueScore >= valueRankScoreB)
        {
            sprite = scrRankData.spriteRankB;
        }
        else
        {
            sprite = scrRankData.spriteRankC;
        }

        // Rank‰æ‘œ‚ğXV
        imageRank.sprite = sprite;
    }
}
