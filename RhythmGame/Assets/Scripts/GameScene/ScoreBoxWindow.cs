using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoxWindow : MonoBehaviour
{
    static int initNum = 0;

    [SerializeField]
    private TextMeshProUGUI txtScoreNum;
    [SerializeField]
    private TextMeshProUGUI txtMaxComboNum;
    [SerializeField]
    private TextMeshProUGUI txtPerfectNum;
    [SerializeField]
    private TextMeshProUGUI txtGreatNum;
    [SerializeField]
    private TextMeshProUGUI txtGoodNum;
    [SerializeField]
    private TextMeshProUGUI txtBadNum;
    [SerializeField]
    private TextMeshProUGUI txtMissNum;

    private void Awake()
    {
        UpdateTxtScore(initNum);
        UpdateTxtMaxCombo(initNum);
        UpdateTxtPerfect(initNum);
        UpdateTxtGreat(initNum);
        UpdateTxtGood(initNum);
        UpdateTxtBad(initNum);
        UpdateTxtMiss(initNum);
    }

    public void UpdateScoreBoxWindow(ScoreData score)
    {
        UpdateTxtScore(score.valueScore);
        UpdateTxtMaxCombo(score.valueMaxCombo);
        UpdateTxtPerfect(score.valuePerfect);
        UpdateTxtGreat(score.valueGreat);
        UpdateTxtGood(score.valueGood);
        UpdateTxtBad(score.valueBad);
        UpdateTxtMiss(score.valueMiss);
    }

    public void UpdateTxtScore(int index)
    {
        txtScoreNum.text = index.ToString();
    }

    public void UpdateTxtMaxCombo(int index)
    {
        txtMaxComboNum.text = index.ToString();
    }

    public void UpdateTxtPerfect(int index)
    {
        txtPerfectNum.text = index.ToString();
    }

    public void UpdateTxtGreat(int index)
    {
        txtGreatNum.text = index.ToString();
    }

    public void UpdateTxtGood(int index)
    {
        txtGoodNum.text = index.ToString();
    }

    public void UpdateTxtBad(int index)
    {
        txtBadNum.text = index.ToString();
    }

    public void UpdateTxtMiss(int index)
    {
        txtMissNum.text = index.ToString();
    }
}
