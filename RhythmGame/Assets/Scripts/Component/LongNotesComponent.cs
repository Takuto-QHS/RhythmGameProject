using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �d�l
 * ������InputTouchManager�Ŕ��聨Perfect�����Ƀ��[������������ԂȂ�OK
 * */

public class LongNotesComponent : MonoBehaviour
{
    [SerializeField]
    NotesJudgeController notesJudgeController;

    public Material longNoteLineMaterial;

    [SerializeField]
    GameObject noteLongObj;

    [HideInInspector]
    public List<int> listLaneNum = new List<int>();
    [HideInInspector]
    public List<int> listNoteType = new List<int>();
    //[HideInInspector]
    public List<float> listNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> listLongNotesObj = new List<GameObject>();

    void Start()
    {
        InitNotes();
        InputTouchManager.deligateLongTapJudge += RaneJudge;
    }

    // Update is called once per frame
    void Update()
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        //�{���m�[�c���������ׂ����Ԃ���0.15�b�����Ă����͂��Ȃ������ꍇ
        if (Time.time > listNotesTime[0] + notesJudgeController.timeBadMiss)
        {
            notesJudgeController.JudgeMiss(listLaneNum[0]);
            DeleteData();
        }
    }

    void InitNotes()
    {
        string inputString = PlaySceneManager.psManager.notesManager.notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].type == 2)
            {
                float time = PlaySceneManager.psManager.notesManager.GetNoteTime(inputJson.notes[i]); // �m�[�c�̍~���Ă��鎞��

                // ����
                InstantiateLongNotes(inputJson.notes[i], time);
                break;
            }

        }
    }

    void InstantiateLongNotes(Note note, float time, LineRenderer line = null)
    {
        // ���X�g�ǉ�
        listNotesTime.Add(time);
        listLaneNum.Add(note.block);
        listNoteType.Add(note.type);

        // ����
        float z = time * PlaySceneManager.psManager.notesSpeed;
        GameObject obj1 = Instantiate(noteLongObj, new Vector3(note.block - 2.5f, 0.03f, z), noteLongObj.transform.rotation);
        listLongNotesObj.Add(obj1);

        // ������������(Mesh�̉����_)
        Vector3[] lineVerticesVec3 = new Vector3[4];
        Vector3[] upperVec3 = GetObjVerticsUpperLower(obj1, true);
        lineVerticesVec3[0] = upperVec3[0];
        lineVerticesVec3[1] = upperVec3[1];

        // Note�̒���notes�z�񂩂琶��
        for (int x = 0; x < note.notes.Length; x++)
        {
            // �m�[�c�̍~���Ă��鎞��
            float timeLongNote = PlaySceneManager.psManager.notesManager.GetNoteTime(note.notes[x]);

            // ���X�g�ǉ�
            listNotesTime.Add(timeLongNote);
            listLaneNum.Add(note.notes[x].block);
            listNoteType.Add(note.notes[x].type);

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

    Vector3[] GetObjVerticsUpperLower(GameObject obj, bool upper)
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

    public void DeleteData()//���łɂ��������m�[�c������폜����֐�
    {
        listNotesTime.RemoveAt(0);
        listLaneNum.RemoveAt(0);
        listNoteType.RemoveAt(0);
    }

    public void RaneJudge(int _hitsRaneNum)
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        if (listLaneNum[0] == _hitsRaneNum)
        {
            // ����^�C�v�擾
            NotesJudgeController.EJudgeType eJudgeType;
            eJudgeType = notesJudgeController.Judgement(listNotesTime[0], listLaneNum[0], true);
            
            // �p�[�t�F�N�g���A���菈�����List�v�f�폜
            if (eJudgeType == NotesJudgeController.EJudgeType.Perfect)
            {
                notesJudgeController.Judgement(listNotesTime[0], listLaneNum[0]);
                DeleteData();
            }
        }
    }
}
