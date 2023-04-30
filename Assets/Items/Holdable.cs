using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Holdable 
{

    public Item ItemReference;

    public int Size = 0;

    protected bool box = false;

    public GameObject heldModel = null;

    //
    public bool IsBox()
    {
        return box;
    }

    public static Holdable FromItem(Item item)
    {
        return new Holdable()
        {
            ItemReference = item,
        };
    }
}
