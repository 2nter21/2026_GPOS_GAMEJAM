using System;
using TMPro;
using UnityEngine;

public class StatusCounter : MonoBehaviour
{
    private int n = 0;

    // Update is called once per frame
    void Update()
    {


        if (n > 1000000)
        {
            GetComponent<TextMeshProUGUI>().text = ((double) n / 1000000).ToString("F2") + "M";
        }
        else if (n > 1000)
        {
            GetComponent<TextMeshProUGUI>().text = ((double)n / 1000).ToString("F2") + "K";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = n.ToString();
        }
    }

    public void setCount(int i)
    {
        n = i;
    }
}
