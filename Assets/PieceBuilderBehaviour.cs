using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBuilderBehaviour : MonoBehaviour
{
    public static PieceBuilderBehaviour ins;
    [SerializeField] Material mat;
    [SerializeField] GameObject pieceParentPREFAB;
    int activePieceId = -1;
    Color activePieceColor;
    [SerializeField] GameObject activePieceParentGO;
    [SerializeField] GameObject AllPiecesRoot;
    public List<GameObject> PieceParents = new List<GameObject>();

    public void PrepareSelfForNewPiece()
    {
        activePieceId++;
        activePieceColor = GameManager.Instance.PieceIdsAndColors[activePieceId];

        activePieceParentGO = Instantiate(pieceParentPREFAB, AllPiecesRoot.transform);
        PieceParents.Add(activePieceParentGO);
        activePieceParentGO = PieceParents[activePieceId];
        PieceParents[activePieceId].transform.gameObject.GetComponent<PieceParentBehaviour>().id = activePieceId;
    }

    public IEnumerator GeneratePieceStartOn(int x,int y)
    {
        int difficulty = (int)GameManager.Instance.GameDifficulty + 1;
        // int howManySlotsForThisPiece = Random.Range( 1, GameManager.Instance.GridSize);

        while ( !SlotManager.instance.IsBoardComplete() )
        {
            int howManySlotsForThisPiece = Random.Range(difficulty, GameManager.Instance.GridSize*2);
            SlotBehaviour sb = SlotManager.instance.GetPerfectlyEmptySlot();
            if (sb != null)
                OccupieSlotOnThisSB(sb);
            for (int size = 0; size < howManySlotsForThisPiece; size++)
            {
                if (sb != null)
                    sb = SlotManager.instance.FindASafeDirectionFor(sb);
                if (sb != null)
                    OccupieSlotOnThisSB(sb);
                yield return new WaitForEndOfFrame();
            }
            if(!SlotManager.instance.IsBoardComplete())
                PrepareSelfForNewPiece();
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.StartTheGame();
        /*
        SlotBehaviour sb = SlotManager.instance.GetSlotOnCoordinate(x, y);
        if (sb != null)
            OccupieSlotOnThisSB(sb);
        sb = SlotManager.instance.FindASafeDirectionFor(sb);
        if (sb != null)
            OccupieSlotOnThisSB(sb);
        sb = SlotManager.instance.FindASafeDirectionFor(sb);
        if (sb != null)
            OccupieSlotOnThisSB(sb);
        sb = SlotManager.instance.FindASafeDirectionFor(sb);
        if (sb != null)
            OccupieSlotOnThisSB(sb);
        */
    }

    public void OccupieSlotOnThisSB(SlotBehaviour slotBehaviour)
    {
        OccupieSlotCompletly(slotBehaviour);
    }
    public void OccupieSlotCompletly(SlotBehaviour sb)
    {
        sb.IsBottomTriUsed = true;
        sb.IsUpTriUsed = true;
        sb.IsRightTriUsed = true;
        sb.IsLeftTriUsed = true;


        sb.DownTri.transform.parent = activePieceParentGO.transform;
        sb.DownTri.gameObject.GetComponent<MeshRenderer>().material = mat;
        sb.DownTri.gameObject.GetComponent<MeshRenderer>().material.color = activePieceColor;

        sb.UpTri.transform.parent = activePieceParentGO.transform;
        sb.UpTri.gameObject.GetComponent<MeshRenderer>().material = mat;
        sb.UpTri.gameObject.GetComponent<MeshRenderer>().material.color = activePieceColor;

        sb.LeftTri.transform.parent = activePieceParentGO.transform;
        sb.LeftTri.gameObject.GetComponent<MeshRenderer>().material = mat;
        sb.LeftTri.gameObject.GetComponent<MeshRenderer>().material.color = activePieceColor;

        sb.RightTri.transform.parent = activePieceParentGO.transform;
        sb.RightTri.gameObject.GetComponent<MeshRenderer>().material = mat;
        sb.RightTri.gameObject.GetComponent<MeshRenderer>().material.color = activePieceColor;
    }
    private void OnEnable()
    {
        ins = this;
    }
    public void Init()
    {
    }
    /*

    [SerializeField] SlotManager SlotM;
    [SerializeField] GameObject AllPieceRoot;

    [SerializeField] GameObject TrianglesSlot;
    [SerializeField] GameObject TriangleObjectPREFAB;


    private GameObject PieceParent;
    private GameObject TriangleObject;
    public void Init()
    {
        SlotM = SlotManager.instance;
        SlotBehaviour sb = SlotM.GetPerfectlyEmptySlot();
        BuildNewPiece(0, sb);
        sb = SlotM.GetPerfectlyEmptySlot();
        BuildNewPiece(0, sb);
    }

    void BuildNewPiece(int PId,SlotBehaviour sb)
    {
        //position your self on target slot
        Vector3 tmp = sb.transform.position;
        tmp.z = 0;
        gameObject.transform.position = tmp;


        //create a parent for multiple triangles
        PieceParent = Instantiate(TrianglesSlot, AllPieceRoot.transform);
        PieceParent.GetComponent<PieceParentBehaviour>().PieceId = PId;
        Color PieceColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );

        CreateUpTriangleOnThisPiece(PieceParent, PId, PieceColor, sb.GetComponent<SlotBehaviour>());
        CreateLeftTriangleOnThisPiece(PieceParent, PId, PieceColor, sb.GetComponent<SlotBehaviour>());
        CreateRightTriangleOnThisPiece(PieceParent, PId, PieceColor, sb.GetComponent<SlotBehaviour>());
        CreateDownTriangleOnThisPiece(PieceParent, PId, PieceColor, sb.GetComponent<SlotBehaviour>());
        // should i fully use the slot
    }

    void CreateUpTriangleOnThisPiece(GameObject PieceParent, int pieceId, Color clr, SlotBehaviour slot)
    {
        slot.IsUpTriUsed = true;
        // create a parent for triangle
        TriangleObject = Instantiate(TriangleObjectPREFAB, PieceParent.transform);

        TriangleObject.GetComponent<TriangleBehaviour>().PartOfPiece = PieceParent;
        TriangleObject.AddComponent<MeshFilter>();
        TriangleObject.AddComponent<MeshRenderer>();
        Mesh mesh = TriangleObject.GetComponent<MeshFilter>().mesh;

        TriangleObject.GetComponent<TriangleBehaviour>().Init(clr, slot, PieceParent);


        mesh.Clear();
        //should i fully use the slot
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0)+slot.transform.position, new Vector3(-0.5f, 0.5f, 0) + slot.transform.position, new Vector3(0.5f, 0.5f, 0) + slot.transform.position };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
    }
    void CreateLeftTriangleOnThisPiece(GameObject PieceParent, int pieceId, Color clr, SlotBehaviour slot)
    {
        slot.IsLeftTriUsed = true;
        // create a parent for triangle
        TriangleObject = Instantiate(TriangleObjectPREFAB, PieceParent.transform);

        TriangleObject.GetComponent<TriangleBehaviour>().PartOfPiece = PieceParent;
        TriangleObject.AddComponent<MeshFilter>();
        TriangleObject.AddComponent<MeshRenderer>();
        Mesh mesh = TriangleObject.GetComponent<MeshFilter>().mesh;

        TriangleObject.GetComponent<TriangleBehaviour>().Init(clr, slot, PieceParent);


        mesh.Clear();
        //should i fully use the slot
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0) + slot.transform.position, new Vector3(-0.5f, -0.5f, 0) + slot.transform.position, new Vector3(-0.5f, 0.5f, 0) + slot.transform.position };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
    }
    void CreateRightTriangleOnThisPiece(GameObject PieceParent, int pieceId, Color clr, SlotBehaviour slot)
    {
        slot.IsRightTriUsed = true;
        // create a parent for triangle
        TriangleObject = Instantiate(TriangleObjectPREFAB, PieceParent.transform);

        TriangleObject.GetComponent<TriangleBehaviour>().PartOfPiece = PieceParent;
        TriangleObject.AddComponent<MeshFilter>();
        TriangleObject.AddComponent<MeshRenderer>();
        Mesh mesh = TriangleObject.GetComponent<MeshFilter>().mesh;

        TriangleObject.GetComponent<TriangleBehaviour>().Init(clr, slot, PieceParent);


        mesh.Clear();
        //should i fully use the slot
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0) + slot.transform.position, new Vector3(0.5f, 0.5f, 0) + slot.transform.position, new Vector3(0.5f, -0.5f, 0) + slot.transform.position };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
    }
    void CreateDownTriangleOnThisPiece(GameObject PieceParent, int pieceId, Color clr, SlotBehaviour slot)
    {
        slot.isBottomTriUsed = true;
        // create a parent for triangle
        TriangleObject = Instantiate(TriangleObjectPREFAB, PieceParent.transform);

        TriangleObject.GetComponent<TriangleBehaviour>().PartOfPiece = PieceParent;
        TriangleObject.GetComponent<TriangleBehaviour>().PartOfPiece = PieceParent;
        TriangleObject.AddComponent<MeshFilter>();
        TriangleObject.AddComponent<MeshRenderer>();
        Mesh mesh = TriangleObject.GetComponent<MeshFilter>().mesh;

        TriangleObject.GetComponent<TriangleBehaviour>().Init(clr, slot, PieceParent);


        mesh.Clear();
        //should i fully use the slot
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0) + slot.transform.position, new Vector3(0.5f, -0.5f, 0) + slot.transform.position, new Vector3(-0.5f, -0.5f, 0) + slot.transform.position };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
    }

    private void OnEnable()
    {

        TriangleObject.AddComponent<MeshFilter>();
        TriangleObject.AddComponent<MeshRenderer>();
        Mesh mesh = TriangleObject.GetComponent<MeshFilter>().mesh;



        mesh.Clear();
        //should i fully use the slot
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0) + gameObject.transform.position, new Vector3(0.5f, -0.5f, 0) + gameObject.transform.position, new Vector3(-0.5f, -0.5f, 0) + gameObject.transform.position };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
    }
    */
}
