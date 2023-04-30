using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public float TimePerDay = 120f;
    
    public List<Item> possibleItems;

    public int minOrderSize = 1;

    public int maxOrderSize = 1;

    public int addToOrderSizePerDay = 1;

    public int maxOrderSizeEver = 5;

    public int numberOfOrdersToday = 3;

    public int addOrdersPerDay = 1;

    public OrderList orderListUI;

    public List<Order> orderList;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timer;

    public TextMeshProUGUI youLose;

    public TextMeshProUGUI youWin;

    public GameObject opening;

    public GameObject nextDayDialogue;

    public TextMeshProUGUI nextDayText;

    public GameObject player;

    private string scoreSymbol;

    public float Score = 0f;

    public float ScoreToWin = 500f;

    private float timeRemaining;

    // Whether or not the player is working
    // Or if we're between days
    private bool working;

    // Start is called before the first frame update
    void Start()
    {
        scoreSymbol = scoreText.text;
        timeRemaining = TimePerDay;
        SetTimer();

        StartNewDay();
        Pause();

        scoreText.text = Score + scoreSymbol + " Earned";

        //Just so we can hide this by default
        if(!opening.activeInHierarchy)
        {
            Debug.Log("Note: Skipping intro text.");
            Begin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            timeRemaining -= Time.deltaTime;
            SetTimer();

            if (Score >= ScoreToWin)
            {
                YouWin();
            }
            else if (timeRemaining < 0f)
            {
                GameOver();
            }
        }
    }

    private void StartNewDay()
    {
        orderList = GenerateOrders(numberOfOrdersToday, minOrderSize, maxOrderSize + 1);
        UpdateOrderList();
        timeRemaining = TimePerDay;
        working = true;
    }

    private void EndCurrentDay()
    {
        // Remove the previous orders
        float dailyTotal = 0;
        int totalItemsToday = 0;
        foreach (Order o in orderList)
        {
            dailyTotal += o.GetOrderValue();
            totalItemsToday += o.orderItems.Count;
        }
        orderList.Clear();

        Pause();

        string nextDayString = "\tAll of the orders for the day have been fullfilled! " +
            "You can now take a breath. Be sure to come back tomorrow to keep working, " +
            "you still need to earn " + (ScoreToWin - Score) + scoreSymbol + " in order " +
            "to pay off your debt.\r\n\tToday you completed " + numberOfOrdersToday + " orders, " +
            "boxing up " + totalItemsToday + " items and earned " + dailyTotal + scoreSymbol;
        nextDayText.text = nextDayString;
        nextDayDialogue.SetActive(true);

        // The player is too good! Make it more difficult
        maxOrderSize += addToOrderSizePerDay;
        numberOfOrdersToday += addOrdersPerDay;

        if (maxOrderSize > maxOrderSizeEver)
        {
            maxOrderSize = maxOrderSizeEver;
        }
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

        scoreText.text = Score + scoreSymbol + " Earned";

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
        if (timeRemaining < 0f)
        {
            timer.text = "Time Remaining\r\n0:00";
            timer.color = Color.red;
        } else if (timeRemaining > 60f)
        {
            timer.color = Color.white;
        } else if (timeRemaining < 20f)
        {
            timer.color = Color.red;
        } else if (timeRemaining < 60f)
        {
            timer.color = Color.yellow;
        }

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

    public void YouWin()
    {
        Pause();
        youWin.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        Pause();
        youLose.gameObject.SetActive(true);
    }

    public void Pause()
    {
        working = false;
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void Begin()
    {
        opening.SetActive(false);
        working = true;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void NextDay()
    {
        nextDayDialogue.SetActive(false);
        working = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        StartNewDay();
    }
}
