using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoxWindow : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        UpdateTxtScore(PlaySceneManager.initNum);
        UpdateTxtCombo(PlaySceneManager.initNum);
        UpdateTxtPerfect(PlaySceneManager.initNum);
        UpdateTxtGreat(PlaySceneManager.initNum);
        UpdateTxtGood(PlaySceneManager.initNum);
        UpdateTxtBad(PlaySceneManager.initNum);
        UpdateTxtMiss(PlaySceneManager.initNum);
    }

    public void UpdateTxtScore(int index)
    {
        txtScoreNum.text = PlaySceneManager.psManager.valueScore.ToString();
    }

    public void UpdateTxtCombo(int index)
    {
        txtMaxComboNum.text = PlaySceneManager.psManager.valueCombo.ToString();
    }

    public void UpdateTxtPerfect(int index)
    {
        txtPerfectNum.text = PlaySceneManager.psManager.valuePerfect.ToString();
    }

    public void UpdateTxtGreat(int index)
    {
        txtGreatNum.text = PlaySceneManager.psManager.valueGreat.ToString();
    }

    public void UpdateTxtGood(int index)
    {
        txtGoodNum.text = PlaySceneManager.psManager.valueGood.ToString();
    }

    public void UpdateTxtBad(int index)
    {
        txtBadNum.text = PlaySceneManager.psManager.valueBad.ToString();
    }

    public void UpdateTxtMiss(int index)
    {
        txtMissNum.text = PlaySceneManager.psManager.valueMiss.ToString();
    }
}
