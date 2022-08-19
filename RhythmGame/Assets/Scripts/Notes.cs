using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * PlaySceneManager.psManager.notesSpeed;
    }
}
