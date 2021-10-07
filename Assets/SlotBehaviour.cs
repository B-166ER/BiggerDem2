using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBehaviour : MonoBehaviour
{
    SlotManager SlotM;
    [SerializeField] GameObject TriangleObject;
    //[SerializeField] SlotManager SManager;
    public int CoordX;
    public int CoordY;

    public bool isBottomTriUsed;
    public bool isUpTriUsed;
    public bool isLeftTriUsed;
    public bool isRightTriUsed;

    public bool isBottomSlotInBounds;
    public bool isTopSlotInbounds;
    public bool isLeftSlotInbounds;
    public bool isRightSlotInbounds;

    public SlotBehaviour UpSlot;
    public SlotBehaviour DownSlot;
    public SlotBehaviour RightSlot;
    public SlotBehaviour LeftSlot;

    public TriangleBehaviour UpTri;
    public TriangleBehaviour DownTri;
    public TriangleBehaviour RightTri;
    public TriangleBehaviour LeftTri;


    public void InitAfterSpawn()
    {
        SlotM = SlotManager.instance;
        UpSlot = SlotM.GetSlotOnCoordinate(CoordX, CoordY + 1);
        DownSlot = SlotM.GetSlotOnCoordinate(CoordX, CoordY - 1);
        RightSlot = SlotM.GetSlotOnCoordinate(CoordX + 1, CoordY);
        LeftSlot = SlotM.GetSlotOnCoordinate(CoordX - 1, CoordY);

        isBottomSlotInBounds = IsBottomSlotInbounds;
        isTopSlotInbounds = IsTopSlotInbounds;
        isLeftSlotInbounds = IsLeftSlotInbounds;
        isRightSlotInbounds = IsRightSlotInbounds;

        UpTri.TriInit(TriPosition.Up);
        DownTri.TriInit(TriPosition.Down);
        RightTri.TriInit(TriPosition.Right);
        LeftTri.TriInit(TriPosition.Left);

        /*
        UpTri.gameObject.AddComponent<MeshFilter>();
        UpTri.gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = UpTri.gameObject.GetComponent<MeshFilter>().mesh;

        mesh.Clear();
        //should i fully use the slot
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0) ,
                                        new Vector3(0.5f, -0.5f, 0) ,
                                        new Vector3(-0.5f, -0.5f, 0) };

        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
        */
    }



public bool IsSlotFullyUsed
    {
        get
        {
            if (IsBottomTriUsed && IsUpTriUsed && IsLeftTriUsed && IsRightTriUsed)
                return true;
            else
                return false;
        }
    }
    public bool IsSlotPartiallyUsed
    {
        get
        {
            if ((IsBottomTriUsed || IsUpTriUsed || IsLeftTriUsed || IsRightTriUsed) && !(IsBottomTriUsed && IsUpTriUsed && IsLeftTriUsed && IsRightTriUsed))
                return true;
            else
                return false;
        }
    }
    public bool IsSlotFullyAvaliable
    {
        get
        {
            if (!IsSlotFullyUsed && !IsSlotPartiallyUsed )
                return true;
            else
                return false;
        }
    }

    public bool IsBottomTriUsed
    {
        get { return isBottomTriUsed; }
        set { isBottomTriUsed = value; }
    }
    public bool IsUpTriUsed
    {
        get { return isUpTriUsed; }
        set { isUpTriUsed = value; }
    }
    public bool IsLeftTriUsed
    {
        get { return isLeftTriUsed; }
        set { isLeftTriUsed = value; }
    }
    public bool IsRightTriUsed
    {
        get { return isRightTriUsed; }
        set { isRightTriUsed = value; }
    }

    public bool IsBottomSlotInbounds
    {
        get
        {
            SlotBehaviour nearSlot = SlotManager.instance.GetSlotOnCoordinate(CoordY - 1, CoordX);
            if (nearSlot == null) return false;
            else return true;
        }
        set { isBottomSlotInBounds = value; }
    }
    public bool IsTopSlotInbounds
    {
        get
        {
            SlotBehaviour nearSlot = SlotManager.instance.GetSlotOnCoordinate(CoordY + 1, CoordX);
            if (nearSlot == null) return false;
            else return true;
        }
        set { isTopSlotInbounds = value; }
    }
    public bool IsLeftSlotInbounds
    {
        get
        {
            SlotBehaviour nearSlot = SlotManager.instance.GetSlotOnCoordinate(CoordY, CoordX - 1);
            if (nearSlot == null) return false;
            else return true;
        }
        set { isLeftSlotInbounds = value; }
    }
    public bool IsRightSlotInbounds
    {
        get
        {
            SlotBehaviour nearSlot = SlotManager.instance.GetSlotOnCoordinate(CoordY, CoordX + 1);
            if (nearSlot == null) return false;
            else return true;
        }
        set { isRightSlotInbounds = value; }
    }


    public void GetCoords (out int x,out int y)
    {
        x = CoordX;
        y = CoordY;
    }

}
