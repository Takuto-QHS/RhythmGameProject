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
    [SerializeField]
    GameObject noteTapObj;
    [SerializeField]
    GameObject noteLongObj;

    [SerializeField]
    private TextAsset notesData;

    [HideInInspector]
    public int noteNum;
    private string songName;
    private int bpm;
    private int offset;

    [HideInInspector]
    public List<int> listLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listNoteType = new List<int>();
    //[HideInInspector]
    public List<float> listNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listNotesObj = new List<GameObject>();

    public Material longNoteLineMaterial;
    public Color longNoteLineColor;
    [HideInInspector]
    public List<int> listLongLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listLongNoteType = new List<int>();
    //[HideInInspector]
    public List<float> listLongNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listLongNotesObj = new List<GameObject>();

    void Start()
    {
        noteNum = 0;
        songName = "�e�X�g";
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // ���m�[�c���ݒ�
        noteNum = inputJson.notes.Length;

        bpm = inputJson.BPM;
        offset = inputJson.offset;

        // ���Ԍv�Z��List�ǉ��������֐�
        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            switch (inputJson.notes[i].type)
            {
                case 1: // �^�b�v�m�[�c
                    // �m�[�c�̍~���Ă��鎞��
                    float time = GetNoteTime(inputJson.notes[i]);

                    // ���X�g�ǉ�
                    listNotesTime.Add(time);
                    listLaneNum.Add(inputJson.notes[i].block);
                    listNoteType.Add(inputJson.notes[i].type);

                    // ����
                    InstantiateTapNotes(time, inputJson.notes[i].block);
                    break;

                case 2: // �����O�m�[�c
                    // �m�[�c�̍~���Ă��鎞��
                    float timeLongNote = GetNoteTime(inputJson.notes[i]);

                    // ���X�g�ǉ�
                    listLongNotesTime.Add(timeLongNote);
                    listLongLaneNum.Add(inputJson.notes[i].block);
                    listLongNoteType.Add(inputJson.notes[i].type);

                    // ����
                    InstantiateLongNotes(inputJson.notes[i], timeLongNote);
                    break;
            }
            
        }
    }

    float GetNoteTime(Note note)
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

    void InstantiateTapNotes(float time, int block)
    {
        float z = time * PlaySceneManager.psManager.notesSpeed;
        GameObject obj = Instantiate(noteTapObj, new Vector3(block - 2.5f, 0.03f, z), noteTapObj.transform.rotation);
        listNotesObj.Add(obj);
    }

    void InstantiateLongNotes(Note note , float time , LineRenderer line = null)
    {
        // ����
        float z = time * PlaySceneManager.psManager.notesSpeed;
        GameObject obj1 = Instantiate(noteLongObj, new Vector3(note.block - 2.5f, 0.03f, z), noteLongObj.transform.rotation);
        listLongNotesObj.Add(obj1);

        // ������������(Mesh�̉����_)
        Vector3[] lineVerticesVec3 = new Vector3[4];
        Vector3[] upperVec3 = GetObjVerticsUpperLower(obj1,true);
        lineVerticesVec3[0] = upperVec3[0];
        lineVerticesVec3[1] = upperVec3[1];

        // Note�̒���notes�z�񂩂琶��
        for (int x = 0; x < note.notes.Length; x++)
        {
            // �m�[�c�̍~���Ă��鎞��
            float timeLongNote = GetNoteTime(note.notes[x]);

            // ���X�g�ǉ�
            listLongNotesTime.Add(timeLongNote);
            listLongLaneNum.Add(note.notes[x].block);
            listLongNoteType.Add(note.notes[x].type);

            // ����
            float z2 = timeLongNote * PlaySceneManager.psManager.notesSpeed;
            GameObject obj2 = Instantiate(noteLongObj, new Vector3(note.notes[x].block - 2.5f, 0.03f, z2), noteLongObj.transform.rotation);
            listLongNotesObj.Add(obj2);

            // ������������(Mesh�̏㒸�_)
            Vector3[] lowerVec3 = GetObjVerticsUpperLower(obj2, false);
            lineVerticesVec3[2] = lowerVec3[0];
            lineVerticesVec3[3] = lowerVec3[1];

            InstantiateLongNotesLine(lineVerticesVec3);

            // ���̐�����������(Mesh�̉����_)
            Vector3[] upperVec3Obj2 = GetObjVerticsUpperLower(obj2, true);
            lineVerticesVec3[0] = upperVec3Obj2[0];
            lineVerticesVec3[1] = upperVec3Obj2[1];
        }

        // ���X�g�����~���Ă��鎞�ԏ��ɐ���

    }

    Vector3[] GetObjVerticsUpperLower(GameObject obj,bool upper)
    {
        Transform targetTransform = obj.transform;

        // �e���_�����[���h���W�ɕϊ�����
        Vector3[] vertices = obj.GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = targetTransform.TransformPoint(vertices[i]);
        }

        // obj�̏㒸�_�������_��Ԃ�
        Vector3[] pos = new Vector3[2];
        if (upper)
        {
            pos[0] = vertices[2];
            pos[1] = vertices[3];
        }
        else
        {
            pos[0] = vertices[0];
            pos[1] = vertices[1];
        }
        return pos;
    }

    void InstantiateLongNotesLine(Vector3[] _lineVerticesVec3)
    {
        // Quad�𐶐�
        GameObject lineObj = GameObject.CreatePrimitive(PrimitiveType.Quad);

        // Mesh���쐬���AMesh�[4�_�̒��_�����Z�b�g
        Mesh mesh = new Mesh();
        mesh.SetVertices(_lineVerticesVec3);

        // Quad��Mesh��2�̎O�p�`�ō\������Ă��܂�
        // �쐬���钸�_�̏��Ԃ��Z�b�g
        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;
        mesh.SetTriangles(triangles, 0);

        // ��������Quad�Ɋe������Z�b�g
        lineObj.GetComponent<MeshFilter>().mesh = mesh;
        lineObj.GetComponent<MeshRenderer>().material = longNoteLineMaterial;
        Notes notes = lineObj.AddComponent<Notes>();
        notes.isMoveZ = true;
    }
}
