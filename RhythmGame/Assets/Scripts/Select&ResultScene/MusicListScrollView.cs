using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Audio;

public class MusicListScrollView : MonoBehaviour
{
    public ScriptableMusicListData sourceData;
    public SelectBoxMusicDetail boxMusicDetail;

    private List<object> viewerList = new List<object>();
    private int selectIndex = 0;
    private bool isEnter = false;

    public void Init()
    {
        viewerList.Clear();
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

        // ���߂�or��ʂɖ߂��ė����ۂ̎����I������
        viewer?.SetSelectedIndex(selectIndex);
    }

    /// <summary>
    /// �I�����̊֐�
    /// </summary>
    public void onCursorMove(List<object> table, int itemIndex, int subIndex, bool userInput)
    {
        // �I������MusicData�i�[
        MusicDataParam data = (MusicDataParam)table[itemIndex];

        // �X�V
        UpdateMusicDetail(data);
        UpdateSelectMusic(data);
        RhythmGameManager.soundManager.StartSelectListBGM(data.musicData.audioClip,RhythmGameManager.gameManager.amgSelectScene);
    }

    /// <summary>
    /// �I����A���莞�̊֐�
    /// </summary>
    public void OnSelect(List<object> table, int itemIndex, int subIndex, bool isCancel)
    {
        // 2��ڈȍ~�A���̊֐��ɕ���������Ă�̂�
        if (isEnter) return;
        isEnter = true;

        // �I������MusicData�i�[
        MusicDataParam data = (MusicDataParam)table[itemIndex];
        RhythmGameManager.gameManager.scrMusicData.musicDataParam = data;

        RhythmGameManager.soundManager.PlayDecisionTapSE();

        // �V�[���`�F���W����
        EnterTransitionBGM(RhythmGameManager.soundManager.secMute);
        RhythmGameManager.sceneManager.ChangePlayScene();
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
    void UpdateSelectMusic(MusicDataParam param)
    {
        RhythmGameManager.gameManager.scrMusicData.musicDataParam = param;
        RhythmGameManager.soundManager.nowPlayMusicData = param.musicData;
    }
    void EnterTransitionBGM(float sec)
    {
        AudioMixerSnapshot[] snapshots =
            {
            RhythmGameManager.gameManager.snapshotSelect,
            RhythmGameManager.gameManager.snapshotMute
        };
        float[] weights = { 0.0f, 1.0f };

        RhythmGameManager.soundManager.ChangeSceneBGM(snapshots,weights,sec);
    }
}
