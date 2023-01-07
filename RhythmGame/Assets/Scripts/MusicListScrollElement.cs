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

    [Space]
    [SerializeField]
    private int num = 0;

    /// <summary>
    /// �t�H�[�J�X ON/OFF �̕\���������ɋL�q����
    /// </summary>
    public override void onEffectFocus(bool focus, bool isAnimation)
    {
        imageFocus.color = new Color(imageFocus.color.r, imageFocus.color.g, imageFocus.color.b, focus == true ? 0.2f : 0.0f);

    }

    ///// <summary>
    ///// �s�̕\���X�V�ʒm���������ꍇ�A�����ŕ\�����X�V����
    ///// </summary>
    public override void onEffectChange(int itemIndex)
    {
        MusicDataParam data = (MusicDataParam)table[itemIndex];

        imageLv.sprite = data.spriteLv;
        imageMusic.sprite = data.spriteMusic;
        txtMusicName.text = data.musicData.title;
        txtMusicArtist.text = data.musicData.composer;
    }

    // OnEffectChange�͋N�����邪0Element�����ړ����Ȃ�

    // ���X�g��ԉ��ɍs���ƑS����ɖ߂����
}
