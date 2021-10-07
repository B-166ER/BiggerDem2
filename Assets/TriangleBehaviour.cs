using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriPosition { Up, Down, Right, Left };
public class TriangleBehaviour : MonoBehaviour
{
    public TriPosition Tpos;

    public void TriInit(TriPosition pos)
    {
        Tpos = pos;
        gameObject.gameObject.AddComponent<MeshFilter>();
        gameObject.gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = gameObject.gameObject.GetComponent<MeshFilter>().mesh;


        mesh.Clear();


        if (pos == TriPosition.Up)
        {
            mesh.vertices = new Vector3[] { new Vector3(0, 0, 0) , new Vector3(-0.5f, 0.5f, 0) , new Vector3(0.5f, 0.5f, 0)  };
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
            mesh.triangles = new int[] { 0, 1, 2 };
        }
        else if (pos == TriPosition.Down)
        {
            mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0.5f, -0.5f, 0), new Vector3(-0.5f, -0.5f, 0) };
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
            mesh.triangles = new int[] { 0, 1, 2 };
        }
        else if (pos == TriPosition.Right)
        {
            mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0.5f, 0.5f, 0), new Vector3(0.5f, -0.5f, 0) };
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
            mesh.triangles = new int[] { 0, 1, 2 };
        }
        else if (pos == TriPosition.Left)
        {
            mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-0.5f, -0.5f, 0), new Vector3(-0.5f, 0.5f, 0) };
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
            mesh.triangles = new int[] { 0, 1, 2 };
        }

    }

    /*
    public SlotBehaviour OnSlot;
    public GameObject PartOfPiece;
    [SerializeField] Material mat;
    public void Init(Color clr, SlotBehaviour onSlot, GameObject partOfPiece )
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
    */

}

