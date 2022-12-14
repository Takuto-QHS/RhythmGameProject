using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MusicListScrollView : MonoBehaviour
{
    public ScriptableMusicData sourceData;
    public SelectBoxMusicDetail boxMusicDetail;

    private List<object> viewerList = new List<object>();
    private int selectIndex = 0;

    void Start()
    {
        TableScrollViewer viewer = this.gameObject.GetComponentInChildren<TableScrollViewer>();
        for (int i = 0; i < sourceData.musicDataList.Count; i++)
        {
            viewerList.Add(sourceData.musicDataList[i]);
        }
        viewer?.Initialize();
        viewer?.SetTable(viewerList.ToArray());
        viewer?.OnSelect.AddListener(OnSelect);
        viewer?.OnKeyDown.AddListener(OnKeyDown);
        viewer?.OnCursorMove.AddListener(onCursorMove);

        // 初めてor画面に戻って来た際の自動選択処理
        viewer?.SetSelectedIndex(selectIndex);
    }

    public void onCursorMove(List<object> table, int itemIndex, int subIndex, bool userInput)
    {
        // 選択したMusicData格納
        MusicDataParam data = (MusicDataParam)table[itemIndex];

        // 更新
        UpdateMusicDetail(data);
        RhythmGameManager.soundManager.StartBGM(data.musicData.audioClip);
    }
    public void OnSelect(List<object> table, int itemIndex, int subIndex, bool isCancel)
    {
        // 選択したMusicData格納
        MusicDataParam data = (MusicDataParam)table[itemIndex];

        // 更新
        UpdateMusicDetail(data);
        RhythmGameManager.soundManager.StartBGM(data.musicData.audioClip);
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
    void UpdateMusicDetail(MusicDataParam data)
    {
        boxMusicDetail.SelectMusicDetail(data);
    }
}
