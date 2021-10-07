using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager instance;

    GameObject slotCacheGO;
    GameObject slotRoot;
    [SerializeField] GameObject EmptySlot;
    float distanceBetweenSlots = 1f;

    // prepare singleton and fields to start
    public void Init()
    {
        instance = this;
        slotRoot = gameObject;
    }
    
    public bool IsBoardComplete()
    {
        SlotBehaviour[] slots = GetComponentsInChildren<SlotBehaviour>();
        foreach (var slot in slots)
        {
            if (slot.IsSlotFullyUsed)
                continue;
            return false;
        }
        return true;
    }

    // loot around of a slot and return random empty slot
    public SlotBehaviour FindASafeDirectionFor (SlotBehaviour sB)
    {

        //used for random selection
        List<SlotBehaviour> nearClearSlots = new List<SlotBehaviour>();

        // find near unused slots that are avaliably to paint
        if (sB.UpSlot != null && sB.UpSlot.IsSlotFullyAvaliable)
            nearClearSlots.Add(sB.UpSlot);
        if (sB.DownSlot != null && sB.DownSlot.IsSlotFullyAvaliable)
            nearClearSlots.Add(sB.DownSlot);
        if (sB.RightSlot != null && sB.RightSlot.IsSlotFullyAvaliable)
            nearClearSlots.Add(sB.RightSlot);
        if (sB.LeftSlot != null && sB.LeftSlot.IsSlotFullyAvaliable)
            nearClearSlots.Add(sB.LeftSlot);

        if (nearClearSlots.Count == 0)
            return null;
        else
        {
            int randomint = Random.Range(0, nearClearSlots.Count);
            return nearClearSlots[randomint];
        }

    }
    //deprecated
    public SlotBehaviour GetPerfectlyEmptySlot()
    {
        SlotBehaviour[] slot = GetComponentsInChildren<SlotBehaviour>();
        foreach (var eachSlot in slot)
        {
            if ( !eachSlot.IsSlotFullyUsed && !eachSlot.IsSlotPartiallyUsed )
            {
                return eachSlot;
            }
        }
        return null;
    }
    
    // get coordinates and return that slot behaviour
    public SlotBehaviour GetSlotOnCoordinate(int x,int y)
    {
        SlotBehaviour[] slot = GetComponentsInChildren<SlotBehaviour>();
        foreach (var eachSlot in slot)
        {
            if (eachSlot.CoordX == x && eachSlot.CoordY == y)
            {
                return eachSlot;
            }
        }
        return null;
    }
    // spawn grid 3b3 4b4 5b5 . triggered from game manager
    public void SpawnGrid(int gridSize)
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                slotCacheGO = Instantiate(EmptySlot,
                            new Vector3(distanceBetweenSlots * x, distanceBetweenSlots * y,0),
                            Quaternion.identity,
                            slotRoot.transform);
                Vector3 tmp = slotCacheGO.transform.localPosition;
                tmp.z = 0;
                slotCacheGO.transform.localPosition = tmp;
                slotCacheGO.GetComponent<SlotBehaviour>().CoordX = x;
                slotCacheGO.GetComponent<SlotBehaviour>().CoordY = y;

            }
        }
    }
    // initialize this to initilaze all the slots
    public void InitAfterSpawn()
    {
        SlotBehaviour[] slot = GetComponentsInChildren<SlotBehaviour>();
        foreach (var eachSlot in slot)
        {
            eachSlot.InitAfterSpawn();
        }
    }
}
