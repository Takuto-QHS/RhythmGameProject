using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �d�l
 * ������InputTouchManager�Ŕ��聨Perfect�����Ƀ��[������������ԂȂ�OK
 * */

/// <summary>
/// 1�̃����O�m�[�c�O���[�v
/// </summary>
public class LongNoteGroup
{
    public List<LongNote> listLongNoteGroup = new List<LongNote>();
}

/// <summary>
/// �����O�m�[�c���`������ŏ��O���[�v
/// </summary>
public class LongNote
{
    public GameObject objLongNote;
    public GameObject objLongNoteLine;
}

public class LongNotesComponent : MonoBehaviour
{
    [SerializeField]
    NotesJudgeController notesJudgeController;

    public Material longNoteLineMaterial;

    [SerializeField]
    GameObject noteLongObj;

    [SerializeField]
    float destroyNoteTime = 3.0f;

    /// <summary>
    /// �����O�m�[�c�������ł̃����O�m�[�c��������Ή�(����������)�͌��ݑΉ����Ă��܂���
    /// ����ꍇ��listLongNotesObj�Ɠ��l�ɂ��鎖�ŁA�f�[�^�̎������͐������Ȃ�Ǝv���܂�
    /// </summary>
    // �����O�m�[�c���莞�p
    private List<int> listLaneNum = new List<int>();
    private List<int> listNoteType = new List<int>();
    private List<float> listNotesTime = new List<float>();
    // �����O�m�[�cObj�폜�p
    private List<LongNoteGroup> listLongNotesObj = new List<LongNoteGroup>();

    void Start()
    {
        InitNotes();
        GameSceneManager.gsManager.inputPlayScene.deligateLongTapJudge += RaneJudge;
    }

    // Update is called once per frame
    void Update()
    {
        if (listNotesTime.Count == 0)
        {
            return;
        }

        //�{���m�[�c���������ׂ����Ԃ���0.15�b�����Ă����͂��Ȃ������ꍇ
        if (GameSceneManager.gsManager.playStopWatchTime > listNotesTime[0] + notesJudgeController.timeBadMiss)
        {
            notesJudgeController.JudgeMiss(listLaneNum[0]);
            DeleteData();
        }
    }

    void InitNotes()
    {
        string inputString = GameSceneManager.gsManager.notesManager.notesData.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].type == 2)
            {
                // �m�[�c�̔��莞��
                float time = GameSceneManager.gsManager.notesManager.GetNoteTime(inputJson.notes[i]);

                // �m�[�c�̍~���Ă��鎞��
                float posTime = GameSceneManager.gsManager.notesManager.GetNoteTime(inputJson.notes[i], true);

                // ����
                InstantiateLongNotes(inputJson.notes[i], time ,posTime);
                break;
            }

        }
    }

    void InstantiateLongNotes(Note _note, float _time, float _posTime, LineRenderer _line = null)
    {
        // ���X�g�ǉ�
        listNotesTime.Add(_time);
        listLaneNum.Add(_note.block);
        listNoteType.Add(_note.type);

        // ����
        float z = _posTime * GameSceneManager.gsManager.notesSpeed;
        GameObject obj1 = Instantiate(noteLongObj, new Vector3(_note.block - 2.5f, 0.03f, z), noteLongObj.transform.rotation);
        List<LongNote> listLongNote = new List<LongNote>();
        LongNote longNote1 = new LongNote();
        longNote1.objLongNote = obj1;
        listLongNote.Add(longNote1);

        // ������������(Mesh�̉����_)
        Vector3[] lineVerticesVec3 = new Vector3[4];
        Vector3[] upperVec3 = GetObjVerticsUpperLower(obj1, true);
        lineVerticesVec3[0] = upperVec3[0];
        lineVerticesVec3[1] = upperVec3[1];

        // Note�̒���notes�z�񂩂琶��
        for (int x = 0; x < _note.notes.Length; x++)
        {
            // �m�[�c�̔��莞��
            float timeLongNote = GameSceneManager.gsManager.notesManager.GetNoteTime(_note.notes[x]);
            // �m�[�c�̍~���Ă��鎞��
            float postimeLongNote = GameSceneManager.gsManager.notesManager.GetNoteTime(_note.notes[x], true);

            // ���X�g�ǉ�
            listNotesTime.Add(timeLongNote);
            listLaneNum.Add(_note.notes[x].block);
            listNoteType.Add(_note.notes[x].type);

            // ����
            float z2 = postimeLongNote * GameSceneManager.gsManager.notesSpeed;
            GameObject obj2 = Instantiate(noteLongObj, new Vector3(_note.notes[x].block - 2.5f, 0.03f, z2), noteLongObj.transform.rotation);
            LongNote longNote2 = new LongNote();
            longNote2.objLongNote = obj2;

            // ������������(Mesh�̏㒸�_)
            Vector3[] lowerVec3 = GetObjVerticsUpperLower(obj2, false);
            lineVerticesVec3[2] = lowerVec3[0];
            lineVerticesVec3[3] = lowerVec3[1];

            longNote2.objLongNoteLine = InstantiateLongNotesLine(lineVerticesVec3);
            listLongNote.Add(longNote2);

            // ���̐�����������(Mesh�̉����_)
            Vector3[] upperVec3Obj2 = GetObjVerticsUpperLower(obj2, true);
            lineVerticesVec3[0] = upperVec3Obj2[0];
            lineVerticesVec3[1] = upperVec3Obj2[1];
        }

        // �o����1�̃����O�m�[�c��Add
        LongNoteGroup result = new LongNoteGroup();
        result.listLongNoteGroup = listLongNote;
        listLongNotesObj.Add(result);
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

    GameObject InstantiateLongNotesLine(Vector3[] _lineVerticesVec3)
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
        lineObj.GetComponent<MeshCollider>().sharedMesh = mesh; // �����蔻��

        Notes notes = lineObj.AddComponent<Notes>();
        notes.isMoveZ = true;
        notes.isMove = false;
        notes.isLongNote = true;

        return lineObj;
    }

    public void DeleteData()//���łɂ��������m�[�c������폜����֐�
    {
        listNotesTime.RemoveAt(0);
        listLaneNum.RemoveAt(0);
        listNoteType.RemoveAt(0);


        // �����O�m�[�cObj�ƃf�[�^�̍폜
        StartCoroutine("ObjDelayDestroy", listLongNotesObj[0].listLongNoteGroup[0]);
        listLongNotesObj[0].listLongNoteGroup.RemoveAt(0);

        // �����O�m�[�c�I���̏ꍇ��listLongNotesObj�̗v�f���̂��폜
        if (listLongNotesObj[0].listLongNoteGroup.Count == 0)
        {
            listLongNotesObj.RemoveAt(0);
        }
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

    public void MoveNotes(bool _isMove)
    {
        foreach (LongNoteGroup listLongNoteGroup in listLongNotesObj)
        {
            foreach(LongNote longNotes in listLongNoteGroup.listLongNoteGroup)
            {
                if (longNotes.objLongNote == null) continue;
                Notes thisNote = longNotes.objLongNote.GetComponent<Notes>();
                thisNote.isMove = _isMove;

                if (longNotes.objLongNoteLine == null) continue;
                Notes thisNoteLine = longNotes.objLongNoteLine.GetComponent<Notes>();
                thisNoteLine.isMove = _isMove;
            }
        }
    }

    IEnumerator ObjDelayDestroy(LongNote obj)
    {
        yield return new WaitForSeconds(destroyNoteTime);
        Destroy(obj.objLongNote);
        Destroy(obj.objLongNoteLine);
    }
}
