using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    /// <summary>
    /// 動かすかどうか
    /// </summary>
    public bool isMove = true;
    
    /// <summary>
    /// LongノーツがZで動かす為(いらないかも)
    /// </summary>
    public bool isMoveZ = false;

    /// <summary>
    /// ロングノーツ見分け用
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
