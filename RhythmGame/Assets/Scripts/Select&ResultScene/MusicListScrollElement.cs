using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicListScrollElement : TableNodeElement
{
    public Image imageLv;
    public Image imageMusic;
    public TextMeshProUGUI txtMusicName;
    public TextMeshProUGUI txtMusicArtist;
    public Image imageFocus;

    /// <summary>
    /// フォーカス ON/OFF の表示をここに記述する
    /// </summary>
    public override void onEffectFocus(bool focus, bool isAnimation)
    {
        imageFocus.color = new Color(imageFocus.color.r, imageFocus.color.g, imageFocus.color.b, focus == true ? 0.2f : 0.0f);

    }

    ///// <summary>
    ///// 行の表示更新通知があった場合、ここで表示を更新する
    ///// </summary>
    public override void onEffectChange(int itemIndex)
    {
        MusicDataParam data = (MusicDataParam)table[itemIndex];

        imageLv.sprite = data.spriteLv;
        imageMusic.sprite = data.spriteMusic;
        txtMusicName.text = data.musicData.name;
        txtMusicArtist.text = data.musicData.artist;
    }

    // OnEffectChangeは起動するが0Elementだけ移動しない

    // リスト一番下に行くと全部上に戻される
}
