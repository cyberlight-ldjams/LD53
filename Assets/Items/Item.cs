using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;

    // Flavor text
    public string description;
    
    public GameObject model;

    public float value;

    public bool isBox = false;

    public Box box;
}
