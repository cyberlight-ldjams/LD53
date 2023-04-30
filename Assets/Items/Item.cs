using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;

    // Flavor text
    public string description;
    
    public GameObject model;

    public float value;

    public Texture2D Icon;

    public Vector3 scaleCorrection = Vector3.one;

    public Vector3 AutoRescale(Transform parent)
    {
        return new Vector3(
            (1f / parent.lossyScale.x) * model.transform.localScale.x * scaleCorrection
    .x,
            (1f / parent.lossyScale.y) * model.transform.localScale.y * scaleCorrection
    .y,
            (1f / parent.lossyScale.z) * model.transform.localScale.z * scaleCorrection
    .z);
    }

    public GameObject Rescale(Transform parent, Vector3 positionCorrection) 
    {
        GameObject scaledItem = Instantiate(model, Vector3.zero,
            Quaternion.identity, parent);
        Vector3 localPosition = new Vector3(0f + positionCorrection.x,
            1.5f + positionCorrection.y, 0f + positionCorrection.z);
        scaledItem.transform.localPosition = localPosition;
        scaledItem.transform.localScale = AutoRescale(parent);

        return scaledItem;
    }
}
