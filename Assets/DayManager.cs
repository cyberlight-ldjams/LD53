using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public List<Item> possibleItems;

    public int minOrderSize = 1;

    public int maxOrderSize = 3;

    public int numberOfOrdersToday = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartNewDay()
    {
        GenerateOrders(numberOfOrdersToday, minOrderSize, maxOrderSize);
    }

    private List<Order> GenerateOrders(int orderNum, int orderSizeMin, int orderSizeMax)
    {
        List<Order> orders = new List<Order>(orderNum);

        for (int i = 0; i < orderNum; i++)
        {
            int orderSize = Random.Range(orderSizeMin, orderSizeMax);
            orders.Add(new Order(possibleItems, orderSize));
        }

        return orders;
    }
}
