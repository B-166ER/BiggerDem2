using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy=2, Mid=3, Hard=4 };

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int GridSize;
    public Difficulty GameDifficulty;
    public GameObject EmptySlot;
    public GameObject SlotRoot;
    [SerializeField] SlotManager SRoot;
    [SerializeField] PieceBuilderBehaviour pieceBuilder;
    [SerializeField] GameObject AllPiecesRoot;

    public Dictionary<int,Color> PieceIdsAndColors = new Dictionary<int, Color>();



    //populate color Dictionary
    //prepare singleton
    private void OnEnable()
    {
        PieceIdsAndColors.Add(0, Color.white);
        PieceIdsAndColors.Add(1, Color.black);
        PieceIdsAndColors.Add(2, Color.gray);
        PieceIdsAndColors.Add(3, Color.red);
        PieceIdsAndColors.Add(4, Color.green);
        PieceIdsAndColors.Add(5, Color.blue);
        PieceIdsAndColors.Add(6, Color.magenta);
        PieceIdsAndColors.Add(7, Color.cyan);
        PieceIdsAndColors.Add(8, Color.green);
        PieceIdsAndColors.Add(9, Color.yellow);
        Instance = this;

        //decide grid size
        GridSize = Random.Range(3, 6);
    }

    //control who starts first
    void Start()
    {
        PieceBuilderBehaviour.ins.Init();
        SRoot.Init();
        SRoot.SpawnGrid(GridSize);
        SRoot.InitAfterSpawn();
        PieceBuilderBehaviour.ins.PrepareSelfForNewPiece();
        //PieceBuilderBehaviour.ins.GeneratePieceStartOn(0, 0);
        StartCoroutine(PieceBuilderBehaviour.ins.GeneratePieceStartOn(0, 0));
    }
    public void StartTheGame()
    {
        PieceParentBehaviour[] allPieceParents = AllPiecesRoot.GetComponentsInChildren<PieceParentBehaviour>();
        
        // handle triangle colliders 
        //merge all triangle colliders and put a clone of it to the piece root object
        foreach (var item in allPieceParents)
        {
            item.gameObject.AddComponent<MeshRenderer>();
            item.gameObject.AddComponent<MeshFilter>();

            MeshFilter[] meshes = item.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshes.Length];
            int i = 0;
            while (i < meshes.Length)
            {
                combine[i].mesh = meshes[i].sharedMesh;
                combine[i].transform = meshes[i].transform.localToWorldMatrix;
                //meshes[i].gameObject.SetActive(false);

                i++;
            }

            item.transform.GetComponent<MeshFilter>().mesh = new Mesh();
            item.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            item.gameObject.AddComponent<MeshCollider>();
            //collidders END

            MoveThePieces();
        }
        void MoveThePieces()
        {
            //bug appears on second to all pieces when moving
            /*
            PieceParentBehaviour[] pParents = AllPiecesRoot.GetComponentsInChildren<PieceParentBehaviour>();
            Vector3 tmp = pParents[0].gameObject.transform.localPosition;
            tmp.x -= 2;
            tmp.y -= 1;
            pParents[0].gameObject.transform.localPosition = tmp;

             tmp = pParents[1].gameObject.transform.localPosition;
            tmp.x -= 2;
            tmp.y -= 1;
            pParents[1].gameObject.transform.localPosition = tmp;
            */
        }
    }

}
