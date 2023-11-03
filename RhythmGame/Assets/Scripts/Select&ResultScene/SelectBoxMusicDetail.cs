using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectBoxMusicDetail : MonoBehaviour
{
    [SerializeField]
    private Image imageMusic;
    [SerializeField]
    private TextMeshProUGUI txtMusicName;
    [SerializeField]
    private TextMeshProUGUI txtMusicArtist;

    public void SelectMusicDetail(MusicDataParam data)
    {
        if (data == null) return;

        imageMusic.sprite = data.spriteMusic;
        txtMusicName.text = data.musicData.name;
        txtMusicArtist.text = data.musicData.artist;
    }
}
