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
    /// Long�m�[�c��Z�œ�������(����Ȃ�����)
    /// </summary>
    public bool isMoveZ = false;

    /// <summary>
    /// �����O�m�[�c�������p
    /// </summary>
    public bool isLongNote = false;

    // Update is called once per frame
    void Update()
    {
        if (!isMove) 
        {
            return;
        }

        if(isMoveZ)
        {
            transform.position -= transform.forward * Time.deltaTime * GameSceneManager.gsManager.notesSpeed;
        }
        else
        {
            transform.position -= transform.up * Time.deltaTime * GameSceneManager.gsManager.notesSpeed;
        }
    }
}
