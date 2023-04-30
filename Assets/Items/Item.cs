using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;

    // Flavor text
    public string description;
    
    public GameObject model;

    public float value;

    public Vector3 scaleCorrection = Vector3.one;

    public GameObject Rescale(Transform parent, Vector3 positionCorrection) 
    {
        GameObject scaledItem = Instantiate(model, Vector3.zero,
            Quaternion.identity, parent);
        Vector3 localPosition = new Vector3(0f + positionCorrection.x,
            1.5f + positionCorrection.y, 0f + positionCorrection.z);
        Debug.Log("LocPos: " + localPosition);
        scaledItem.transform.localPosition = localPosition;
        scaledItem.transform.localScale = new Vector3(
            (1f / parent.localScale.x) * model.transform.localScale.x * scaleCorrection
    .x,
            (1f / parent.localScale.y) * model.transform.localScale.y * scaleCorrection
    .y,
            (1f / parent.localScale.z) * model.transform.localScale.z * scaleCorrection
    .z);

        return scaledItem;
    }
}
