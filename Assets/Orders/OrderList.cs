using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderList : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI listTitle;

    [SerializeField]
    private TextMeshProUGUI listCol1;

    [SerializeField]
    private TextMeshProUGUI listCol2;

    [SerializeField]
    private TextMeshProUGUI listCol3;

    public void SetOrderText(List<Order> orders)
    {
        string title = "Order List for Today: \r\n\r\n";
        string col1 = "";
        string col2 = "";
        string col3 = "";

        for (int i = 0; i < orders.Count; i++)
        {
            // If we're a multiple of 3, build column 1
            if (i % 3 == 0)
            {
                col1 += "--- Order " + (i + 1) + " ---\r\n";
                for (int j = 0; j < orders[i].GetOrderItemCount(); j++)
                {
                    string itemName = orders[i].GetOrderItem(j).itemName;
                    col1 += itemName + "\r\n";
                }

                col1 += "\r\n";
            }

            // If we're a multiple of 3 less 1, build column 2
            else if (i % 3 == 1)
            {
                col2 += "--- Order " + (i + 1) + " ---\r\n";
                for (int j = 0; j < orders[i].GetOrderItemCount(); j++)
                {
                    string itemName = orders[i].GetOrderItem(j).itemName;
                    col2 += itemName + "\r\n";
                }

                col2 += "\r\n";
            }

            // If we're a multiple of 3 less 2, build column 3
            if (i % 3 == 2)
            {
                col3 += "--- Order " + (i + 1) + " ---\r\n";
                for (int j = 0; j < orders[i].GetOrderItemCount(); j++)
                {
                    string itemName = orders[i].GetOrderItem(j).itemName;
                    col3 += itemName + "\r\n";
                }

                col3 += "\r\n";
            }
        }

        // Build the list
        listTitle.text = title;
        listCol1.text = col1;
        listCol2.text = col2;
        listCol3.text = col3;
    }

    public string GetOrderText()
    {
        string listText;

        // This is a rough way to do this
        // If this is ever actually needed for something user visible
        // Then this should be re-coded to interleave the columns
        listText = listTitle.text + "\r\n" + listCol1.text + "\r\n" +
            listCol2.text + "\r\n" + listCol3.text + "\r\n";

        return listText;
    }
}
