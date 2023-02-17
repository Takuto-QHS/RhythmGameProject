using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    /// <summary>
    /// ���������ǂ���
    /// </summary>
    public bool isMove = true;
    
    /// <summary>
    /// Long�m�[�c��Z�œ�������
    /// </summary>
    public bool isMoveZ = false;

    // Update is called once per frame
    void Update()
    {
        if (!isMove) 
        {
            return;
        }

        if(isMoveZ)
        {
            transform.position -= transform.forward * Time.deltaTime * PlaySceneManager.psManager.notesSpeed;
        }
        else
        {
            transform.position -= transform.up * Time.deltaTime * PlaySceneManager.psManager.notesSpeed;
        }
    }
}
