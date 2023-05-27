using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScore : MonoBehaviour
{
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
    private TextMeshProUGUI txtTotalScore;
    [SerializeField]
    private TextMeshProUGUI txtMaxCombo;
    [SerializeField]
    private TextMeshProUGUI txtPerfect;
    [SerializeField]
    private TextMeshProUGUI txtGreat;
    [SerializeField]
    private TextMeshProUGUI txtGood;
    [SerializeField]
    private TextMeshProUGUI txtBad;
    [SerializeField]
    private TextMeshProUGUI txtMiss;

    void Start()
    {
        
    }

    public void ResultTxtTotalScore(int index)
    {
        txtTotalScore.text = PlaySceneManager.psManager.valueScore.ToString();
    }
}
