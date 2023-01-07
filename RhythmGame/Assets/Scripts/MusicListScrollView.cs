using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MusicListScrollView : MonoBehaviour
{
    public ScriptableMusicData sourceData;

    List<object> viewerList = new List<object>();

    void Start()
    {
        TableScrollViewer viewer = this.gameObject.GetComponentInChildren<TableScrollViewer>();
        for (int i = 0; i < sourceData.musicDataList.Count; i++)
        {
            viewerList.Add(sourceData.musicDataList[i]);
        }
        //for (int i = 0; i < 16; i++)
        //{
        //    viewerList.Add(i);
        //}
        viewer?.Initialize();
        viewer?.SetTable(viewerList.ToArray());
        viewer?.OnSelect.AddListener(OnSelect);
        viewer?.OnKeyDown.AddListener(OnKeyDown);
        viewer?.OnCursorMove.AddListener(onCursorMove);
    }

    public void onCursorMove(List<object> table, int itemIndex, int subIndex, bool userInput)
    {
        int row = (int)table[itemIndex];
        Debug.Log($"move: {row + 1}");
    }
    public void OnSelect(List<object> table, int itemIndex, int subIndex, bool isCancel)
    {
        int row = (int)table[itemIndex];
        Debug.Log($"selected : {row + 1}");
    }
    public void OnKeyDown(TableScrollViewer.KeyDownArgs args)
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            args.Flag = TableScrollViewer.eKeyMoveFlag.Select;
        }
        else
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            args.Flag = TableScrollViewer.eKeyMoveFlag.Up;
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            args.Flag = TableScrollViewer.eKeyMoveFlag.Down;
        }
    }
}
