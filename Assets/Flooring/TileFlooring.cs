using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFlooring : MonoBehaviour
{
    public List<GameObject> Blocks;

    public int Length = 50;

    public int Width = 50;

    public Vector3 Center = Vector3.zero;

    public float Height = 0;

    public GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        PlaceFloorGrid();
    }

    private void PlaceFloorGrid()
    {
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                GameObject go = ChooseRandomBlock();
                go.transform.position = new Vector3((i * 2) - Length + Center.x, 
                    Height + Center.y, (j * 2) - Width + Center.z);
            }
        }
    }

    private GameObject ChooseRandomBlock()
    {
        int r = Random.Range(0, Blocks.Count);

        GameObject block;

        if (Parent != null)
        {
            block = Instantiate(Blocks[r], Parent.transform);
        }
        else
        {
            block = Instantiate(Blocks[r]);
        }

        block.transform.Rotate(ChooseRandomRotation());

        return block;
    }

    private Vector3 ChooseRandomRotation()
    {
        int x = Random.Range(0, 4) * 90;
        int y = Random.Range(0, 4) * 90;
        int z = Random.Range(0, 4) * 90;

        return new Vector3(x, y, z);
    }
}
