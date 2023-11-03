using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MusicDetailBox : MonoBehaviour
{
    [SerializeField]
    private Image imageMusic;
    [SerializeField]
    private TextMeshProUGUI txtMusicName;
    [SerializeField]
    private TextMeshProUGUI txtMusicArtist;
    [SerializeField]
    private Slider slider;

    public void UpdateMusicDetailBox(float musicEndSec)
    {
        imageMusic.sprite = RhythmGameManager.gameManager.scrMusicData.musicDataParam.spriteMusic;
        txtMusicName.text = RhythmGameManager.gameManager.scrMusicData.musicDataParam.musicData.name;
        txtMusicArtist.text = RhythmGameManager.gameManager.scrMusicData.musicDataParam.musicData.artist;
        slider.maxValue = musicEndSec;
    }

    public void UpdateMusicSeekBar(float value)
    {
        slider.value = value;
    }
}
