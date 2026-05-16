using System;
using TMPro;
using UnityEngine;

public class CrewCounter : MonoBehaviour
{
    public String s;

    private int n = 0;
    // Update is called once per frame
    void Update()
    {
        if (s == "Programmer")
        {

        }
        else if (s == "Planner")
        {

        }
        else if (s == "Art")
        {

        }

        if (n > 1000000)
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:F2}M", n / 1000);
        }
        else if (n > 1000)
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:F2}K", n / 1000);
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = n.ToString();
        }
    }

    public void setN(int i)
    {
        n = i;
    }
}
