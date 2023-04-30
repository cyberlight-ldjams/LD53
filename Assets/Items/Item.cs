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

    // Use this if the scale needs to be modified - multiply by this
    public Vector3 scaleCorrection = new Vector3(1f, 1f, 1f);
}
