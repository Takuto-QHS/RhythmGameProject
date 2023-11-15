using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaneTxt : MonoBehaviour
{
    static int initNum = 0;

    [SerializeField]
    private TextMeshProUGUI txtComboNum;

    private void Awake()
    {
        UpdateLaneTxtCombo(initNum);
    }

    public void UpdateLaneTxtCombo(int index)
    {
        txtComboNum.text = "COMBO\n" + index;
    }
}
