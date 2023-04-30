using System.Collections;
using System.Collections.Generic;

public class Box : Holdable
{ 
    public List<Item> Items { get; private set; }

    public Order ForOrder { get; private set; }

    public Box(Order order)
    {
        Init(order, new List<Item>(), 1);
    }

    public Box(Order order, List<Item> items)
    {
        Init(order, items, 1);
    }

    public Box(Order order, List<Item> items, int size)
    {
        
        Init(order, items, size);
    }

    public void Init(Order order, List<Item> items, int size)
    {

        this.Items = items;
        ForOrder = order;
        this.Size = size;
        box = true;
    }

    public bool AddItem(Item item)
    {
        Items.Add(item);

        //TODO - if we want box sizes to be a thing
        //check against that here and return false if it fails

        return true;
    }
}
