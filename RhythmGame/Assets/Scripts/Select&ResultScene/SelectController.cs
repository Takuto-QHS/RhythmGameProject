using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    [SerializeField]
    MusicListScrollView musicListView;

    private void OnEnable()
    {
        musicListView.Init();
    }
}
