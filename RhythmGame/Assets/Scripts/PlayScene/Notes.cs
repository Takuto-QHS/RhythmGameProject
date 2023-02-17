using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    /// <summary>
    /// “®‚©‚·‚©‚Ç‚¤‚©
    /// </summary>
    public bool isMove = true;
    
    /// <summary>
    /// Longƒm[ƒc‚ªZ‚Å“®‚©‚·ˆ×
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
