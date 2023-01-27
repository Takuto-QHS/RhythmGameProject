using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;

}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public Note[] notes;
}

public class NotesManager : MonoBehaviour
{
    public TapNotesComponent tapNotesComp;
    public LongNotesComponent longNotesComp;

    public TextAsset notesData;

    [HideInInspector]
    public int noteNum;
    private int bpm;
    private int offset;

    void Awake()
    {
        noteNum = 0;
        Load();
    }

    void Load()
    {
        string inputString = notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // ���m�[�c���ݒ�
        noteNum = inputJson.notes.Length;

        bpm = inputJson.BPM;
        offset = inputJson.offset;

        // ���Ԍv�Z��List�ǉ��������֐�
        
    }

    public float GetNoteTime(Note note)
    {
        // �ꏬ�߂̒���
        float kankaku = 60 / (bpm * (float)note.LPB);
        // �m�[�c�Ԃ̒���
        float beatSec = kankaku * (float)note.LPB;
        // �J�n�O�̒���
        float preStartTime = offset * 0.01f + PlaySceneManager.psManager.preStartTime;
        // �m�[�c�̍~���Ă��鎞��
        float time = (beatSec * note.num / (float)note.LPB) + preStartTime;

        return time;
    }

    
}