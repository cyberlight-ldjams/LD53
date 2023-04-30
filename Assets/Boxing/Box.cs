using System.Collections;
using System.Collections.Generic;

public class Box : Item
{ 
    public List<Item> Items { get; private set; }

    public Order ForOrder { get; private set; }

    public int Size;

    public Box(Order order)
    {
        Items = new List<Item>();
        ForOrder = order;
        Size = 1;
    }

    public Box(Order order, List<Item> items)
    {
        this.Items = items;
        ForOrder = order;
        Size = 1;
    }

    public Box(Order order, List<Item> items, int size)
    {
        this.Items = items;
        ForOrder = order;
        this.Size = size;
    }

    public bool AddItem(Item item)
    {
        Items.Add(item);

        //TODO - if we want box sizes to be a thing
        //check against that here and return false if it fails

        return true;
    }
}
