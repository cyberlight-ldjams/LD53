using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public float TimePerDay = 120f;
    
    public List<Item> possibleItems;

    public int minOrderSize = 1;

    public int maxOrderSize = 3;

    public int numberOfOrdersToday = 5;

    public OrderList orderListUI;

    public List<Order> orderList;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timer;

    private string scoreSymbol;

    public float Score;

    private float timeRemaining;

    // Whether or not the player is working
    // Or if we're between days
    private bool working;

    // Start is called before the first frame update
    void Start()
    {
        scoreSymbol = scoreText.text;
        Score = 0;
        timeRemaining = TimePerDay;
        SetTimer();
        working = true;

        StartNewDay();

        scoreText.text = Score + scoreSymbol;
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            timeRemaining -= Time.deltaTime;
            SetTimer();
        }
    }

    private void StartNewDay()
    {
        orderList = GenerateOrders(numberOfOrdersToday, minOrderSize, maxOrderSize);
        UpdateOrderList();
    }

    private void EndCurrentDay()
    {
        // Remove the previous orders
        float dailyTotal = 0;
        foreach (Order o in orderList)
        {
            dailyTotal += o.GetOrderValue();
            orderList.Remove(o);
        }

        UpdateOrderList();
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

    // Returns whether or not the day is complete
    public bool UpdateOrderList()
    {
        orderListUI.SetOrderText(orderList);

        scoreText.text = Score + scoreSymbol;

        foreach (Order o in orderList)
        {
            if (o.Completed == false)
            {
                return false;
            }
        }

        // If we made it here, all orders are completed
        EndCurrentDay();

        return true;
    }

    public Order CompleteOrder(Order order)
    {
        order.MarkCompleted();

        Score += order.GetOrderValue();

        UpdateOrderList();

        return order;
    }

    private void SetTimer()
    {
        int timeSeconds = 0;
        int timeMinutes = 0;

        timeMinutes = (int) timeRemaining / 60;
        timeSeconds = (int) timeRemaining % 60;

        if (timeSeconds < 10)
        {
            timer.text = "Time Remaining\r\n" + 
                timeMinutes + ":0" + timeSeconds;
        } else
        {
            timer.text = "Time Remaining\r\n" + 
                timeMinutes + ":" + timeSeconds;
        }

        
    }
}
