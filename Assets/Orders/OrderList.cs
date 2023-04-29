using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderList : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI orderListText;

    public void SetOrderText(List<Order> orders)
    {
        string orderList = "Order List for Today: \r\n\r\n";

        for (int i = 0; i < orders.Count; i++)
        {
            orderList += "Order " + (i + 1) + "\r\n";
            for (int j = 0; j < orders[i].GetOrderItemCount(); j++)
            {
                string itemName = orders[i].GetOrderItem(j).itemName;
                orderList += itemName + "\r\n";
            }

            orderList += "\r\n";
        }

        orderListText.text = orderList;
    }

    public string GetOrderText()
    {
        return orderListText.text;
    }
}
