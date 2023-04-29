using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;

    // Flavor text
    public string description;
    
    public GameObject model;

    public float value;
}
