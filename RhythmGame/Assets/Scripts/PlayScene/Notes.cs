using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public bool isMoveZ = false;

    // Update is called once per frame
    void Update()
    {
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
